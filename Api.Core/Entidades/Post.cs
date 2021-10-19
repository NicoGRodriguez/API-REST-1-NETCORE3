using System;

namespace Api.Core.Entidades
{
    public class Post
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string Descripcion { get; set; }

        public string Image { get; set; }
    }
}
