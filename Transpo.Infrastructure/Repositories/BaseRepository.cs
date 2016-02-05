using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;
using Transpo.Core.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected TranspoDbContext _context;
        public BaseRepository(TranspoDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public TEntity GetById(int id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            if (entity == null || entity.Active == false)
                return null;
            return entity;
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().Where(e => e.Active == true).ToList();
        }

        public void Delete(TEntity entity)
        {
            entity.Active = false;
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Edit(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
