using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using MvcNetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Immutable;
using System.Data;
using System.Numerics;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{

    #region STORED PROCEDURES
  //  CREATE OR REPLACE PROCEDURE SP_DELETE_DOCTOR
  //(p_iddoctor DOCTOR.DOCTOR_NO%type)
  //AS
  //BEGIN
  //  DELETE FROM DOCTOR WHERE DOCTOR_NO = P_iddoctor;
  //  commit;

  //END;
    #endregion
    public class RepositoryDoctoresOracle: IRepositoryDoctores
    {
        private DataTable tablaDoctor;
        private OracleConnection cn;
        private OracleCommand com;
        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=true;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            this.tablaDoctor = new DataTable();
            string sql = "select * from DOCTOR";
            OracleDataAdapter ad = new OracleDataAdapter(sql,this.cn);
            ad.Fill(this.tablaDoctor);
                
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable() select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
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
            string sql = "INSERT INTO DOCTOR VALUES(:idhospital,:id, :apellido,:especialidad,:salario)";
            OracleParameter pamidDoctor = new OracleParameter(":iddoctor", IdDoctor);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            OracleParameter pamEspe = new OracleParameter(":especialidad", especiaidad);
            OracleParameter pamSal = new OracleParameter(":salario", salario);
            OracleParameter pamidHospital = new OracleParameter(":idhospital", idhospital);
            this.com.Parameters.Add(pamidHospital);
            this.com.Parameters.Add(pamidDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspe);
            this.com.Parameters.Add(pamSal);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeletDoctorAsync(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            OracleParameter pamId = new OracleParameter(":p_iddoctor", idDoctor);
            this.com.Parameters.Add(pamId);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task EditarDoctor(int IdDoctor, int idhospital, string apellido, string especiaidad, int salario)
        {
            string sql = "SP_EDIT_DOCTOR";
            OracleParameter pamidDoctor = new OracleParameter(":id", IdDoctor);
            OracleParameter pamidHospital = new OracleParameter(":idhospital", idhospital);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            OracleParameter pamEspe = new OracleParameter(":especialidad", especiaidad);
            OracleParameter pamSal = new OracleParameter(":salario", salario);
            this.com.Parameters.Add(pamidDoctor);
            this.com.Parameters.Add(pamidHospital);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspe);
            this.com.Parameters.Add(pamSal);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

        }


              
       public Doctor GetDoctor(int id)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == id
                           select datos;

            var row = consulta.First();
            Doctor doctor = new Doctor
            {
                IdHospital = row.Field<int>("HOSPITAL_COD"),
                IdDoctor = row.Field<int>("DOCTOR_NO"),
                Apellido = row.Field<string>("APELLIDO"),
                Especialidad = row.Field<string>("ESPECIALIDAD"),
                Salario = row.Field<int>("SALARIO")

            };
            return doctor;

        }

        public List<Doctor> BuscarDoctores(string busqueda)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           where (datos.Field<string>("ESPECIALIDAD")).ToUpper().StartsWith(busqueda.ToUpper())
                           select datos;

            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
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
    }
    }

