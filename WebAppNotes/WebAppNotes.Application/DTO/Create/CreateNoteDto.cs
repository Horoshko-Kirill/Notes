using WebAppNotes.Data.Models;

namespace WebAppNotes.Application.DTO.Create
{
    public class CreateNoteDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
    }
}
