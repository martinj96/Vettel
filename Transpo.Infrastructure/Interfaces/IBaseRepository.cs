using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.Infrastructure.Data.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        TEntity GetById(int id);
        List<TEntity> GetAll();
        void Delete(TEntity entity);
        void Edit(TEntity entity);
        void Save();
    }
}
