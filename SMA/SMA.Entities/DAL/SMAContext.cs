using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SMA.Entities.Models;
using Repository.Pattern.Ef6;

namespace SMA.Entities.Models
{
    public partial class SMAContext : DataContext
    {
        static SMAContext()
        {
            Database.SetInitializer<SMAContext>(new SMADBInitializer());
        }

        public SMAContext()
            : base("Name=SMAContext")
        {
        }        

        public DbSet<Contact> Contacts { get; set; }


        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FeedComment> FeedComments { get; set; }
        public DbSet<FeedType> FeedTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupeRole> GroupeRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<ParentHasStudent> ParentHasStudents { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<PunishmentType> PunishmentTypes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolHead> SchoolHeads { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentComment> StudentComments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectLevel> SubjectLevels { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<UserSMA> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSMA>()
               .Map(m => m.ToTable("Users"))
               .Map<Teacher>(m => m.ToTable("Teachers"))
               .Map<Parent>(m => m.ToTable("Parents"))
               .Map<SchoolHead>(m => m.ToTable("SchoolHeads"));

            modelBuilder.Entity<Feed>()
              .Map(m => m.ToTable("Feeds"))
              .Map<Event>(m => m.ToTable("Events"));

            
               
        }
    }
}

