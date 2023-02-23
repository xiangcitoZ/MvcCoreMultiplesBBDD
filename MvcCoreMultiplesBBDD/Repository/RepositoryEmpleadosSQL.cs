
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repository;

#region PROCEDURE
//CREATE PROCEDURE SP_DETAILS_EMPLEADO
//(@IDEMPLEADO int)
//AS
//	SELECT * FROM EMP
//	where EMP_NO = @IDEMPLEADO
//GO

#endregion

public class RepositoryEmpleadosSql : IRepositoryEmpleados
{
    private EmpleadosContext context;
    public RepositoryEmpleadosSql(EmpleadosContext context)
    {
        this.context = context;
    }
    public List<Empleado> GetEmpleados()
    {
        string sql = "SP_ALL_EMPLOYEES";
        var consulta = this.context.Empleados.FromSqlRaw(sql);
        List<Empleado> empleados = consulta.ToList();
        return empleados;
    }
    public Empleado FindEmpleado(int id)
    {
        string sql = "SP_DETAILS_EMPLEADO @IDEMPLEADO";
        SqlParameter pamid = new SqlParameter("@IDEMPLEADO", id);
        var consulta = this.context.Empleados.FromSqlRaw(sql, pamid);
        Empleado empleado = consulta.AsEnumerable().FirstOrDefault();
        return empleado;
    }
 

    public async Task UpdateEmpleado(int idempleado, int salario, string oficio)
    {
        Empleado empleado = this.FindEmpleado(idempleado);
        empleado.Salario = salario;
        empleado.Oficio = oficio;
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteEmpleado(int id)
    {
        Empleado empleado = this.FindEmpleado(id);
        this.context.Empleados.Remove(empleado);
        await this.context.SaveChangesAsync();
    }
}
