using HeyBanking.App.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PagedResult<TDestination>> PagedResultAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PagedResult<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }
}
