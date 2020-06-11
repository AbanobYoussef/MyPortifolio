using Core.Interfaces;
using Infrastucture.Respository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;
        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }


        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
