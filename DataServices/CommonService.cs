using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DataAccess;
using System.Linq;

namespace DataServices
{
    public class CommonService<T> where T : class
    {
        private readonly DataContext _context;
        public CommonService(DataContext dbContext)
        {
            _context = dbContext;
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetByFilter(Func<T, bool> func)
        {
            return _context.Set<T>().Where(func).ToList();
        }
    }
}
