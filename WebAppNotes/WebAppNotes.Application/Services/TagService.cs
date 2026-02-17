using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.DTO.Response;
using WebAppNotes.Application.Interfaces;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task AddAsync(CreateTagDto entity, CancellationToken cancellationToken = default)
        {
            var existTags = await _tagRepository.GetAllAsync(cancellationToken);

            if (!existTags.Any(t => t.Name.Equals(entity.Name)))
            {
                Tag tag = new Tag
                {
                    Name = entity.Name
                };
                
                await _tagRepository.AddAsync(tag, cancellationToken);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with Id {id} not found");
            }

            await _tagRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<Tag> tags = await _tagRepository.GetAllAsync(cancellationToken);

            List<TagDto> tagDtos = new List<TagDto>();

            foreach (Tag tag in tags)
            {
                tagDtos.Add(MapToTagDto(tag));
            }

            return tagDtos;
        }

        public async Task<TagDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Tag? tag = await _tagRepository.GetByIdAsync(id, cancellationToken);

            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with Id {id} not found");
            }

            return MapToTagDto(tag);
        }

        public async Task UpdateAsync(Guid id, CreateTagDto entity, CancellationToken cancellationToken = default)
        {

            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with Id {id} not found");
            }

            tag.Name = entity.Name;

            await _tagRepository.UpdateAsync(tag, cancellationToken);
        }

        private TagDto MapToTagDto(Tag entity)
        {
            return new TagDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
