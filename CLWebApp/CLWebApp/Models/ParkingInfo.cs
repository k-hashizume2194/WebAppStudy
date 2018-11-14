using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{

    /// <summary>
    /// コインパーキング管理モデル
    /// </summary>
    [Table("ParkingInfos")]
    public class ParkingInfo
    {
        /// <summary>
        /// 駐車場ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 駐車場名
        /// </summary>
        public string ParkingName { get; set; }

        /// <summary>
        /// 値段1(分)
        /// </summary>
        public int TimeRate { get; set; }

        /// <summary>
        /// 値段1(円)
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// 値段2(最大料金)
        /// </summary>
        public int MaxFee { get; set; }

        /// <summary>
        /// 場所
        /// </summary>
        public string Location { get; set; }

    }
}
