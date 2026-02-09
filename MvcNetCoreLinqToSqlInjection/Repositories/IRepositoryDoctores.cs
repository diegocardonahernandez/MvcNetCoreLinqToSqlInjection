using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        Task CreateDoctor(int IdDoctor, string apellido, string especiaidad, int salario, int idhospital);

        Task DeletDoctorAsync(int idDoctor);

        Task EditarDoctor(int IdDoctor, int idhospital, string apellido, string especiaidad, int salario);

        Doctor GetDoctor(int id);

        List<Doctor> BuscarDoctores(string busqueda);

    }
}
