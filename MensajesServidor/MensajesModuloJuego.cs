
namespace MensajesServidor;

public enum EnumMensajeJuego
{
    MandarPersonaje,
    MandarEstado,
    MandarHabilidad
}

public class MensajesModuloJuego
{
    public EnumMensajeJuego TipoMensaje { get; set; }
    public string Valor { get; set; }
}
