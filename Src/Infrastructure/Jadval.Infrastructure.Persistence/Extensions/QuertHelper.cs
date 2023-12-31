﻿using Jadval.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Jadval.Infrastructure.Persistence.Extensions
{
    internal static class QueryHelper
    {
        public static IQueryable<T> FilteredAndOrderedAndPaged<T>(this IQueryable<T> query, PagenationRequestParameter requestParameter, bool order = true, bool filter = true)
        {
            if (order && requestParameter.OrderParameters is not null)
                query = Ordered(query, requestParameter.OrderParameters);

            if (filter && requestParameter.FilterParameters is not null)
                query = Filtered(query, requestParameter.FilterParameters);

            query = query.Paged(requestParameter.PageNumber, requestParameter.PageSize);
            return query;

        }
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Ordered<T>(this IQueryable<T> query, List<PagenationOrderParameter> orderParameters)
        {
            foreach (var item in orderParameters)
                query = OrderByColumn(query, item.PropertyName, item.Descending);

            return query;
            IQueryable<T> OrderByColumn(IQueryable<T> collection, string propertyName, bool desk)
            {
                PropertyInfo prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == propertyName.ToLower().Trim());

                if (prop is null) return collection;

                ParameterExpression param = Expression.Parameter(typeof(T), "x");

                MemberExpression propertyExpression = Expression.Property(param, prop);

                LambdaExpression lambda = Expression.Lambda(propertyExpression, param);

                string methodName = desk ? "OrderByDescending" : "OrderBy";

                MethodCallExpression orderByExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), prop.PropertyType },
                    collection.Expression,
                    Expression.Quote(lambda)
                );

                return collection.Provider.CreateQuery<T>(orderByExpression);
            }
        }
        public static IQueryable<T> Filtered<T>(this IQueryable<T> query, List<PagenationFilterParameter> filterParameters)
        {
            foreach (var item in filterParameters)
                query = FilterByColumn(query, item.PropertyName, item.PropertyValue, item.Operation);

            return query;
            IQueryable<T> FilterByColumn(IQueryable<T> collection, string propertyName, string propertyValue, OperationTypes operation)
            {
                PropertyInfo prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == propertyName.ToLower().Trim());

                if (prop is null) return collection;

                var queryString = GetQueryString();

                if (!string.IsNullOrEmpty(queryString))
                    collection = collection.Where(queryString);

                return collection;

                string GetQueryString()
                {
                    try
                    {
                        var queries = new Dictionary<OperationTypes, string>();

                        if (prop.PropertyType.Name == nameof(String))
                        {
                            queries.Add(OperationTypes.Equal, $" == \"{propertyValue}\"");
                            queries.Add(OperationTypes.NotEqual, $" != \"{propertyValue}\"");
                            queries.Add(OperationTypes.Contains, $".Contains(\"{propertyValue}\")");
                            queries.Add(OperationTypes.StartWith, $".StartsWith(\"{propertyValue}\")");
                            queries.Add(OperationTypes.EndsWith, $".EndsWith(\"{propertyValue}\")");
                        }
                        else if (prop.PropertyType.Name == nameof(Boolean))
                        {
                            Convert.ToBoolean(propertyValue);
                            queries.Add(OperationTypes.Equal, $" == {propertyValue}");
                            queries.Add(OperationTypes.NotEqual, $" != {propertyValue}");
                        }
                        else if (prop.PropertyType.Name == nameof(DateTime))
                        {
                            Convert.ToDateTime(propertyValue);
                            queries.Add(OperationTypes.Equal, $" == Convert.ToDateTime(\"{propertyValue}\")");
                            queries.Add(OperationTypes.NotEqual, $" != Convert.ToDateTime(\"{propertyValue}\")");
                            queries.Add(OperationTypes.LessThan, $" < Convert.ToDateTime(\"{propertyValue}\")");
                            queries.Add(OperationTypes.LessThanOrEqual, $" <= Convert.ToDateTime(\"{propertyValue}\")");
                            queries.Add(OperationTypes.GreaterThan, $" > Convert.ToDateTime(\"{propertyValue}\")");
                            queries.Add(OperationTypes.GreaterThanOrEqual, $" >= Convert.ToDateTime(\"{propertyValue}\")");
                        }
                        else if (prop.PropertyType.Name.ToLower().StartsWith("int"))
                        {
                            Convert.ToInt64(propertyValue);
                            queries.Add(OperationTypes.Equal, $" == {propertyValue}");
                            queries.Add(OperationTypes.NotEqual, $" != {propertyValue}");
                            queries.Add(OperationTypes.LessThan, $" < {propertyValue}");
                            queries.Add(OperationTypes.LessThanOrEqual, $" <= {propertyValue}");
                            queries.Add(OperationTypes.GreaterThan, $" > {propertyValue}");
                            queries.Add(OperationTypes.GreaterThanOrEqual, $" >= {propertyValue}");
                        }

                        return queries.TryGetValue(operation, out string value) ? prop.Name + value : null;

                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

    }
}
