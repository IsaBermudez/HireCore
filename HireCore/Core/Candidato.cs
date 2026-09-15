using HireCore.States.Interfaces;

namespace HireCore.Core
{
    public class Candidato
    {
        public string Nombre { get; }
        public EstadoProceso Estado { get; set; }
        public EstadoProceso EstadoAnterior { get; set; }
        public Dictionary<string, string> CorreosRoles { get; }

        public Candidato(string nombre, EstadoProceso estadoInicial, Dictionary<string, string> correosRoles)
        {
            Nombre = nombre;
            Estado = estadoInicial;
            EstadoAnterior = null;
            CorreosRoles = correosRoles;
        }

    }
}
