using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.ClassMapper;

namespace ZEDFramework.Collection.Infrastructure.AutoMapper
{
    public class ClassMapper : IClassMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class
        {
            Mapper.Initialize(x => x.CreateMap<TSource, TDestination>());
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            Mapper.Initialize(x => x.CreateMap<TSource, TDestination>());
            return Mapper.Map<TSource, TDestination>(source, destination);
        }

        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
            where TSource : class
            where TDestination : class
        {
            Mapper.Initialize(x => x.CreateMap<TSource, TDestination>());
            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source, IEnumerable<TDestination> destination)
            where TSource : class
            where TDestination : class
        {
            Mapper.Initialize(x => x.CreateMap<TSource, TDestination>());
            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source, destination);
        }
    }
}
