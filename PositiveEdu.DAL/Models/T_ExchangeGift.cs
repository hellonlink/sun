using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    ///礼品兑换表
    /// </summary>


    public class T_ExchangeGifts
    {

        /// <summary>
        /// 兑换单ID
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int? T_GiftsId { get; set; }
        public T_Gifts T_Gifts { get; set; }
        /// <summary>
        /// 兑换会员ID
        /// </summary>
        public string T_CustomerId { get; set; }
        public T_Customer T_Customer { get; set; }
        /// <summary>
        /// 消费积分
        /// </summary>
        public int? UserIntegral { get; set; }
        /// <summary>
        /// 兑换类型- 
        ///0 虚拟礼品兑换
        ///1 实体礼品兑换
        /// </summary>
        public int? ExchangeType { get; set; }
        /// <summary>
        /// 提货方式 – 
        ///0自提
        ///1快递
        /// </summary>
        public int? PickType { get; set; }
        /// <summary>
        /// 收件省
        /// </summary>
        public int? AcceptAddressProvince { get; set; }
        /// <summary>
        /// 收件市
        /// </summary>
        public int? AcceptAddressCity { get; set; }
        /// <summary>
        /// 收件区
        /// </summary>
        public DateTime? AcceptAddressDistrict { get; set; }
        /// <summary>
        /// 收件详细地址
        /// </summary>
        public string AcceptAddressDetail { get; set; }
        /// <summary>
        /// 收件邮编
        /// </summary>
        public DateTime? AcceptPostNum { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string AcceptName { get; set; }
        /// <summary>
        /// 收件人联系电话
        /// </summary>
        public string AcceptPhoneNum { get; set; }
        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNum { get; set; }
        /// <summary>
        /// 兑换单状态-
        ///0 已完成
        ///1已作废
        ///2待自提
        ///3待发货
        /// 等等
        /// </summary>
        public int? CourierStatus { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime? ScrappedTime { get; set; }
        /// <summary>
        /// 自提/发货时间
        /// </summary>
        public DateTime? ObtainTime { get; set; }
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