using Distri.Shared.DTOs;

namespace Distri.Backend.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page -1) * pagination.RecordNumber)
                .Take(pagination.RecordNumber);
        }
    }
}
