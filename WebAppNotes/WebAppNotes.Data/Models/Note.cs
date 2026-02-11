
namespace WebAppNotes.Data.Models
{
    public class Note : IEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Tag> Tags { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
