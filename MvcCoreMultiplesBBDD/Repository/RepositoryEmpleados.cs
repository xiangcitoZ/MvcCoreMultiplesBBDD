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

        public Empleado FindEmpleado(int id)
        {
            var consulta = from datos in context.Empleados
                           where datos.IdEmpleado == id
                           select datos;
            return consulta.ToList().FirstOrDefault();
        }

        public async Task DeleteEmpleado(int id)
        {
            Empleado empleado = this.FindEmpleado(id);
            context.Empleados.Remove(empleado);
            await this.context.SaveChangesAsync();
        }

    }
}
