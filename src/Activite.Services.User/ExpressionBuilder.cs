using System;
using System.Linq.Expressions;

namespace Activite.Services.User;

public static class ExpressionBuilder
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        if (left is null) return right;
        if (right is null) return left;

        var parameter = left.Parameters[0];
        var replacedRight = new ParameterReplacer(right.Parameters[0], parameter).Visit(right.Body);
        var body = Expression.AndAlso(left.Body, replacedRight);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        if (left is null) return right;
        if (right is null) return left;

        var parameter = left.Parameters[0];
        var replacedRight = new ParameterReplacer(right.Parameters[0], parameter).Visit(right.Body);
        var body = Expression.OrElse(left.Body, replacedRight);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private sealed class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
}