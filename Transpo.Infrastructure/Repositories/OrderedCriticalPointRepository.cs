using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class OrderedCriticalPointRepository
    {
        protected TranspoDbContext _context;
        public OrderedCriticalPointRepository(TranspoDbContext context)
        {
            _context = context;
        }
        public void Add(OrderedCriticalPoint entity)
        {
            _context.CriticalPointsRides.Add(entity);
        }

        public OrderedCriticalPoint GetById(int id)
        {
            return _context.CriticalPointsRides.Find(id);
        }

        public List<OrderedCriticalPoint> GetAll()
        {
            return _context.CriticalPointsRides.ToList();
        }

        public void Delete(OrderedCriticalPoint entity)
        {
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Edit(OrderedCriticalPoint entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
