using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    [Table("BmiRecords")]
    public class BmiRecord
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// BMI計測日時
        /// </summary>
        [Display(Name = "BMI計測日時")]
        public DateTime BmiDate { get; set; }

        /// <summary>
        /// 身長
        /// </summary>
        [Display(Name = "身長(m)")]
        public double Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [Display(Name = "体重(kg)")]
        public double Weight { get; set; }

        /// <summary>
        /// BMI
        /// </summary>
        [Display(Name = "BMI")]
        public double Bmi { get; set; }

        /// <summary>
        /// 記録ユーザー
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// ユーザーId
        /// </summary>
        [NotMapped]
        [DisplayName("ユーザーId")]
        [Required(ErrorMessage = "ユーザーIdを選択してください")]
        public Guid SelectUserId { get; set; }

    }
}
