using Moq;
using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.Services;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.Tests.ApplicationTests
{
    public class NoteServiceTests
    {
        private readonly Mock<INoteRepository> _noteRepoMock;
        private readonly Mock<ITagRepository> _tagRepoMock;
        private readonly NoteService _service;

        public NoteServiceTests()
        {
            _noteRepoMock = new Mock<INoteRepository>();
            _tagRepoMock = new Mock<ITagRepository>();
            _service = new NoteService(_noteRepoMock.Object, _tagRepoMock.Object);
        }

        [Fact]
        public async Task AddAsyncWithNewTag()
        {
            var dto = new CreateNoteDto
            {
                Name = "Test",
                Description = "Test description",
                Tags = new List<string>
                {
                    "Tag1",
                    "Tag2"
                }
            };

            _tagRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<Tag>());


            await _service.AddAsync(dto);

            _noteRepoMock.Verify(r => r.AddAsync(
               It.Is<Note>(n =>
                   n.Name == "Test" &&
                   n.Description == "Test description" &&
                   n.Tags.Count == 2 &&
                   n.Tags.Any(t => t.Name == "Tag1") &&
                   n.Tags.Any(t => t.Name == "Tag2")
               ),
               It.IsAny<CancellationToken>()
           ), Times.Once);
        }

        [Fact]
        public async Task AddAsyncWithExistingTag()
        {
            var dto = new CreateNoteDto
            {
                Name = "Test Note",
                Description = "Desc",
                Tags = new List<string> { "Tag1", "Tag2" }
            };

            var existingTags = new List<Tag> { new Tag { Name = "Tag1" } };

            _tagRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(existingTags);

            await _service.AddAsync(dto);

            _noteRepoMock.Verify(r => r.AddAsync(
                It.Is<Note>(n =>
                    n.Tags.Count == 2 &&
                    n.Tags.Any(t => t.Name == "Tag1") &&
                    n.Tags.Any(t => t.Name == "Tag2")
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNoteDto_WhenNoteExists()
        {
            var note = new Note { Id = Guid.NewGuid(), Name = "Test", Tags = new List<Tag>() };
            _noteRepoMock.Setup(r => r.GetByIdAsync(note.Id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(note);

            var result = await _service.GetByIdAsync(note.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFound_WhenNoteDoesNotExist()
        {
            var id = Guid.NewGuid();
            _noteRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Note?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDelete_WhenNoteExists()
        {
            var note = new Note { Id = Guid.NewGuid() };

            _noteRepoMock.Setup(r => r.GetByIdAsync(note.Id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(note);

            await _service.DeleteAsync(note.Id);

            _noteRepoMock.Verify(r => r.DeleteAsync(note.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFound_WhenNoteDoesNotExist()
        {
            var id = Guid.NewGuid();
            _noteRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Note?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(id));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingNote()
        {
            var id = Guid.NewGuid();
            var note = new Note { Id = id, Name = "Old", Tags = new List<Tag>() };
            _noteRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(note);
            _tagRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new List<Tag>());

            var dto = new CreateNoteDto { Name = "New", Description = "NewDesc", Tags = new List<string> { "Tag1" } };

            await _service.UpdateAsync(id, dto);

            Assert.Equal("New", note.Name);
            Assert.Equal("NewDesc", note.Description);
            Assert.Single(note.Tags);
            Assert.Equal("Tag1", note.Tags[0].Name);
            _noteRepoMock.Verify(r => r.UpdateAsync(note, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowKeyNotFound_WhenNoteDoesNotExist()
        {
            var id = Guid.NewGuid();
            _noteRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Note?)null);

            var dto = new CreateNoteDto { Name = "New", Description = "NewDesc", Tags = new List<string>() };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(id, dto));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllNotes()
        {
            var notes = new List<Note>
            {
                new Note { Id = Guid.NewGuid(), Name = "Note1", Tags = new List<Tag>() },
                new Note { Id = Guid.NewGuid(), Name = "Note2", Tags = new List<Tag>() }
            };
            _noteRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(notes);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, n => n.Name == "Note1");
            Assert.Contains(result, n => n.Name == "Note2");
        }
    }
}
