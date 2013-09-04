using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BandManager.Models
{
    public class Profile
    {
        public virtual int ID { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string WebsiteUrl { get; set; }

        [DisplayName("Reverb Nation Artist ID")]
        public virtual string ReverbNationID { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Bio { get; set; }

        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
        
        public XDocument ToXml()
        {
            int showId = 0;
            int linkId = 0;
            int videoId = 0;

            XDocument xDoc = new XDocument(
                new XElement("PROFILE",
                    new XElement("NAME", Name),
                    new XElement("WEBSITEURL", WebsiteUrl),
                    new XElement("DESCRIPTION", Description),
                    new XElement("BIO", Bio),
                    new XElement("REVERBNATIONID", ReverbNationID),
                    new XElement("MEMBERS",
                        Members.OrderBy(m => m.Position).Select(m => new XElement("MEMBER",
                            new XElement("ID", m.ID),
                            new XElement("NAME", m.Name),
                            new XElement("INSTRUMENTS", m.Instruments),
                            new XElement("BIO", m.Bio),
                            new XElement("PHOTOURL", m.PhotoUrl),
                            new XElement("GEAR", m.Gear))
                        )
                    ),
                    new XElement("LINKS",
                        Links.OrderBy(l => l.Position).Select(l => new XElement("LINK",
                            new XElement("ID", l.ID = linkId++),
                            new XElement("NAME", l.Name),
                            new XElement("URL", l.Url))
                        )
                    ),
                    new XElement("VIDEOS",
                        Videos.OrderBy(v => v.Position).Select(v => new XElement("VIDEO",
                            new XElement("ID", v.ID = videoId++),
                            new XElement("NAME", v.Name),
                            new XElement("VIDEOHTML", v.VideoHtml))
                        )
                    ),
                    new XElement("SHOWS",
                        Shows.Select(s => new XElement("SHOW",
                            new XElement("ID", s.ID = showId++),
                            new XElement("VENUENAME", s.VenueName),
                            new XElement("VENUEWEBSITE", s.VenueWebsite),
                            new XElement("VENUELOCATION", s.VenueLocation),
                            new XElement("PHOTOURL", s.PhotoUrl),
                            new XElement("DATE", s.Date),
                            new XElement("DESCRIPTION", s.Description),
                            new XElement("MAPHTML", s.MapHtml))
                        )
                    )
                )
            );

            return xDoc;
        }
    }
}