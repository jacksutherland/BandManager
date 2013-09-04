using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Models
{
    public class Show
    {
        public virtual int ID { get; set; }

        public virtual Profile Profile { get; set; }

        [Required]
        [DisplayName("Venue or Show Name")]
        public virtual string VenueName { get; set; }

        [DisplayName("Venue Website")]
        public virtual string VenueWebsite { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Venue Address or Location")]
        public virtual string VenueLocation { get; set; }

        [DisplayName("Flier Photo")]
        public virtual string PhotoUrl { get; set; }

        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        [DisplayName("Google Map Html")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public virtual string MapHtml { get; set; }

        public void update(Show show)
        {
            VenueName = show.VenueName;
            VenueWebsite = show.VenueWebsite;
            VenueLocation = show.VenueLocation;
            if (!string.IsNullOrEmpty(show.PhotoUrl))
            {
                PhotoUrl = show.PhotoUrl;
            }
            Description = show.Description;
            Date = show.Date;
            MapHtml = show.MapHtml;
        }
    }
}