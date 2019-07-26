using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    [MetadataType(typeof(NewsMetadatas))]
    public partial class News
    {
        private class NewsMetadatas
        {
            [Required(ErrorMessage = "Title must be filledd")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Subtitle must be filled")]
            public string Content { get; set; }
            [Required]
            public DateTime CreatedAt { get; set; }
        }
    }
}