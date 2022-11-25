namespace Infrastructure.CrossCutting.Constants;

public static class MessageValidatorConst
{
    public const string NotNullValidator = "{PropertyName} no debe estar vacío.";
    public const string NotEmptyValidator = "{PropertyName} no debería estar vacío.";

    public const string MaximumLengthValidator =
        "{PropertyName} debe ser menor o igual que {MaxLength} caracteres. Ingresó {TotalLength} caracter(es).";

    public const string LessThanValidator = "{PropertyName} debe ser menor que '{ComparisonValue}'.";
    public const string LessThanOrEqualValidator = "{PropertyName} debe ser menor o igual que '{ComparisonValue}'.";
    public const string GreaterThanValidator = "{PropertyName} debe ser mayor que '{ComparisonValue}'.";

    public const string GreaterThanOrEqualValidator =
        "{PropertyName} debe ser mayor o igual que '{ComparisonValue}'.";

    public const string RegularExpressionValidator = "{PropertyName} no tiene el formato correcto.";
}