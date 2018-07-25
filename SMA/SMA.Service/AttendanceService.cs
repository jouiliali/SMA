#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System.Linq;
using System.Data.Entity;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface IAttendanceService : IService<Attendance>
    {
        IQueryable<Attendance> GetStudentsWithAttendance();
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class AttendanceService : Service<Attendance>, IAttendanceService
    {
        private readonly IRepositoryAsync<Attendance> _repository;


        public AttendanceService(IRepositoryAsync<Attendance> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<Attendance> GetStudentsWithAttendance()
        {
            return _repository.Queryable().Include(s => s.Student)

                ;
        }

    }
}