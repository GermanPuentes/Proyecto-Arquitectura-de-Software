using Subscriptor.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptor.Services
{
    public class EjecutarPagoService
    {
        public async Task ProcessAsync(PagoDeNominaRequest request)
        {
            Console.WriteLine($"📩Recibido: {request.NombreEmpleado} – Monto: {request.Sueldo:C}");
            Console.WriteLine("💰 Procesando Pago...");
            await Task.Delay(1500);
            Console.WriteLine($"✅ Pago para {request.NombreEmpleado} completado.\n");
        }
    }
}
