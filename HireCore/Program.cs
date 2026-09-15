using HireCore.Core;
using HireCore.States.Implementaciones;
using HireCore.Infrastructure;
using HireCore.Services;

public class Program
{
    public static void Main(string[] args)
    {
        EmailService emailService = new EmailService();
        NotificadorEmail notificador = new NotificadorEmail(emailService);
        Auditoria auditoria = new Auditoria();

        var correosConfigurados = new Dictionary<string, string>
        {
            { "Reclutador", "reclutador@empresa.com" },
            { "GerenteContratacion", "gerente.tech@empresa.com" },
            { "Nomina", "nomina@empresa.com" },
            { "PortalCandidato", "candidato.juan@gmail.com" }
        };

        Candidato candidato = new Candidato("Juan Pérez", new EstadoAplicado(), correosConfigurados);
        GestorCandidato gestor = new GestorCandidato(candidato, "admin_rrhh", notificador, auditoria);

        Console.WriteLine($"=== ESTADO INICIAL: {gestor.ObtenerEstadoActual().GetType().Name} ===\n");

        // 1. Transición normal a Entrevista
        Console.WriteLine("--- 1. Avanzando a Entrevista ---");
        gestor.DelegarCambioEstado(new EstadoEntrevista());
        Console.WriteLine($"Estado actual: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 2. Transición a Prueba Técnica
        Console.WriteLine("--- 2. Avanzando a Prueba Técnica ---");
        gestor.DelegarCambioEstado(new EstadoPruebaTecnica());
        Console.WriteLine($"Estado actual: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 3. Simulación de error de RRHH: Rechazo accidental
        Console.WriteLine("--- 3. RRHH comete un error y rechaza al candidato ---");
        gestor.DelegarCambioEstado(new EstadoRechazado());
        Console.WriteLine($"Estado actual tras error: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 4. AQUÍ SE LLAMA RestaurarEstadoAnterior()
        // RRHH detecta que el rechazo fue accidental y requiere revertir la acción
        Console.WriteLine("--- 4. Se detecta el error: Se invoca RestaurarEstadoAnterior() ---");
        gestor.RestaurarEstadoAnterior();
        Console.WriteLine($"Estado restaurado con éxito: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 5. El proceso continúa legítimamente hacia Oferta
        Console.WriteLine("--- 5. El candidato continúa su proceso y pasa a Oferta ---");
        gestor.DelegarCambioEstado(new EstadoOferta());
        Console.WriteLine($"Estado actual: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 6. Prueba de validación de entrada nula
        Console.WriteLine("--- 6. Intento de enviar un estado nulo/vacío ---");
        gestor.DelegarCambioEstado(null);
        Console.WriteLine($"Estado actual sin alteraciones: {gestor.ObtenerEstadoActual().GetType().Name}\n");

        // 7. Verificación del historial de auditoría
        Console.WriteLine("=== HISTORIAL COMPLETO DE AUDITORÍA ===");
        foreach (var registro in auditoria.Historial)
        {
            Console.WriteLine($"[{registro.Fecha:HH:mm:ss}] Usuario: {registro.Usuario,-12} | Transición: {registro.EstadoAnterior,-22} -> {registro.EstadoNuevo}");
        }
    }
}