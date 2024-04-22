using System;

namespace AutoStrongDesk.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Text { get; set; }

        public byte[]? Image { get; set; }
    }
}
