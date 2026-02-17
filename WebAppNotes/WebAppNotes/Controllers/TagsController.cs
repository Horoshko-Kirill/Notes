using Microsoft.AspNetCore.Mvc;
using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.Interfaces;
using WebAppNotes.Application.Services;

namespace WebAppNotes.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var tags = await _tagService.GetAllAsync(cancellationToken);
            return Ok(tags);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _tagService.GetByIdAsync(id, cancellationToken);
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto dto, CancellationToken cancellationToken = default)
        {
            await _tagService.AddAsync(dto, cancellationToken);
            return StatusCode(201);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CreateTagDto dto, CancellationToken cancellationToken = default)
        {
            await _tagService.UpdateAsync(id, dto, cancellationToken);
            return NoContent();

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _tagService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
