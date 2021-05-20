using System;
using System.Collections.Generic;

namespace DataServices
{
    public interface ICommonService<T> where T : class
    {
        List<T> Get();
        List<T> GetByFilter(Func<T, bool> func);
    }
}