using System;
using System.Collections.Generic;

namespace DataServices
{
    public interface ICommonService<T> where T : class
    {
        List<T> Get();
        T GetById(int id);
        List<T> GetByFilter(Func<T, bool> func);
    }
}