using System;
using System.Collections.Generic;
using System.Linq;
using GravityZero.HuntingSupport.Repository.Configuration;
using GravityZero.HuntingSupport.Repository.Extension;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;

namespace GravityZero.HuntingSupport.Repository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly HuntContext context;
         private readonly ILog log = LogManager.GetLogger(typeof(RoleRepository));
         private string TAG = "RR";

        public RoleRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }

        public RepositoryResult<Role> Add(Role role)
        {
            //var result = new RepositoryResult<Role>(false, new Role());
            IDbContextTransaction tx = null;
            try{              
                tx = context.Database.BeginTransaction();
                role.Identifier = Guid.NewGuid();
                context.Roles.Add(role);
                context.SaveChanges();
                tx.Commit();
                log.Info($"Dodano role: {role}");
                return new RepositoryResult<Role>(true, role, TAG+"01");
            }catch(Exception ex){
                log.Error($"Nie udało się dodać roli:{role.Name}, {ex}");
                return new RepositoryResult<Role>(false,null,TAG+"02");
            }finally{
                    tx?.Dispose();   
                }
            
        }

        public RepositoryResult<string> Delete(Guid identifier)
        {
            //var result = new RepositoryResult<string>(false, "",TAG);
            var tmpRole = context.Roles.Find(identifier);
            if(tmpRole!=null)
            {
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Roles.Remove(tmpRole);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto role: {identifier}");
                    return new RepositoryResult<string>(true,"",TAG+"05");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {tmpRole.Name},{ex} ");
                    return new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"06");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<string>(false,"",TAG+"11");
            }
        }

        public RepositoryResult<Role> Find(Guid identifier)
        {
            try{
                var found = context.Roles.Find(identifier);
                return found != null ? 
                                new RepositoryResult<Role>(true, found, TAG+"07") : 
                                    new RepositoryResult<Role>(false, null, TAG+"08");

            }catch(Exception ex){
                return new RepositoryResult<Role>(false, null);    
            }finally{
                
            }

        }

        public RepositoryResult<IEnumerable<Role>> Query(Func<Role, bool> query)
        {
            //RepositoryResult<IEnumerable<Role>> result = new RepositoryResult<IEnumerable<Role>>(false, new List<Role>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Roles.Where(ux => query.Invoke(ux));
                if (resultQuery.IsNotNullAndAny()){
                    return new RepositoryResult<IEnumerable<Role>>(true, resultQuery);
                }
                return new RepositoryResult<IEnumerable<Role>>(true, resultQuery.AsEnumerable(), TAG+"09");
            }catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return new RepositoryResult<IEnumerable<Role>>(false,null,TAG+"10");
            }finally{
                tx?.Dispose();
            }
        }

        public RepositoryResult<Role> Update(Role role)
        {
            //var result = new RepositoryResult<Role>(false, null,TAG);
            var tmpRole = context.Roles.Find(role.Identifier);
            if(tmpRole!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpRole.Name=role.Name;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano role:{role.Name} ");
                    return new RepositoryResult<Role>(true,tmpRole,TAG+"03");
                }
                catch(Exception ex){
                    log.Error($"Nie udało update usera:{role.Name}, {ex}");
                    return new RepositoryResult<Role>(false,null,TAG+"04");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<Role>(false,null, TAG+"12");
            }
        }
    }
}