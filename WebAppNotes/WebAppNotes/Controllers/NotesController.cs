using Microsoft.AspNetCore.Mvc;
using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.Interfaces;

namespace WebAppNotes.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var notes = await _noteService.GetAllAsync(cancellationToken);
            return Ok(notes);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var note = await _noteService.GetByIdAsync(id, cancellationToken);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteDto dto, CancellationToken cancellationToken = default)
        {
            await _noteService.AddAsync(dto, cancellationToken);
            return StatusCode(201);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CreateNoteDto dto, CancellationToken cancellationToken = default)
        {
            await _noteService.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _noteService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

    }
}
