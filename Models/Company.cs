
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Estacionamento.Migrations;

namespace Estacionamento.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cnpj { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int CarSpace { get; set; }
        public int BikeSpace { get; set; }

        public decimal CarHour { get; set; }
        public decimal BikeHour { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Vehicles>? Vehicles { get; set; }
    }
}