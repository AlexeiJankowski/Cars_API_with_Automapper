using System.ComponentModel.DataAnnotations;

namespace BMW_API
{
    public class CreateCarDto
    {
        [Required]
        public string ModelSeries {get;set;}
        [Required]
        public string Years {get;set;}
        [Required]
        public string VehicleClass {get;set;}
    }
}