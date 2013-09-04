using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BandManager.Models;
using System.Xml.Linq;

namespace BandManager
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ShowDetails",
                "ShowDetails/{id}/{slug}",
                new { controller = "Website", action = "ShowDetails", id = UrlParameter.Optional, slug = string.Empty }
            );

            routes.MapRoute(
                "Default", // Route name
                "{action}/{id}", // URL with parameters
                new { controller = "Website", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            
        }

        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            string path = Server.MapPath("~/App_Data/Profile.xml");
            var xProfile = System.Xml.Linq.XDocument.Load(path).Root;
            int memberPosition = 0;
            int linkPosition = 0;
            int videoPosition = 0;

            var profile = new Profile
            {                
                Name = xProfile.Element("NAME").Value,
                WebsiteUrl = xProfile.Element("WEBSITEURL").Value,
                Description = xProfile.Element("DESCRIPTION").Value,
                Bio = xProfile.Element("BIO").Value,
                ReverbNationID = xProfile.Element("REVERBNATIONID").Value,
                Members = new List<Member>(),
                Links = new List<Link>(),
                Shows = new List<Show>(),
                Videos = new List<Video>()
            };

            if (xProfile.Element("MEMBERS") != null)
            {
                foreach (XElement xMember in xProfile.Element("MEMBERS").Elements("MEMBER"))
                {
                    Guid newId, id = Guid.NewGuid();
                    if (Guid.TryParse(xMember.Element("ID").Value, out newId))
                    {
                        id = newId;
                    }
                    profile.Members.Add(new Member
                    {
                        ID = id,
                        Position = memberPosition++,
                        Name = xMember.Element("NAME").Value,
                        Bio = xMember.Element("BIO").Value,
                        Instruments = xMember.Element("INSTRUMENTS").Value,
                        PhotoUrl = xMember.Element("PHOTOURL").Value,
                        Gear = xMember.Element("GEAR").Value
                    });
                }
            }

            if (xProfile.Element("SHOWS") != null)
            {
                foreach (XElement xMember in xProfile.Element("SHOWS").Elements("SHOW"))
                {
                    profile.Shows.Add(new Show
                    {
                        ID = Convert.ToInt32(xMember.Element("ID").Value),
                        VenueName = xMember.Element("VENUENAME").Value,
                        VenueWebsite = xMember.Element("VENUEWEBSITE").Value,
                        VenueLocation = xMember.Element("VENUELOCATION").Value,
                        PhotoUrl = xMember.Element("PHOTOURL").Value,
                        Date = DateTime.Parse(xMember.Element("DATE").Value),
                        Description = xMember.Element("DESCRIPTION").Value,
                        MapHtml = xMember.Element("MAPHTML").Value
                    });
                }
            }

            if (xProfile.Element("LINKS") != null)
            {
                foreach (XElement xMember in xProfile.Element("LINKS").Elements("LINK"))
                {
                    profile.Links.Add(new Link
                    {
                        ID = Convert.ToInt32(xMember.Element("ID").Value),
                        Position = linkPosition++,
                        Name = xMember.Element("NAME").Value,
                        Url = xMember.Element("URL").Value
                    });
                }
            }

            if (xProfile.Element("VIDEOS") != null)
            {
                foreach (XElement xMember in xProfile.Element("VIDEOS").Elements("VIDEO"))
                {
                    profile.Videos.Add(new Video
                    {
                        ID = Convert.ToInt32(xMember.Element("ID").Value),
                        Position = videoPosition++,
                        Name = xMember.Element("NAME").Value,
                        VideoHtml = xMember.Element("VIDEOHTML").Value
                    });
                }
            }

            Session["currentProfile"] = profile;
        }

    }
}