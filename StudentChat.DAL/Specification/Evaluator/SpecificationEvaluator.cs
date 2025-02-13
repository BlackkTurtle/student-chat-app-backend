﻿using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StudentChat.DAL.Specification.Evaluator
{
    public class SpecificationEvaluator<TEntity>
        where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            IBaseSpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification == null)
            {
                return query;
            }

            if (specification.Predicate != null)
            {
                query = query.Where(specification.Predicate);
            }

            query = specification.Includes.Aggregate(
                query,
                (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip.Value)
                            .Take(specification.Take.Value);
            }

            return query;
        }
    }
}
