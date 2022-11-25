using System.Linq.Expressions;

namespace Infrastructure.CrossCutting.TreeExpressions;

public static class TreeExpressionHelper
{
    public static Expression GetMemberAccessLambda<T>(ParameterExpression arg, string itemField) where T : class
    {
        var propertyList = itemField.Split('.');
        Expression expression = arg;

        var type = typeof(T);

        foreach (var name in propertyList)
        {
            var propertyInfo = type.GetProperty(name);
            expression = Expression.MakeMemberAccess(expression, propertyInfo);
            type = propertyInfo.PropertyType;
        }

        return expression;
    }

    public static LambdaExpression GetMemberAccessLambda<T>(string itemField) where T : class
    {
        var p = Expression.Parameter(typeof(T), "p");
        return Expression.Lambda(Expression.Property(p, typeof(T).GetProperty(itemField)), p);
    }

    public static Type GetPropertyType<T>(string itemField) where T : class
    {
        var propertyList = itemField.Split('.');
        var type = typeof(T);

        foreach (var name in propertyList)
        {
            var propertyInfo = type.GetProperty(name);
            type = propertyInfo.PropertyType;
        }

        return type;
    }
}