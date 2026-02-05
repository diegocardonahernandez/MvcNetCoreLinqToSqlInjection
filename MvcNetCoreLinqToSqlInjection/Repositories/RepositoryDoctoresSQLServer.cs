using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using MvcNetCoreLinqToSqlInjection.Models;
using System.Data;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tablDoctor;
        public RepositoryDoctoresSQLServer()
        {
            string connctionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connctionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
            string sql = "SELECT * FROM DOCTOR";
            SqlDataAdapter ad = new SqlDataAdapter(sql, this.cn);
            this.tablDoctor = new DataTable();
            ad.Fill(this.tablDoctor);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablDoctor.AsEnumerable() select datos;
            List<Doctor> doctores = new List<Doctor>();
                
                foreach(var row  in consulta)
            {
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD")

                };
                doctores.Add(doc);
            }
                return doctores;
        }

        public async Task CreateDoctor(int IdDoctor, string apellido, string especiaidad, int salario, int idhospital)
        {
            string sql = "INSERT INTO DOCTOR VALUES(@idhospital,@id, @apellido,@especialidad,@salario)";
            this.com.Parameters.AddWithValue("@idhospital", idhospital);
            this.com.Parameters.AddWithValue("@id", IdDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especiaidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

    }
}
