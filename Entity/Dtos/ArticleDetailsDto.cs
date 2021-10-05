using System;

namespace Entity.Dtos
{
    public class ArticleDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReadTime { get; set; }
        public byte[] Image { get; set; }
        public int LikeCount { get; set; }
        public string WriterName { get; set; }
    }
}
