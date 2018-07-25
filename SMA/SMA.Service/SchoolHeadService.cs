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
    public interface ISchoolHeadService : IService<SchoolHead>
    {
       
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class SchoolHeadService : Service<SchoolHead>, ISchoolHeadService
    {
        private readonly IRepositoryAsync<SchoolHead> _repository;


        public SchoolHeadService(IRepositoryAsync<SchoolHead> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}