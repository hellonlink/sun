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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Activity()
        {
            T_CustomerActivity = new HashSet<T_CustomerActivity>();
            T_Reward = new HashSet<T_Reward>();

        }
        /// <summary>
        /// 活动ID-
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 活动奖项表,一对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Reward> T_Reward { get; set; } = new List<T_Reward>();
        /// <summary>
        /// 会员主表,多对的多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CustomerActivity> T_CustomerActivity { get; set; } = new List<T_CustomerActivity>();
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
        public DateTime? ActivityStopTime { get; set; }
        /// <summary>
        /// 报名人数限制
        /// </summary>
        public int? RegistrationNumber { get; set; }
        /// <summary>
        /// 参与人数限制
        /// </summary>
        public int? JoinNumber { get; set; }
        /// <summary>
        /// 活动参与消耗积分
        /// </summary>
        public int? NeedIntegral { get; set; }


        /// <summary>
        /// 是否报名者
        /// 0都可
        /// 1否
        /// 参与
        /// </summary>
        public int? IsAllTake { get; set; }
        /// <summary>
        /// 交给前端开发人员使用的活动唯一标识
        /// </summary>
        public string GUID { get; set; }


        /// <summary>
        /// 是否每人都有相同的奖励
        /// 0 是
        /// 1否
        /// 
        /// </summary>
        public int? IsAllSameReward { get; set; }
        /// <summary>
        /// 活动是 1否
        ///         0有效
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