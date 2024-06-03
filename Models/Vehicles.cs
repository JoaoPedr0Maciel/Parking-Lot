
namespace Estacionamento.Models
{
    public class Vehicles
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? Placa { get; set; }
        public string? Type { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}