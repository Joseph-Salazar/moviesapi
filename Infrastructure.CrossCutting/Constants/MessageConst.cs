namespace Infrastructure.CrossCutting.Constants;

public static class MessageConst
{
    public const string UserNameAlreadyExists = "El usuario ya se encuentra registrado.";
    public const string EmailAlreadyExists = "El correo ya se encuentra registrado.";
    public const string RoleAlreadyExists = "El role ya se encuentra registrado.";
    public const string NameAlreadyExists = "El nombre ya se encuentra registrado.";
    public const string InvalidLogin = "Las credenciales especificadas son incorrectas o el usuario no está activo.";
    public const string UnregisteredEmailAddress = "El correo electrónico ingresado no está registrado o el usuario no está activo.";
    public const string ProcessSuccessfullyCompleted = "Proceso completado correctamente.";
    public const string InvalidSelection = "El registro seleccionado no es válido.";
}