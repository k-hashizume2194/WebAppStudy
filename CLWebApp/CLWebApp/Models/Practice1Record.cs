using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    [Table("BmiRecords")]
    public class Practice1Record
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// BMI
        /// </summary>
        public DateTime Bmi { get; set; }
    }
}
