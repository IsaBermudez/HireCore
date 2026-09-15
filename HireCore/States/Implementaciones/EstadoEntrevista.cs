using HireCore.Core;
using HireCore.States.Interfaces;

namespace HireCore.States.Implementaciones
{
    public class EstadoEntrevista : EstadoProceso
    {
        public void CambiarEstado(Candidato candidato, EstadoProceso nuevoEstado)
        {
            if (nuevoEstado is EstadoPruebaTecnica || nuevoEstado is EstadoRechazado)
            {
                candidato.EstadoAnterior = this;
                candidato.Estado = nuevoEstado;
            }
        }

        public List<Notificacion> ObtenerNotificaciones(Candidato candidato)
        {
            var notificaciones = new List<Notificacion>();

            if (candidato.CorreosRoles.TryGetValue("Reclutador", out var correoReclutador))
            {
                notificaciones.Add(new Notificacion(correoReclutador, $"{candidato.Nombre} avanzó a la etapa de Entrevista."));
            }

            if (candidato.CorreosRoles.TryGetValue("PortalCandidato", out var correoPortal))
            {
                notificaciones.Add(new Notificacion(correoPortal, $"Hola {candidato.Nombre}, has sido programado para una entrevista."));
            }

            return notificaciones;
        }

        public RegistroAuditoria ObtenerRegistroAuditoria(Candidato candidato, string usuario)
        {
            return new RegistroAuditoria(candidato.EstadoAnterior?.GetType().Name, nameof(EstadoEntrevista), DateTime.Now, usuario);
        }

    }
}
