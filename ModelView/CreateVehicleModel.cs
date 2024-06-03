using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ModelView
{
    public class CreateVehicleModel
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? Placa { get; set; }
        public string? Type { get; set; }
        public int CompanyId { get; set; }
    }
}