using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandManager.Models
{
    public class Link
    {
        public virtual int ID { get; set; }

        public virtual int Position { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Url { get; set; }
    }
}