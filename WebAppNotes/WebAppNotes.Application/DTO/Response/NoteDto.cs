using WebAppNotes.Data.Models;

namespace WebAppNotes.Application.DTO.Response
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
