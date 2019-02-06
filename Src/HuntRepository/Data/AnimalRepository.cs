using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
{
    public class AnimalRepository : IAnimalRepository
    {

        private readonly HuntContext context;

        public AnimalRepository(HuntContext _context)
        {
            context = _context;
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
                return result;
                //TODO: logger info
            }
            catch(Exception ex){
                //TODO: logger error
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
                    //TODO logger info
                }
                catch(Exception ex){
                    // TODO logger error
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
                //TODO logger error
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
                    //TODO logger info
                }
                catch(Exception ex){
                    //TOOD logger error
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}