using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{

    /// <summary>
    /// 勝率管理
    /// </summary>
    [Table("WinningRecords")]
    public class WinningRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 勝ち数
        /// </summary>
        public int Victory { get; set; }

        /// <summary>
        /// 負け数
        /// </summary>
        public int Defeat { get; set; }

        /// <summary>
        /// 引き分け数
        /// </summary>
        public int Draw { get; set; }

        /// <summary>
        /// 勝率
        /// </summary>
        public double WinningPercentage { get; set; }

    }
}
