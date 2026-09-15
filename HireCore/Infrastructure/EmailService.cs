namespace HireCore.Infrastructure
{
    public class EmailService
    {
        public void Enviar(string destinatario, string mensaje)
        {
            Console.WriteLine($"[Email enviado a: {destinatario}] {mensaje}");
        }

    }
}
