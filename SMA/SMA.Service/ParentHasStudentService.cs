#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface IParentHasStudentService : IService<ParentHasStudent>
    {
       
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class ParentHasStudentService : Service<ParentHasStudent>, IParentHasStudentService
    {
        private readonly IRepositoryAsync<ParentHasStudent> _repository;


        public ParentHasStudentService(IRepositoryAsync<ParentHasStudent> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}