using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.DTO.Response;
using WebAppNotes.Application.Interfaces;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;

        public NoteService(INoteRepository noteRepository, ITagRepository tagRepository)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
        }

        public async Task AddAsync(CreateNoteDto entity, CancellationToken cancellationToken = default)
        {

            List<Tag> tags = new List<Tag>();

            var existsTag = await _tagRepository.GetAllAsync(cancellationToken);

            foreach (string tagItem in entity.Tags)
            {
                Tag tag = existsTag.FirstOrDefault(t => t.Name == tagItem) ?? new Tag { Name = tagItem };
                if (!tags.Any(t => t.Name == tag.Name))
                {
                    tags.Add(tag);
                }
            }

            var note = new Note
            {
                Name = entity.Name,
                Description = entity.Description,
                Tags = tags,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };

            await _noteRepository.AddAsync(note, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var note = await _noteRepository.GetByIdAsync(id, cancellationToken);
            if (note == null)
            {
                throw new KeyNotFoundException($"Note with Id {id} not found");
            }

            await _noteRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<NoteDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var notes = await _noteRepository.GetAllAsync(cancellationToken);

            var noteDtos = new List<NoteDto>();

            foreach (var note in notes)
            {
                noteDtos.Add(MapToNoteDto(note));
            }

            return noteDtos;
        }

        public async Task<NoteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var note = await _noteRepository.GetByIdAsync(id, cancellationToken);

            if (note == null)
            {
                throw new KeyNotFoundException($"Note with Id {id} not found");
            }

            return MapToNoteDto(note);
        }

        public async Task UpdateAsync(Guid id, CreateNoteDto entity, CancellationToken cancellationToken = default)
        {
            var note = await _noteRepository.GetByIdAsync(id, cancellationToken);
            if (note == null)
            {
                throw new KeyNotFoundException($"Note with Id {id} not found");
            }

            List<Tag> tags = new List<Tag>();

            var existsTag = await _tagRepository.GetAllAsync(cancellationToken);

            foreach (string tagItem in entity.Tags)
            {
                Tag tag = existsTag.FirstOrDefault(t => t.Name == tagItem) ?? new Tag { Name = tagItem };
                if (!tags.Contains(tag))
                {
                    tags.Add(tag);
                }
            }

            note.Name = entity.Name;
            note.Description = entity.Description;
            note.Tags = tags;
            note.UpdateDate = DateTime.UtcNow;

            await _noteRepository.UpdateAsync(note, cancellationToken);
        }

        public NoteDto MapToNoteDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Name = note.Name,
                Description = note.Description,
                Tags = note.Tags.Select(t => t.Name).ToList(),
                CreationDate = note.CreationDate,
                UpdateDate = note.UpdateDate,
            };

        }
    }
}
