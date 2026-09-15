using HireCore.Core;
using HireCore.States.Interfaces;

namespace HireCore.Services
{
    public class GestorCandidato
    {
        private readonly Candidato _candidato;
        private readonly string _usuario;
        private readonly NotificadorEmail _notificadorEmail;
        private readonly Auditoria _auditoria;

        public GestorCandidato(Candidato candidato, string usuario, NotificadorEmail notificadorEmail, Auditoria auditoria)
        {
            _candidato = candidato;
            _usuario = usuario;
            _notificadorEmail = notificadorEmail;
            _auditoria = auditoria;
        }

        public void DelegarCambioEstado(EstadoProceso nuevoEstado)
        {
            try
            {
                if (nuevoEstado == null)
                {
                    throw new ArgumentNullException(nameof(nuevoEstado), "El nuevo estado no puede ser nulo o vacío.");
                }

                EstadoProceso estadoPrevio = _candidato.Estado;
                _candidato.Estado.CambiarEstado(_candidato, nuevoEstado);

                if (!ReferenceEquals(estadoPrevio, _candidato.Estado))
                {
                    DelegarRegistroAuditoria();
                    List<Notificacion> notificaciones = _candidato.Estado.ObtenerNotificaciones(_candidato);
                    _notificadorEmail.Notificar(notificaciones);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error de validación de entrada: {ex.Message}");
            }
        }

        public void RestaurarEstadoAnterior()
        {
            if (_candidato.EstadoAnterior == null)
            {
                return;
            }

            EstadoProceso estadoPrevio = _candidato.EstadoAnterior;
            string estadoActualNombre = _candidato.Estado.GetType().Name;

            _candidato.Estado = estadoPrevio;
            _candidato.EstadoAnterior = null;

            var registroDeshacer = new RegistroAuditoria(
                estadoActualNombre,
                estadoPrevio.GetType().Name,
                DateTime.Now,
                _usuario
            );

            _auditoria.RegistrarCambio(registroDeshacer);
        }

        public EstadoProceso ObtenerEstadoActual()
        {
            return _candidato.Estado;
        }

        public void DelegarRegistroAuditoria()
        {
            RegistroAuditoria registro = _candidato.Estado.ObtenerRegistroAuditoria(_candidato, _usuario);
            _auditoria.RegistrarCambio(registro);
        }

    }
}
