using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ClassMapper
{
    public interface IClassMapper
    {
        TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class;
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class;
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
            where TSource : class
            where TDestination : class;
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source, IEnumerable<TDestination> destination)
            where TSource : class
            where TDestination : class;
    }
}
