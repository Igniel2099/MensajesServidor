﻿
namespace MensajesServidor;

public class Propiedad(EnumTipoValor tipoValor, string valor)
{
    public EnumTipoValor TipoValor { get; set; } = tipoValor;
    public string Valor { get; set; } = valor;
}

public class MensajesModuloLogin
{
    public EnumOrigen Origen { get; set; }
    public List<Propiedad> Propiedades { get; set; }
    public EnumTipoRespuesta TipoRespuesta { get; set; }
    public EnumRespuesta? Respuesta { get; set; }

    public MensajesModuloLogin(
        EnumOrigen origen,
        EnumTipoRespuesta tipoRespuesta,
        List<Propiedad> propiedades,
        EnumRespuesta? respuesta = null)
    {
        Origen = origen;
         TipoRespuesta = ValidarTipoRespuesta(tipoRespuesta);
        Propiedades = ValidarPropiedades(propiedades);
        Respuesta = respuesta.HasValue
            ? ValidarRespuesta(respuesta.Value)
            : null;
    }

    private EnumTipoRespuesta ValidarTipoRespuesta(EnumTipoRespuesta tipoRespuesta)
    {
        if (Origen == EnumOrigen.CrearCuenta &&
            tipoRespuesta != EnumTipoRespuesta.Comprobar &&
            tipoRespuesta != EnumTipoRespuesta.Guardar)
            throw new Exception("CrearCuenta solo permite Comprobar o Guardar.");

        if (Origen == EnumOrigen.Login &&
            tipoRespuesta != EnumTipoRespuesta.Comprobar)
            throw new Exception("Login solo permite Comprobar.");

        if (Origen == EnumOrigen.OlvidarInformacion &&
            tipoRespuesta != EnumTipoRespuesta.Enviar &&
            tipoRespuesta != EnumTipoRespuesta.Comprobar &&
            tipoRespuesta != EnumTipoRespuesta.Guardar)
            throw new Exception("OlvidarInformacion solo permite Comprobar, Enviar o Guardar.");

        return tipoRespuesta;
    }

    public EnumRespuesta ValidarRespuesta(EnumRespuesta respuesta)
    {
        if (respuesta == EnumRespuesta.Error)
            return respuesta;

        if (TipoRespuesta == EnumTipoRespuesta.Comprobar &&
            respuesta != EnumRespuesta.NoExistente &&
            respuesta != EnumRespuesta.Existente)
            throw new Exception("Si TipoRespuesta es Comprobar, respuesta debe ser Existente o NoExistente.");

        if (TipoRespuesta == EnumTipoRespuesta.Enviar &&
            respuesta != EnumRespuesta.Enviado)
            throw new Exception("Si TipoRespuesta es Enviar, respuesta debe ser Enviado.");

        if (TipoRespuesta == EnumTipoRespuesta.Guardar &&
            respuesta != EnumRespuesta.Guardado)
            throw new Exception("Si TipoRespuesta es Guardar, respuesta debe ser Guardado.");

        return respuesta;
    }

    private List<Propiedad> ValidarPropiedades(List<Propiedad> lista)
    {
        var tipos = lista.Select(p => p.TipoValor).ToHashSet();

        // Nueva regla: si recuperación exitosa, sólo NombreUsuario
        if (Origen == EnumOrigen.OlvidarInformacion && Respuesta == EnumRespuesta.Existente)
        {
            if (lista.Count == 1 && tipos.SetEquals(new[] { EnumTipoValor.NombreUsuario }))
                return lista;
            throw new Exception("OlvidarInformacion con respuesta Existente debe incluir solo NombreUsuario.");
        }

        // Guardar según origen
        if (TipoRespuesta == EnumTipoRespuesta.Guardar)
        {
            if (Origen == EnumOrigen.CrearCuenta)
            {
                var requeridos = new[]
                {
                    EnumTipoValor.NombreUsuario,
                    EnumTipoValor.CorreoElectronico,
                    EnumTipoValor.Contraseña
                };
                if (!tipos.SetEquals(requeridos))
                    throw new Exception("Guardar CrearCuenta requiere NombreUsuario, CorreoElectronico y Contraseña.");
            }
            else if (Origen == EnumOrigen.OlvidarInformacion)
            {
                var requeridos = new[]
                {
                    EnumTipoValor.NombreUsuario,
                    EnumTipoValor.Contraseña
                };
                if (!tipos.SetEquals(requeridos))
                    throw new Exception("Guardar OlvidarInformacion requiere NombreUsuario y Contraseña.");
            }
            else
            {
                throw new Exception("Guardar no está permitido para este Origen.");
            }
            return lista;
        }

        // Enviar solo CorreoElectronico
        if (TipoRespuesta == EnumTipoRespuesta.Enviar)
        {
            if (Origen != EnumOrigen.OlvidarInformacion || !tipos.SetEquals(new[] { EnumTipoValor.CorreoElectronico }))
                throw new Exception("Enviar OlvidarInformacion requiere CorreoElectronico.");
            return lista;
        }

        // Comprobar según origen
        if (TipoRespuesta == EnumTipoRespuesta.Comprobar)
        {
            switch (Origen)
            {
                case EnumOrigen.Login:
                    if (!tipos.SetEquals(new[] { EnumTipoValor.NombreUsuario, EnumTipoValor.Contraseña }))
                        throw new Exception("Comprobar Login requiere NombreUsuario y Contraseña.");
                    break;
                case EnumOrigen.CrearCuenta:
                    if (lista.Count != 1 || !(tipos.Contains(EnumTipoValor.NombreUsuario) || tipos.Contains(EnumTipoValor.CorreoElectronico)))
                        throw new Exception("Comprobar CrearCuenta requiere NombreUsuario o CorreoElectronico.");
                    break;
                case EnumOrigen.OlvidarInformacion:
                    var permitidos = new[] { EnumTipoValor.CorreoElectronico, EnumTipoValor.CodigoConfirmacion, EnumTipoValor.NombreUsuario };
                    if (lista.Count < 1 || lista.Count > 2 || !tipos.All(t => permitidos.Contains(t)))
                        throw new Exception("Comprobar OlvidarInformacion requiere una o dos propiedades: CorreoElectronico y/o CodigoConfirmacion.");
                    break;
                default:
                    throw new InvalidOperationException("Origen desconocido para Comprobar.");
            }
            return lista;
        }

        if (TipoRespuesta == EnumTipoRespuesta.Recuperar)
        {
            return lista;
        }

        throw new InvalidOperationException("TipoRespuesta no reconocido.");
    }
}
