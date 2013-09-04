using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandManager.Models
{
    public class Member
    {
        public virtual Guid ID { get; set; }

        public virtual int Position { get; set; }
        
        public virtual Profile Profile { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Gear { get; set; }
        
        public virtual string Instruments { get; set; }

        [DisplayName("Photo")]
        public virtual string PhotoUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Bio { get; set; }

        public void update(Member member)
        {
            Name = member.Name;
            Bio = member.Bio;
            Instruments = member.Instruments;
            Gear = member.Gear;
            if (!string.IsNullOrEmpty(member.PhotoUrl))
            {
                PhotoUrl = member.PhotoUrl;
            }
        }
    }
}