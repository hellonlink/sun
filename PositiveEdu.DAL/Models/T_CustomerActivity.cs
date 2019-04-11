using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 活动会员表
    /// </summary>
    public class T_CustomerActivity
    {
        /// <summary>
        /// 活动会员ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 活动表ID
        /// </summary>
        public int? T_ActivityId { get; set; }
        public virtual T_Activity T_Activity { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int? T_CustomerId { get; set; }
        public virtual T_Customer T_Customer { get; set; }
        /// <summary>
        /// 报名时间/导入时间
        /// </summary>
        public DateTime? RegistrationTime { get; set; }
        /// <summary>
        /// 是否参与成功
        /// </summary>
        public bool? IsSuccessTake { get; set; }
        /// <summary>
        /// 参与资格 1 有，0无
        /// </summary>
        public int? IsCanTake { get; set; }
        /// <summary>
        /// 所获奖项ID-   如果这个人没获奖就是0或者空
        /// </summary>
        public int? T_RewardId { get; set; }
        public virtual T_Reward T_Reward { get; set; }
        /// <summary>
        /// 获奖的时间- 
        /// </summary>
        public DateTime? RewardTime { get; set; }
        /// <summary>
        /// 实体奖品获取方式- 
        ///0自提
        ///1快递
        /// </summary>
        public int? EntityRewardGettype { get; set; }
        /// <summary>
        /// 实体奖品收件信息的省
        /// </summary>
        public string ReciptAddressProvince { get; set; }
        /// <summary>
        /// 实体奖品收件信息的市
        /// </summary>
        public string ReciptAddressCity { get; set; }
        /// <summary>
        /// 实体奖品收件信息的区
        /// </summary>
        public string ReciptAddressDistrict { get; set; }
        /// <summary>
        /// 实体奖品收件信息的详细地址
        /// </summary>
        public string ReciptDetailedAddress { get; set; }
        /// <summary>
        /// 实体奖品收件信息的收件人姓名
        /// </summary>
        public string ReciptName { get; set; }
        /// <summary>
        /// 实体奖品收件信息的收件人联系电话
        /// </summary>
        public string ReciptPhoneNum { get; set; }
        /// <summary>
        /// 实体奖品派件的快递公司
        /// </summary>
        public string CourierCompany { get; set; }
        /// <summary>
        /// 实体奖品派件的单号
        /// </summary>
        public string DeliveryNumber { get; set; }
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

        /// <summary>
        ///   扩展字段1
        /// </summary>
        public string Tag1 { get; set; }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string Tag2 { get; set; }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string Tag3 { get; set; }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string Tag4 { get; set; }
        /// <summary>
        /// 扩展字段5
        /// </summary>
        public string Tag5 { get; set; }
        /// <summary>
        /// 收件邮编
        /// </summary>
        public string AcceptAddressPostCode { get; set; }
    }
}