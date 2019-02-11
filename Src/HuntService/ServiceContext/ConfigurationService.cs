using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Extensions;
using Hunt.ServiceContext.Results;
using HuntRepository.Infrastructure;

namespace Hunt.ServiceContext{

    public class ConfigurationService : IConfigurationService
    {
        private readonly IRoleRepository roleRepository;
        public ConfigurationService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public Results.Result<string> AddRole(Role role)
        {
            var addResult = roleRepository.Add(role.ConvertToModel());
            return new Results.Result<string>(addResult.IsSuccess, addResult.IsSuccess ? string.Empty : "Error during adding a role.");
        }

        public Results.Result<string> DeleteRole(Guid identifier)
        {
            roleRepository.Delete(identifier);
            var found = roleRepository.Find(identifier);
            if (!found.IsSuccess)
                return new Results.Result<string>(true, "The role has been removed correctly.");
            return new Results.Result<string>(false, "The role has not been removed correctly.");
        }

        public Results.Result<IEnumerable<Role>> GetRoles()
        {
            var elements = roleRepository.Query(rx => { return true; });
            if (elements.IsSuccess){
                ICollection<Role> result = new Collection<Role>();
                foreach (var item in elements.Payload){
                    result.Add(new Role().ConvertToServiceRole(item));
                }
                return new Results.Result<IEnumerable<Role>>(true, result);
            }
            return new Results.Result<IEnumerable<Role>>(false, Enumerable.Empty<Role>());
        }
    }
}