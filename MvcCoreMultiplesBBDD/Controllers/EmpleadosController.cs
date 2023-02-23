using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repository;

namespace MvcCoreMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpelados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteEmpleado(id);
            return RedirectToAction("Index");
        }

    }
}
