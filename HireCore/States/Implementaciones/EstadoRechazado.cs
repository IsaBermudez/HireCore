using HireCore.Core;
using HireCore.States.Interfaces;

namespace HireCore.States.Implementaciones
{
    public class EstadoRechazado : EstadoProceso
    {
        public void CambiarEstado(Candidato candidato, EstadoProceso nuevoEstado)
        {
        }

        public List<Notificacion> ObtenerNotificaciones(Candidato candidato)
        {
            var notificaciones = new List<Notificacion>();

            if (candidato.CorreosRoles.TryGetValue("Reclutador", out var correoReclutador))
            {
                notificaciones.Add(new Notificacion(correoReclutador, $"{candidato.Nombre} ha sido rechazado del proceso."));
            }

            if (candidato.CorreosRoles.TryGetValue("PortalCandidato", out var correoPortal))
            {
                notificaciones.Add(new Notificacion(correoPortal, $"Hola {candidato.Nombre}, lamentamos informarte que tu proceso no continuará."));
            }

            return notificaciones;
        }

        public RegistroAuditoria ObtenerRegistroAuditoria(Candidato candidato, string usuario)
        {
            return new RegistroAuditoria(candidato.EstadoAnterior?.GetType().Name, nameof(EstadoRechazado), DateTime.Now, usuario);
        }

    }
}