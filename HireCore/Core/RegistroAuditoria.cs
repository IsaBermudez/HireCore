namespace HireCore.Core
{
    public class RegistroAuditoria
    {
        public string EstadoAnterior { get; }
        public string EstadoNuevo { get; }
        public DateTime Fecha { get; }
        public string Usuario { get; }

        public RegistroAuditoria(string estadoAnterior, string estadoNuevo, DateTime fecha, string usuario)
        {
            EstadoAnterior = estadoAnterior;
            EstadoNuevo = estadoNuevo;
            Fecha = fecha;
            Usuario = usuario;
        }
    }

}
