using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 活动奖项子表
    /// </summary>

 
    public class T_RewardChild
    {
        /// <summary>
        /// 动奖项子表ID
        /// </summary>
        [Key]
            public int  Id { get; set; }
        /// <summary>
        /// 礼品ID-就是对应的礼品表里面的礼品ID
        /// </summary>
            public int?  T_GiftsId { get; set; }
        /// <summary>
        /// 奖项ID-此礼品对应哪个奖项
        /// </summary>
            public int?  T_RewardId { get; set; }

        public virtual T_Reward T_Reward { get; set; }
        public virtual T_Gifts T_Gift { get; set; }


        /// <summary>
        /// 数量-就是此奖项包含此礼品多少份
        /// 举个例子就是  XXX活动的 2等奖 里面有 3张 XX券
        /// </summary>
            public int? GiftNumber { get; set; }
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
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}