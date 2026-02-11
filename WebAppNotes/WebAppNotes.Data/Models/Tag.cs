using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppNotes.Data.Models
{
    public class Tag : IEntity
    {
        public string Name { get; set; } = null!;
    }
}
