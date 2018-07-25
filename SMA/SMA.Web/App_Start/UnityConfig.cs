using System;
using Microsoft.Practices.Unity;
using SMA.Entities.Models;
using SMA.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace SMA.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container
                .RegisterType<IDataContextAsync, SMAContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())

                .RegisterType<IRepositoryAsync<Assessment>, Repository<Assessment>>()
                .RegisterType<IAssessmentService, AssessmentService>()

                .RegisterType<IRepositoryAsync<Attendance>, Repository<Attendance>>()
                .RegisterType<IAttendanceService, AttendanceService>()

                .RegisterType<IRepositoryAsync<Class>, Repository<Class>>()
                .RegisterType<IClassService, ClassService>()

                .RegisterType<IRepositoryAsync<ClassRoom>, Repository<ClassRoom>>()
                .RegisterType<IClassRoomService, ClassRoomService>()

                .RegisterType<IRepositoryAsync<Contact>, Repository<Contact>>()
                .RegisterType<IContactService, ContactService>()

                .RegisterType<IRepositoryAsync<Event>, Repository<Event>>()
                .RegisterType<IEventService, EventService>()

                .RegisterType<IRepositoryAsync<Exam>, Repository<Exam>>()
                .RegisterType<IExamService, ExamService>()

                .RegisterType<IRepositoryAsync<Feed>, Repository<Feed>>()
                .RegisterType<IFeedService, FeedService>()

                .RegisterType<IRepositoryAsync<FeedComment>, Repository<FeedComment>>()
                .RegisterType<IFeedCommentService, FeedCommentService>()

                .RegisterType<IRepositoryAsync<FeedType>, Repository<FeedType>>()
                .RegisterType<IFeedTypeService, FeedTypeService>()

                .RegisterType<IRepositoryAsync<Group>, Repository<Group>>()
                .RegisterType<IGroupService, GroupService>()

                .RegisterType<IRepositoryAsync<Holiday>, Repository<Holiday>>()
                .RegisterType<IHolidayService, HolidayService>()

                .RegisterType<IRepositoryAsync<Image>, Repository<Image>>()
                .RegisterType<IImageService, ImageService>()

                .RegisterType<IRepositoryAsync<Level>, Repository<Level>>()
                .RegisterType<ILevelService, LevelService>()

                .RegisterType<IRepositoryAsync<Mark>, Repository<Mark>>()
                .RegisterType<IMarkService, MarkService>()

                .RegisterType<IRepositoryAsync<Message>, Repository<Message>>()
                .RegisterType<IMessageService, MessageService>()

                .RegisterType<IRepositoryAsync<Notification>, Repository<Notification>>()
                .RegisterType<INotificationService, NotificationService>()

                .RegisterType<IRepositoryAsync<Parent>, Repository<Parent>>()
                .RegisterType<IParentService, ParentService>()

                .RegisterType<IRepositoryAsync<ParentHasStudent>, Repository<ParentHasStudent>>()
                .RegisterType<IParentHasStudentService, ParentHasStudentService>()

                .RegisterType<IRepositoryAsync<Punishment>, Repository<Punishment>>()
                .RegisterType<IPunishmentService, PunishmentService>()

                .RegisterType<IRepositoryAsync<PunishmentType>, Repository<PunishmentType>>()
                .RegisterType<IPunishmentTypeService, PunishmentTypeService>()

                .RegisterType<IRepositoryAsync<Schedule>, Repository<Schedule>>()
                .RegisterType<IScheduleService, ScheduleService>()

                .RegisterType<IRepositoryAsync<School>, Repository<School>>()
                .RegisterType<ISchoolService, SchoolService>()

                .RegisterType<IRepositoryAsync<SchoolHead>, Repository<SchoolHead>>()
                .RegisterType<ISchoolHeadService, SchoolHeadService>()

                .RegisterType<IRepositoryAsync<SchoolYear>, Repository<SchoolYear>>()
                .RegisterType<ISchoolYearService, SchoolYearService>()

                .RegisterType<IRepositoryAsync<Student>, Repository<Student>>()
                .RegisterType<IStudentService, StudentService>()

                .RegisterType<IRepositoryAsync<StudentComment>, Repository<StudentComment>>()
                .RegisterType<IStudentCommentService, StudentCommentService>()

                .RegisterType<IRepositoryAsync<Subject>, Repository<Subject>>()
                .RegisterType<ISubjectService, SubjectService>()

                .RegisterType<IRepositoryAsync<SubjectLevel>, Repository<SubjectLevel>>()
                .RegisterType<ISubjectLevelService, SubjectLevelService>()

                .RegisterType<IRepositoryAsync<Teacher>, Repository<Teacher>>()
                .RegisterType<ITeacherService, TeacherService>()

                .RegisterType<IRepositoryAsync<Term>, Repository<Term>>()
                .RegisterType<ITermService, TermService>()

                .RegisterType<IRepositoryAsync<Testimonial>, Repository<Testimonial>>()
                .RegisterType<ITestimonialService, TestimonialService>()

                .RegisterType<IRepositoryAsync<UserSMA>, Repository<UserSMA>>()
                .RegisterType<IUserSMAService, UserSMAService>();

        }
    }
}
