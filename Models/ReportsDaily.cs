using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Models
{
    public class ReportsDaily
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public decimal TotalAmount { get; set; }
        public int Entries { get; set; }
        public int Exits { get; set; }
    }
}