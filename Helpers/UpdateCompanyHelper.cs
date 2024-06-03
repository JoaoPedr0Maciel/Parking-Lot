using Estacionamento.Models;
using Estacionamento.ModelView;

namespace Estacionamento.Helpers
{
    public class UpdateCompanyHelper
    {
        public static void UpdateCompany(Company company, UpdateCompanyModel model) 
        {
            company.Name = model.Name ?? company.Name;
            company.Cnpj = model.Cnpj ?? company.Cnpj;
            company.Address = model.Address ?? company.Address;
            company.Phone = model.Phone ?? company.Phone;
            company.CarSpace = model.CarSpace ?? company.CarSpace;
            company.BikeSpace = model.BikeSpace ?? company.BikeSpace;
            company.CarHour = model.CarHour;
            company.BikeHour = model.BikeHour;
        }
    }
}