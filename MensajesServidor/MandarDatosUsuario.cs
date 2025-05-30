
namespace MensajesServidor;

public class PersonajeDTO
{
    public int IdPersonaje { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string? Tipo { get; set; }
    public string? Grupo { get; set; }
    public int? Coste { get; set; }

    public PersonajeDTO(int idPersonaje, string nombreCompleto, string? tipo, string? grupo, int? coste)
    {
        IdPersonaje = idPersonaje;
        NombreCompleto = nombreCompleto;
        Tipo = tipo;
        Grupo = grupo;
        Coste = coste;
    }
}

public class HabilidadeDTO
{
    public int IdHabilidades { get; set; }
    public int? IdPersonaje { get; set; }
    public string? Nombre { get; set; }
    public int? Valor { get; set; }
    public string? Tipo { get; set; }

    public HabilidadeDTO(int idHabilidades, int? idPersonaje, string? nombre, int? valor, string? tipo)
    {
        IdHabilidades = idHabilidades;
        IdPersonaje = idPersonaje;
        Nombre = nombre;
        Valor = valor;
        Tipo = tipo;
    }
}

public class PersonajeUsuarioDTO
{
    public int IdPersonajeUsuario { get; set; }
    public int? IdPersonaje { get; set; }
    public int? Nivel { get; set; }
    public int? ValorHabilidad1 { get; set; }
    public int? ValorHabilidad2 { get; set; }
    public int? ValorHabilidad3 { get; set; }

    public PersonajeUsuarioDTO(int idPersonajeUsuario, int? idPersonaje, int? nivel, int? valorHabilidad1, int? valorHabilidad2, int? valorHabilidad3)
    {
        IdPersonajeUsuario = idPersonajeUsuario;
        IdPersonaje = idPersonaje;
        Nivel = nivel;
        ValorHabilidad1 = valorHabilidad1;
        ValorHabilidad2 = valorHabilidad2;
        ValorHabilidad3 = valorHabilidad3;
    }
}

public class EquipoDTO
{
    public int IdEquipo { get; set; }
    public int? IdPersonajeUsuario1 { get; set; }
    public int? IdPersonajeUsuario2 { get; set; }
    public int? IdPersonajeUsuario3 { get; set; }

    public EquipoDTO(int idEquipo, int? idPersonajeUsuario1, int? idPersonajeUsuario2, int? idPersonajeUsuario3)
    {
        IdEquipo = idEquipo;
        IdPersonajeUsuario1 = idPersonajeUsuario1;
        IdPersonajeUsuario2 = idPersonajeUsuario2;
        IdPersonajeUsuario3 = idPersonajeUsuario3;
    }
}

public class PeleaDTO
{
    public int IdPeleas { get; set; }
    public string? ContrincanteUsuario { get; set; }
    public bool SoyGanador { get; set; }

    public PeleaDTO(int idPeleas, string? contrincanteUsuario, bool soyGanador)
    {
        IdPeleas = idPeleas;
        ContrincanteUsuario = contrincanteUsuario;
        SoyGanador = soyGanador;
    }
}

public class MandarDatosUsuario
{
    // Información del usuario
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public int? Experiencia { get; set; }
    public int? Monedas { get; set; }
    public int? SuperPuntos { get; set; }

    // Equipos y personajes
    public List<PersonajeDTO> Personajes { get; set; } = [];
    public List<HabilidadeDTO> Habilidades { get; set; } = [];
    public List<PersonajeUsuarioDTO> PersonajeUsuarios { get; set; } = [];
    public EquipoDTO? Equipo { get; set; }
    public List<PeleaDTO> Peleas { get; set; } = [];

    public MandarDatosUsuario(
        int idUsuario, 
        string nombreUsuario, 
        string correo, 
        int? experiencia, 
        int? monedas, 
        int? superPuntos, 
        List<PersonajeDTO> personajes, 
        List<HabilidadeDTO> habilidades, 
        List<PersonajeUsuarioDTO> personajeUsuarios, 
        EquipoDTO? equipo, 
        List<PeleaDTO> peleas)
    {
        IdUsuario = idUsuario;
        NombreUsuario = nombreUsuario;
        Correo = correo;
        Experiencia = experiencia;
        Monedas = monedas;
        SuperPuntos = superPuntos;
        Personajes = personajes;
        Habilidades = habilidades;
        PersonajeUsuarios = personajeUsuarios;
        Equipo = equipo;
        Peleas = peleas;
    }
}
