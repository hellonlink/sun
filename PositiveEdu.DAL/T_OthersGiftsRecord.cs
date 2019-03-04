using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    [Table("T_OthersGiftsRecord")]

    public class T_OthersGiftsRecord
    {

        /// <summary>
        /// 第三方卷批量导入表主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 卷数量
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 操作人名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? Time { get; set; }
    }
}