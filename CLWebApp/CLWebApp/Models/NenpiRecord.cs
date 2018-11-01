using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    /// <summary>
    /// 燃費記録モデル
    /// </summary>
    [Table("NenpiRecords")]
    public class NenpiRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }
   
        /// <summary>
        /// 給油日付
        /// </summary>
        public DateTime RefuelDate { get; set; }
      
        /// <summary>
        /// 給油時総走行距離
        /// </summary>
        public double Mileage { get; set; }

        /// <summary>
        /// 区間走行距離
        /// </summary>
        public double TripMileage { get; set; }

        /// <summary>
        /// 区間燃費
        /// </summary>
        public double FuelCost { get; set; }

        /// <summary>
        /// 記録ユーザー
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
