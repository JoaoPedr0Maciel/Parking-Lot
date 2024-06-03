using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ModelView
{
    public class CreateCompanyModel
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal CarHour { get; set; }
        public decimal BikeHour { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas para carros deve ser pelo menos 1.")]
        public int CarSpace { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas para motocicletas deve ser pelo menos 1.")]
        public int BikeSpace { get; set; }
    }
}