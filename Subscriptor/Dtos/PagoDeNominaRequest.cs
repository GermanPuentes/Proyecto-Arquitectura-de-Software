using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptor.Dtos
{
    public class PagoDeNominaRequest
    {
        public string NombreEmpleado { get; set; }
        public decimal Sueldo { get; set; }
    }
}
