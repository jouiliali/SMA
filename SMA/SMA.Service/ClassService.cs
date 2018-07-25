#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

#endregion
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface IClassService : IService<Class>
    {
        IQueryable<Class> GetClassesByName(string Name);
        IQueryable<Class> GetClasses();
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class ClassService : Service<Class>, IClassService
    {
        private readonly IRepositoryAsync<Class> _repository;


        public ClassService(IRepositoryAsync<Class> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<Class> GetClassesByName(string Name)
        {
            return _repository.Queryable().Where(s => s.Libel == Name);
        }

        public IQueryable<Class> GetClasses()
        {
            return _repository.Queryable().Include(s => s.Schedules)
                                         
                                          ;
        }
    }
}