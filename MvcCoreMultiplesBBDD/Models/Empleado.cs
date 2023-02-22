﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreMultiplesBBDD.Models
{
    [Table ("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }

        [Column("FECHA_ALT")]
        public DateTime FechaAlta { get; set; }

        [Column("COMISION")]
        public int Comision { get; set; }
        [Column("DEPT_NO")]
        public int IdDept { get; set; }

    }
}
