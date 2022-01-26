using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain.Models.InputModels
{
    public class BuildCarInputModelItem
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public CarSpecificationInputModel Specification { get; set; }
    }
}
