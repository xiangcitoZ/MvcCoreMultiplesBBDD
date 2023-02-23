using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repository
{
    public interface IRepositoryEmpleados
    {

        public List<Empleado> GetEmpleados();

        Empleado FindEmpleado(int id);
        Task UpdateEmpleado(int idempleado, int salario, string oficio);
        Task DeleteEmpleado(int id);

    }
}
