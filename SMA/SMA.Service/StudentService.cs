#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface IStudentService : IService<Student>
    {

        IQueryable<Student> GetStudentsByName(string Name);
        IQueryable<Student> GetStudentsWithClass();

    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class StudentService : Service<Student>, IStudentService
    {
        private readonly IRepositoryAsync<Student> _repository;


        public StudentService(IRepositoryAsync<Student> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IQueryable<Student> GetStudentsByName(string Name)
        {
            return _repository.Queryable().Where(s => s.FirstName == Name);
        }
        public IQueryable<Student> GetStudentsWithClass()
        {
            // return _repository.Queryable().Include(s => s.Class).Include(p => p.Image);
                                        
            return _repository.Queryable();
        }
    }
}