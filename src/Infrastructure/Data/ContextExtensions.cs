using Microsoft.EntityFrameworkCore;
using System;

namespace Arbetsprov.Infrastructure.Data
{
    public static class ContextExtensions
    {
        /// <summary>
        /// Adds an AddOrUpdate function to DbContext.
        /// Source: https://www.michaelgmccarthy.com/2016/08/24/entity-framework-addorupdate-is-a-destructive-operation/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="entity">Entity to add</param>
        public static void AddOrUpdate<T>(this DbContext ctx, T entity)
            where T : class
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    // Item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
