using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
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
            await this.repo.CreateDoctor(doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.IdHospital);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> ELiminar(int id)
        {
            await this.repo.DeletDoctorAsync(id);
            return RedirectToAction("index");
        }

        public IActionResult Editar(int id)
        {
            Doctor doctor = this.repo.GetDoctor(id);
            return View(doctor);
        }

        [HttpPost]

        public async Task<IActionResult> Editar(Doctor doctor)
        {
            await this.repo.EditarDoctor(doctor.IdDoctor, doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return RedirectToAction("index");

        }

        public IActionResult Buscador()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]

        public IActionResult Buscador(string busqueda)
        {
            List<Doctor> doctores = this.repo.BuscarDoctores(busqueda);
            return View(doctores);
        }

    }
}
