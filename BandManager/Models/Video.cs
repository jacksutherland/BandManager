using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Models
{
    public class Video
    {
        public virtual int ID { get; set; }

        public virtual int Position { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [DisplayName("Video Html")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public virtual string VideoHtml { get; set; }
    }
}