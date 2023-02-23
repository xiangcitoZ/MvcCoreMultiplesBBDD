using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repository;

namespace MvcCoreMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryEmpleados repo;

        public EmpleadosController(IRepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
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

        public IActionResult EditEmpleado(int id)
        {
            Empleado empleado = repo.FindEmpleado(id);
            return View(empleado);
        }
        [HttpPost]

        public async Task<IActionResult> EditEmpleado(Empleado empleado)
        {
            await repo.UpdateEmpleado(empleado.IdEmpleado
                , empleado.Salario, empleado.Oficio);
            return RedirectToAction("Index");
        }


    }
}
