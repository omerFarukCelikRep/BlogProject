using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class ArticleWithUserDetailDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReadTime { get; set; }
        public byte[] Image { get; set; }
        public int LikeCount { get; set; }
        public int ReadingCount { get; set; }
        public string WriterName { get; set; }
        public List<string> Topics { get; set; }
    }
}
