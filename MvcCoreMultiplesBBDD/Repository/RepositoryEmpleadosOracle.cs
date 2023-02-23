using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region
//create or replace procedure sp_delete_empleado
//(p_idempleado emp.emp_no%type)
//as
//begin
//    delete from emp where emp_no=p_idempleado;
//commit;
//end;


//create or replace procedure sp_update_empleado
//(p_idempleado emp.emp_no%type, p_oficio emp.oficio%type , p_salario emp.salario%type)
//as
//begin
//    update emp set salario = p_salario, oficio = p_oficio where emp_no=p_idempleado;
//commit;
//end;



//CREATE PROCEDURE SP__ALL_EMPLOYEES
//AS
//	SELECT * FROM EMP
//GO



//create or replace procedure sp_all_employees
//(p_cursor_empleados out sys_refcursor)
//as
//begin
//  open p_cursor_empleados for
//   select * from emp
//commit;
//end;


//create or replace procedure sp_details_empleado
//(p_cursor_empleado out sys_refcursor
//, p_idempleado emp.emp_no%type)
//as
//begin
//  open p_cursor_empleado for
//  select * from emp
//  where emp_no=p_idempleado;
//end;


#endregion


namespace MvcCoreMultiplesBBDD.Repository
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleadosOracle(EmpleadosContext context)
        {
            this.context = context;

        }

      

        public Empleado FindEmpleado(int id)
        {
            string sql = "begin ";
            sql += " sp_details_empleado(:p_cursor_empleados, :p_idempleado); ";
            sql += " end;";
            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = "p_cursor_empleados";
            pamcursor.Value = null;
            pamcursor.Direction = System.Data.ParameterDirection.Output;
            pamcursor.OracleDbType = OracleDbType.RefCursor;
            OracleParameter pamid = new OracleParameter("p_idempleado", id);
            var consulta =
                this.context.Empleados.FromSqlRaw(sql, pamcursor, pamid);
            Empleado empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;

        }



        public async Task DeleteEmpleado(int id)
        {
            //EN ORACLE, LOS PARAMETROS SE DENOMINAN
            //MEDIANTE :parametro
            string sql = "begin ";
            sql += " sp_delete_empleado(:p_idempleado); ";
            sql += " end;";
            OracleParameter pamid = new OracleParameter("p_idempleado", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamid);

        }


        public async Task UpdateEmpleado(int idempleado, 
            int salario, string oficio)
        {
            string sql = "begin ";
            sql += " sp_update_empleado(:p_idempleado, :p_oficio , :p_salario); ";
            sql += " end;";
            OracleParameter pamid = new OracleParameter("p_idempleado", idempleado);
           
            OracleParameter pamofi = new OracleParameter("p_oficio", oficio);

            OracleParameter pamsal = new OracleParameter("p_salario", salario);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamid, pamofi, pamsal);
        }

        public List<Empleado> GetEmpleados()
        {
            string sql = "begin ";
            sql += " sp_all_employees(:p_cursor_empleados); ";
            sql += " end;";
            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = "p_cursor_empleados";
            pamcursor.Value = null;
            pamcursor.Direction = System.Data.ParameterDirection.Output;
            pamcursor.OracleDbType = OracleDbType.RefCursor;
            var consulta =
                this.context.Empleados.FromSqlRaw(sql, pamcursor);
            List<Empleado> empleados = consulta.ToList();
            return empleados;
        }
    }
}
