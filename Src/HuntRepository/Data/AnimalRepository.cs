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
    public class AnimalRepository : IAnimalRepository
    {

        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(AnimalRepository));

        public AnimalRepository(HuntContext _context)
        {
            context = _context;
            LoggerConfig.ReadConfiguration();
        }

        public Result<Animal> Add(Animal animal)
        {
            var result = new Result<Animal>(false, new Animal());
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                animal.Identifier = Guid.NewGuid();
                context.Animals.Add(animal);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Animal>(true, animal);
                log.Info($"Dodano zwierzyne {animal}");
                return result;
                
            }
            catch(Exception ex){
                log.Error($"Nie udało dodać sie zwierzyny {animal}, {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }           
        }

        public void Delete(Animal animal)
        {
            var tmpAnimal = context.Animals.Find(animal.Identifier);

            if(tmpAnimal!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Animals.Remove(tmpAnimal);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto zwierzyne: {animal}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało usunac się zwierzyny {animal}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }    
            }
        }

        public Result<Animal> Find(Animal animal)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Animal>> Query(Func<Animal, bool> query)
        {
            Result<IEnumerable<Animal>> result = new Result<IEnumerable<Animal>>(false, new List<Animal>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Animals.Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Animal>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {query}, {ex}");
                return result;
            }
            finally{              
                tx?.Dispose();
            }
        }

        public void Update(Animal animal)
        {
            var tmpAnimal = context.Animals.Find(animal.Identifier);
            if(tmpAnimal!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpAnimal.Name = animal.Name;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano zwierzyne {animal}");
                }
                catch(Exception ex){
                    log.Error($"NIe powiodła się aktualizacja {animal}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}