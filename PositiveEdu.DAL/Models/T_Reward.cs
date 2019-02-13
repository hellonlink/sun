using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 活动奖项表
    /// </summary>
    [Table("T_Reward")]

    public class T_Reward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Reward()
        {
       
            //奖项字表，奖的类型 对应 礼品的类型
            T_RewardChild = new HashSet<T_RewardChild>();
            // 所获奖项ID-   如果这个人没获奖就是0或者空
            T_CustomerActivity = new HashSet<T_CustomerActivity>();

        }
        /// <summary>
        /// 活动奖项表ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 活动ID-此奖项对应哪个活动
        /// </summary>
        public int? T_ActivityId { get; set; }
        /// <summary>
        /// 一对多，一个活动对应多个奖励
        /// </summary>
        public virtual T_Activity T_Activity { get; set; }


        /// <summary>
        /// 礼品主表, 活动奖项子表  多对多
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RewardChild> T_RewardChild { get; set; } = new List<T_RewardChild>();
        /// <summary>
        /// 活动会员表,一对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CustomerActivity> T_CustomerActivity { get; set; } = new List<T_CustomerActivity>();



        /// <summary>
        /// 奖项名称-  类似一等奖 二等奖什么的，
        /// 一个活动可能会对应多个奖项
        /// ，如果活动是参与者人手一份的那种的话
        /// ，这里只要一个奖项就行了。
        /// </summary>
        public string RewardName { get; set; }
        /// <summary>
        /// 奖项份数-  就是允许获取此等奖项的人数，例如二等奖 10人之类的
        /// </summary>
        public int? RewardNumber { get; set; }
        /// <summary>
        /// 奖项剩余份数-  就是此奖项还有多少份没送出去
        /// </summary>
        public int? RewardRemaining { get; set; }
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