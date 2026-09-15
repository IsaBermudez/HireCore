using HireCore.Core;

namespace HireCore.Services
{
    public class Auditoria
    {
        public List<RegistroAuditoria> Historial { get; } = new List<RegistroAuditoria>();

        public void RegistrarCambio(RegistroAuditoria registro)
        {
            Historial.Add(registro);
        }

    }
}
