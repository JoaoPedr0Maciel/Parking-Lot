using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Models
{
    public class Movements
    {
        public int Id { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime? ExitTime { get; set; }
        public string? MovementType { get; set; }

        public decimal Amount { get; set; }
        public int VehicleId { get; set; }

    }
}