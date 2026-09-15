namespace HireCore.Core
{
    public class Notificacion
    {
        public string Destinatario { get; }
        public string Mensaje { get; }

        public Notificacion(string destinatario, string mensaje)
        {
            Destinatario = destinatario;
            Mensaje = mensaje;
        }
    }

}
