using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DataAccess;
using System.Linq;

namespace DataServices
{
    public class CommonService<T> : ICommonService<T> where T : class
    {
        protected readonly DataContext context;
        public CommonService(DataContext dbContext)
        {
            context = dbContext;
        }

        public List<T> Get()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public List<T> GetByFilter(Func<T, bool> func)
        {
            return context.Set<T>().Where(func).ToList();
        }
    }
}
