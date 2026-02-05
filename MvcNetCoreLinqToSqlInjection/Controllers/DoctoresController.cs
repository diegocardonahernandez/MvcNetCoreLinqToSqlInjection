using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctoresSQLServer repo;

        public DoctoresController(RepositoryDoctoresSQLServer repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Doctor doctor)
        {
            await this.repo.CreateDoctor(doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario,doctor.IdHospital);
            return RedirectToAction("index");
        }

    }
}
