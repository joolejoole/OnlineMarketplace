using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJo.Service
{
    public static class ToQuery
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return  new[] { instance }.AsQueryable();
        }
    }
}
