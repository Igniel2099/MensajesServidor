
namespace MensajesServidor;

public enum EnumOrigen
{ 
    Login, 
    CrearCuenta, 
    OlvidarInformacion,
    RecuperarDatos
}

public enum EnumTipoRespuesta 
{ 
    Comprobar, 
    Guardar, 
    Enviar,
    Recuperar
}
public enum EnumRespuesta 
{ 
    Existente, 
    NoExistente, 
    Error, 
    Guardado, 
    Enviado,
    Recuperando
}

public enum EnumTipoValor 
{ 
    NombreUsuario, 
    CorreoElectronico, 
    Contraseña,
    CodigoConfirmacion 
}
