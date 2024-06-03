namespace Estacionamento.ModelView
{
    public class UpdateCompanyModel
    {
        public string? Name { get; set; }
        public string? Cnpj { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? CarSpace { get; set; }
        public int? BikeSpace { get; set; }
        public decimal CarHour { get; set; }
        public decimal BikeHour { get; set; }

        
    }
}