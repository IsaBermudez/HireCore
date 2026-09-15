using HireCore.Core;
using HireCore.States.Interfaces;

namespace HireCore.States.Implementaciones
{
    public class EstadoContratado : EstadoProceso
    {
        public void CambiarEstado(Candidato candidato, EstadoProceso nuevoEstado)
        {
        }

        public List<Notificacion> ObtenerNotificaciones(Candidato candidato)
        {
            var notificaciones = new List<Notificacion>();

            if (candidato.CorreosRoles.TryGetValue("Reclutador", out var correoReclutador))
            {
                notificaciones.Add(new Notificacion(correoReclutador, $"{candidato.Nombre} ha sido formalmente contratado."));
            }

            if (candidato.CorreosRoles.TryGetValue("GerenteContratacion", out var correoGerente))
            {
                notificaciones.Add(new Notificacion(correoGerente, $"{candidato.Nombre} ha sido contratado exitosamente."));
            }

            if (candidato.CorreosRoles.TryGetValue("Nomina", out var correoNomina))
            {
                notificaciones.Add(new Notificacion(correoNomina, $"Alta en nómina requerida para el nuevo ingreso: {candidato.Nombre}."));
            }

            if (candidato.CorreosRoles.TryGetValue("PortalCandidato", out var correoPortal))
            {
                notificaciones.Add(new Notificacion(correoPortal, $"¡Felicitaciones {candidato.Nombre}! Has sido contratado."));
            }

            return notificaciones;
        }

        public RegistroAuditoria ObtenerRegistroAuditoria(Candidato candidato, string usuario)
        {
            return new RegistroAuditoria(candidato.EstadoAnterior?.GetType().Name, nameof(EstadoContratado), DateTime.Now, usuario);
        }
    }

}
