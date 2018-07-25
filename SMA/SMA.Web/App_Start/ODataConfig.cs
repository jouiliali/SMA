using System;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace SMA.Web
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Entities.Models.Contact>(typeof(Entities.Models.Contact).Name);

            //builder.EntitySet<Entities.Models.Group>(typeof(Entities.Models.Group).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.School>(typeof(Entities.Models.School).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.FeedComment>(typeof(Entities.Models.FeedComment).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.FeedType>(typeof(Entities.Models.FeedType).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Feed>("Feeds");
            //builder.EntitySet<Entities.Models.Event>(typeof(Entities.Models.Event).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Level>(typeof(Entities.Models.Level).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Subject>(typeof(Entities.Models.Subject).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.SubjectLevel>(typeof(Entities.Models.SubjectLevel).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Image>(typeof(Entities.Models.Image).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Class>(typeof(Entities.Models.Class).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Assessment>(typeof(Entities.Models.Assessment).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Attendance>(typeof(Entities.Models.Attendance).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.ClassRoom>(typeof(Entities.Models.ClassRoom).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Exam>(typeof(Entities.Models.Exam).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Holiday>(typeof(Entities.Models.Holiday).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Mark>(typeof(Entities.Models.Mark).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Message>(typeof(Entities.Models.Message).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Notification>(typeof(Entities.Models.Notification).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Parent>(typeof(Entities.Models.Parent).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.ParentHasStudent>(typeof(Entities.Models.ParentHasStudent).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Punishment>(typeof(Entities.Models.Punishment).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Schedule>(typeof(Entities.Models.Schedule).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.SchoolHead>(typeof(Entities.Models.SchoolHead).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.SchoolYear>(typeof(Entities.Models.SchoolYear).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Student>(typeof(Entities.Models.Student).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.StudentComment>(typeof(Entities.Models.StudentComment).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Teacher>(typeof(Entities.Models.Teacher).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Term>(typeof(Entities.Models.Term).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.Testimonial>(typeof(Entities.Models.Testimonial).Name).EntityType.HasKey(c => c.Id);
            //builder.EntitySet<Entities.Models.UserSMA>(typeof(Entities.Models.UserSMA).Name).EntityType.HasKey(c => c.Id);
            
            var model = builder.GetEdmModel();
            config.Routes.MapODataRoute("odata", "odata", model);
                
            config.EnableQuerySupport();
        }
    }
}