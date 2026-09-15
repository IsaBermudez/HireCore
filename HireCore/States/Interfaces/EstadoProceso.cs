using HireCore.Core;

namespace HireCore.States.Interfaces
{
    public interface EstadoProceso
    {
        void CambiarEstado(Candidato candidato, EstadoProceso nuevoEstado);
        List<Notificacion> ObtenerNotificaciones(Candidato candidato);
        RegistroAuditoria ObtenerRegistroAuditoria(Candidato candidato, string usuario);


    }
}
