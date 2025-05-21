using CapaPresentacion.Models;

public class PermisoProyecto
{
    public int Id { get; set; }

    public string UsuarioId { get; set; }
    public virtual ApplicationUser Usuario { get; set; }

    public int ProyectoId { get; set; }
    public virtual Proyecto Proyecto { get; set; }

    public bool PuedeVer { get; set; }
    public bool PuedeEditar { get; set; }
    public bool PuedeModificar { get; set; }
}
