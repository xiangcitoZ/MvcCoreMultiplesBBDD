using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repository
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;

        }

        public List<Empleado> GetEmpelados()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return consulta.ToList();
        }

    }
}
