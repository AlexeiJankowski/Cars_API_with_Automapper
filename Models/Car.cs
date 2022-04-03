using System.ComponentModel.DataAnnotations;

namespace BMW_API
{
    public class Car
    {
        public int Id {get;set;}
        [Required]
        public string ModelSeries {get;set;}
        [Required]
        public string Years {get;set;}
        [Required]
        public string VehicleClass {get;set;}
    }
}