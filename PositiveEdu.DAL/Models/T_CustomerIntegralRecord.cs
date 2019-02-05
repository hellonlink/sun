using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    ///会员积分记录表
    /// </summary>
    [Table("T_CustomerIntegralRecord")]

    public class T_CustomerIntegralRecord
    {

        /// <summary>
        /// 积分记录ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string T_CustomerId { get; set; }
        public T_Customer T_Customer { get; set; }
        /// <summary>
        /// 积分变化值    
        /// 增积分就是正 
        /// 减积分就是负
        /// </summary>
        public string integralExchangeValue { get; set; }
        /// <summary>
        /// 此操作后的积分分值
        /// </summary>
        public int? AfterExchangeintegralValue { get; set; }
        /// <summary>
        /// 积分操作类型   
        ///0兑换消费积分、
        ///1消费获取积分、
        ///2手工增减积分。
        /// </summary>
        public int? ExchangeType { get; set; }
        /// <summary>
        /// 积分变更时间
        /// </summary>
        public DateTime? ExchangeTime { get; set; }
        /// <summary>
        /// 操作人  -   只有手工增减积分才会记录操作人
        /// </summary>
        public string ExchangeBy { get; set; }
        /// <summary>
        /// 操作原因  - 只有手工增减积分才会记录操作原因
        /// </summary>
        public string ExchangeReason { get; set; }
        /// <summary>
        /// 积分兑换单号 – 只有积分消费的时候 才会记录兑换单号
        /// </summary>
        public string ExchangecNum { get; set; }
        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// 更新人`
        /// </summary>
        public string UpdatedBy { get; set; }

    }
}