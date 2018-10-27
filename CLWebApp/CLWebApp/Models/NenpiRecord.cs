using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLWebApp.Models
{
    [Table("t_nenpi")]
    public class NenpiRecord
    {
        [Key]
        public long Id { get; set; }
        public DateTime RefuelDate { get; set; }
        public double Mileage { get; set; }
        public double TripMileage { get; set; }
        public double FuelCost { get; set; }
    }
}
