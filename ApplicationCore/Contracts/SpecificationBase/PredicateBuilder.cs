using System.Linq.Expressions;
using System.Reflection;

namespace ApplicationCore.Contracts.SpecificationBase;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> Build<T>(string propertyName, string comparison, string value)
    {
        const string parameterName = "x";
        ParameterExpression parameter = Expression.Parameter(typeof(T), parameterName);
        if (propertyName.Contains("||"))
        {
            //List<Expression> orExpressions = new();
            //string[] props = propertyName.Split("||");
            //foreach (string prop in props)
            //{
            //    if (string.IsNullOrWhiteSpace(prop)) continue;

            //    Expression leftProp = prop.Split('.').Aggregate((Expression)parameter, Expression.Property);
            //    Expression exProp = MakeComparison(leftProp, comparison, value.Trim());
            //    orExpressions.Add(exProp);
            //}

            //Expression finalExpr = null;
            //foreach (Expression expr in orExpressions)
            //{
            //    finalExpr = finalExpr == null ? expr : Expression.Or(finalExpr, expr);
            //}

            //return Expression.Lambda<Func<T, bool>>(finalExpr, parameter);

            Expression<Func<T, bool>> buildCombine = BuildCombineOr<T>(propertyName, comparison, value);
            return buildCombine;
        }
        if (propertyName.Contains("_"))
        {
            Expression<Func<T, bool>> buildCombine = BuildCombineAnd<T>(propertyName, comparison, value);
            return buildCombine;
        }
        Expression left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        Expression body = MakeComparison(left, comparison, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static Expression<Func<T, bool>> BuildCombineOr<T>(string propertyName, string comparison, string value)
    {
        const string parameterName = "x";
        ParameterExpression parameter = Expression.Parameter(typeof(T), parameterName);

        List<Expression> orExpressions = new();
        string[] props = propertyName.Split("||");
        foreach (string prop in props)
        {
            if (string.IsNullOrWhiteSpace(prop)) continue;

            Expression leftProp = prop.Split('.').Aggregate((Expression)parameter, Expression.Property);
            Expression exProp = MakeComparison(leftProp, comparison, value.Trim());
            orExpressions.Add(exProp);
        }

        Expression finalExpr = null;
        foreach (Expression expr in orExpressions)
        {
            finalExpr = finalExpr == null ? expr : Expression.Or(finalExpr, expr);
        }

        return Expression.Lambda<Func<T, bool>>(finalExpr, parameter);
    }

    public static Expression<Func<T, bool>> BuildCombineAnd<T>(string propertyName, string comparison, string value)
    {
        const string parameterName = "x";
        ParameterExpression parameter = Expression.Parameter(typeof(T), parameterName);

        string[] props = propertyName.Split("_");
        string[] valuesCombine = value.Trim().Split("!@#$");

        List<Expression> orExpressions = new();

        foreach (string val in valuesCombine)
        {
            string[] values = val.Trim().Split("|&|");//30000,70000

            if (string.IsNullOrWhiteSpace(val)) continue;
            int index = 0;
            List<Expression> andExpressions = new();
            foreach (string prop in props)
            {
                Expression leftProp = prop.Split('.').Aggregate((Expression)parameter, Expression.Property);
                Expression exProp = MakeComparison(leftProp, "==", values[index]);

                andExpressions.Add(exProp);
                index++;
            }
            Expression combineExprAnd = null;
            foreach (Expression expr in andExpressions)
            {
                combineExprAnd = combineExprAnd == null ? expr : Expression.And(combineExprAnd, expr);
            }

            orExpressions.Add(combineExprAnd);
        }

        Expression finalExpr = null;
        foreach (Expression expr in orExpressions)
        {
            finalExpr = finalExpr == null ? expr : Expression.Or(finalExpr, expr);
        }

        return Expression.Lambda<Func<T, bool>>(finalExpr, parameter);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        ParameterExpression p = a.Parameters[0];

        SubstExpressionVisitor visitor = new();
        visitor.subst[b.Parameters[0]] = p;

        Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        ParameterExpression p = a.Parameters[0];

        SubstExpressionVisitor visitor = new();
        visitor.subst[b.Parameters[0]] = p;

        Expression body = Expression.Or(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    private static Expression MakeComparison(Expression left, string comparison, string value)
    {
        return comparison switch
        {
            "==" => MakeBinary(ExpressionType.Equal, left, value),
            "!=" => MakeBinary(ExpressionType.NotEqual, left, value),
            ">" => MakeBinary(ExpressionType.GreaterThan, left, value),
            ">=" => MakeBinary(ExpressionType.GreaterThanOrEqual, left, value),
            "<" => MakeBinary(ExpressionType.LessThan, left, value),
            "<=" => MakeBinary(ExpressionType.LessThanOrEqual, left, value),
            "Contains" or "StartsWith" or "EndsWith" =>
                Expression.Call(
                    MakeString(left),
                    comparison,
                    Type.EmptyTypes,
                    Expression.Constant(value, typeof(string))),
            "In" => MakeList(left, value.Split(',')),
            "InOther" => MakeList(left, value.Split("!@#$")),
            "ContainsAny" => MakeAny(left, value.Split(',', '-', '/', '|')),
            _ => throw new NotSupportedException($"Invalid comparison operator '{comparison}'.")
        };
    }

    private static Expression MakeAny(Expression left, string[] split)
    {
        // Create a list to store the expressions
        List<Expression> expressions = new();

        // Loop through the values array
        foreach (string value in split)
        {
            expressions.Add(Expression.Call(MakeString(left), "Contains",
                Type.EmptyTypes, Expression.Constant(value.Trim(), typeof(string))));
        }

        // Create an expression that combines all the expressions in the list with the OR operator
        Expression orExpression = expressions.Aggregate(Expression.Or);

        return orExpression;
    }

    private static Expression MakeList(Expression left, IEnumerable<string> codes)
    {
        List<object> objValues = codes.Cast<object>().ToList();
        Type type = typeof(List<object>);
        MethodInfo? methodInfo = type.GetMethod("Contains", new[] { typeof(object) });
        ConstantExpression list = Expression.Constant(objValues);
        UnaryExpression converted = Expression.Convert(left, typeof(object));
        MethodCallExpression body = Expression.Call(list, methodInfo, converted);
        return body;
    }

    private static Expression MakeString(Expression source)
    {
        return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
    }

    private static Expression MakeBinary(ExpressionType type, Expression left, string value)
    {
        object typedValue = value;
        if (left.Type != typeof(string))
        {
            if (string.IsNullOrEmpty(value))
            {
                typedValue = default!;
                if (Nullable.GetUnderlyingType(left.Type) is null)
                {
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                }
            }
            else
            {
                Type valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                typedValue = valueType.IsEnum ? Enum.Parse(valueType, value) :
                    valueType == typeof(Guid) ? Guid.Parse(value) :
                    Convert.ChangeType(value, valueType);
            }
        }

        ConstantExpression right = Expression.Constant(typedValue, left.Type);
        return Expression.MakeBinary(type, left, right);
    }

    public static void ApplySort(this IRootSpecification spec,
        string sort,
        string orderByDescendingMethodName,
        string orderByAscendingMethodName)
    {
        if (string.IsNullOrEmpty(sort))
        {
            return;
        }

        const string descendingSuffix = "Desc";

        bool descending = sort.EndsWith(descendingSuffix, StringComparison.Ordinal);
        string propertyName = sort[..1].ToUpperInvariant() +
                              sort.Substring(1, sort.Length - 1 - (descending ? descendingSuffix.Length : 0));

        Type? specificationType = spec.GetType().BaseType;
        Type? targetType = specificationType?.GenericTypeArguments.First();
        PropertyInfo property = targetType!.GetRuntimeProperty(propertyName) ??
                                throw new InvalidOperationException(
                                    $"Because the property {propertyName} does not exist it cannot be sorted.");

        ParameterExpression lambdaParamX = Expression.Parameter(targetType, "x");

        LambdaExpression propertyReturningExpression = Expression.Lambda(
            Expression.Convert(
                Expression.Property(lambdaParamX, property),
                typeof(object)),
            lambdaParamX);

        if (descending)
        {
            specificationType?.GetMethod(
                    orderByDescendingMethodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.Invoke(spec, new object[] { propertyReturningExpression });
        }
        else
        {
            specificationType?.GetMethod(
                    orderByAscendingMethodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.Invoke(spec, new object[] { propertyReturningExpression });
        }
    }

    private class SubstExpressionVisitor : ExpressionVisitor
    {
        public readonly Dictionary<Expression, Expression> subst = new();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return subst.TryGetValue(node, out Expression? newValue) ? newValue : node;
        }
    }
}