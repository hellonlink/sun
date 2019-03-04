using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    [Table("T_PrivateGiftsRecord")]

    public class T_PrivateGiftsRecord
    {
        /// <summary>
        /// 自主卷批量生成表主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        public int? Count { get; set; }
        /// <summary>
        /// 操作人名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time { get; set; }


    }
}