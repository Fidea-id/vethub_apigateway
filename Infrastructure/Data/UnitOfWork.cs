using Domain.Entities;
using Domain.Entities.Models;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        // standard repository variable name
        // example :
        // private readonly TaskRepository _taskRepository
        // public ITaskRepository TaskRepository { get .....

        // add your repository here.
        // here just an example.
        private IUserRepository _UserRepositry;
        public IUserRepository UserRepository
        {
            get
            {
                if (_UserRepositry == null)
                {
                    _UserRepositry = new UserRepository(_context);
                }
                return _UserRepositry;
            }
        }


    }
}
