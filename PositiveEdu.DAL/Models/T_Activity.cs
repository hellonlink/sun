using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 活动主表
    /// </summary>
    [Table("T_Activity")]

    public class T_Activity
    {
        /// <summary>
        /// 活动ID-
        /// </summary>
        [Key]
        public int  Id { get; set; }

        /// <summary>
        /// 活动奖项表,一对多的关系
        /// </summary>
        public virtual ICollection<T_Reward> T_Reward { get; set; } = new List<T_Reward>();

        /// <summary>
        /// 活动名称-
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动报名开始时间
        /// </summary>
        public DateTime? RegistrationStartTime { get; set; }
        /// <summary>
        /// 活动报名截止时间
        /// </summary>
        public DateTime? RegistrationStopTime { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? ActivityStartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string ActivityStopTime { get; set; }
        /// <summary>
        /// 报名人数限制
        /// </summary>
        public int? RegistrationNumber { get; set; }
        /// <summary>
        /// 是否报名者都可参与
        /// </summary>
        public int? IsAllTake { get; set; }
        /// <summary>
        /// 是否每人都有相同的奖励
        /// </summary>
        public int? IsAllSameReward { get; set; }
        /// <summary>
        /// 活动是否有效
        /// </summary>
        public int? IsActivityEffective { get; set; }
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