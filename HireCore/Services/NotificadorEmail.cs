using HireCore.Core;
using HireCore.Infrastructure;

namespace HireCore.Services
{
    public class NotificadorEmail
    {
        private readonly EmailService _emailService;

        public NotificadorEmail(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void Notificar(List<Notificacion> notificaciones)
        {
            foreach (var notificacion in notificaciones)
            {
                _emailService.Enviar(notificacion.Destinatario, notificacion.Mensaje);
            }
        }

    }
}
