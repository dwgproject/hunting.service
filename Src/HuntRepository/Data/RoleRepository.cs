using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Configuration;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
{
    public class RoleRepository : IRoleRepository
    {

        private readonly HuntContext context;
         private readonly ILog log = LogManager.GetLogger(typeof(RoleRepository));

        public RoleRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }

        public Result<Role> Add(Role role)
        {
            var result = new Result<Role>(false, new Role());
            IDbContextTransaction tx = null;

            try{
                
                tx = context.Database.BeginTransaction();
                role.Identifier = Guid.NewGuid();
                context.Roles.Add(role);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Role>(true, role);
                log.Info($"Dodano role: {role}");
                return result;
            }catch(Exception ex){
                log.Error($"Nie udało się dodać roli:{role.Name}, {ex}");
                return result;
            }finally{
                    tx?.Dispose();   
                }
            
        }

        public void Delete(Guid identifier)
        {
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
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {tmpRole.Name},{ex} ");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }

        public Result<Role> Find(Guid identifier)
        {
            HuntContext context = null;
            try{
                var found = context.Roles.Find(identifier);
                return found != null ? 
                                new Result<Role>(true, found) : 
                                    new Result<Role>(false, null);

            }catch(Exception ex){
                return new Result<Role>(false, null);    
            }finally{
                context?.Dispose();
            }
        }

        public Result<IEnumerable<Role>> Query(Func<Role, bool> query)
        {
            Result<IEnumerable<Role>> result = new Result<IEnumerable<Role>>(false, new List<Role>());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                var resultQuery = context.Roles.Where(ux => query.Invoke(ux));                
                return new Result<IEnumerable<Role>>(true, resultQuery.AsEnumerable());
            }catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return result;
            }finally{
                tx?.Dispose();
            }
        }

        public void Update(Role role)
        {
            var tmpRole = context.Roles.Find(role.Identifier);
            if(tmpRole!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpRole.Name=role.Name;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano role:{role.Name} ");
                }
                catch(Exception ex){
                    log.Error($"Nie udało update usera:{role.Name}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}