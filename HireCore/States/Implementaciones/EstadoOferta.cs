using HireCore.Core;
using HireCore.States.Interfaces;

namespace HireCore.States.Implementaciones
{
    public class EstadoOferta : EstadoProceso
    {
        public void CambiarEstado(Candidato candidato, EstadoProceso nuevoEstado)
        {
            if (nuevoEstado is EstadoVerificacionDeReferencias || nuevoEstado is EstadoRechazado)
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
                notificaciones.Add(new Notificacion(correoReclutador, $"Oferta enviada a {candidato.Nombre}."));
            }

            if (candidato.CorreosRoles.TryGetValue("GerenteContratacion", out var correoGerente))
            {
                notificaciones.Add(new Notificacion(correoGerente, $"Se ha generado una oferta económica para el candidato {candidato.Nombre}."));
            }

            if (candidato.CorreosRoles.TryGetValue("PortalCandidato", out var correoPortal))
            {
                notificaciones.Add(new Notificacion(correoPortal, $"Hola {candidato.Nombre}, tienes una oferta formal pendiente de revisión."));
            }

            return notificaciones;
        }

        public RegistroAuditoria ObtenerRegistroAuditoria(Candidato candidato, string usuario)
        {
            return new RegistroAuditoria(candidato.EstadoAnterior?.GetType().Name, nameof(EstadoOferta), DateTime.Now, usuario);
        }

    }
}
