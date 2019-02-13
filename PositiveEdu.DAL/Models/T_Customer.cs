using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 会员主表
    /// </summary>
    [Table("T_Customer")]

    public class T_Customer
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Customer()
        {
            //会员活动
            T_CustomerActivity = new HashSet<T_CustomerActivity>();
            //礼品兑换
            T_ExchangeGifts = new HashSet<T_ExchangeGifts>();
            //会员收件
            T_CustomerAccept = new HashSet<T_CustomerAccept>();
            //会员积分修改
            T_CustomerIntegralRecord = new HashSet<T_CustomerIntegralRecord>();

        }
        /// <summary>
        /// 会员ID-本系统自己的会员唯一ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 活动表， 活动会员表 多对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CustomerActivity> T_CustomerActivity { get; set; } = new List<T_CustomerActivity>();
        /// <summary>
        ///  礼品主表，礼品兑换表 多对多的关系 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ExchangeGifts> T_ExchangeGifts { get; set; } = new List<T_ExchangeGifts>();
        /// <summary>
        /// 会员收件信息表,一对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CustomerAccept> T_CustomerAccept { get; set; } = new List<T_CustomerAccept>();
        /// <summary>
        /// 会员积分记录表,一对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CustomerIntegralRecord> T_CustomerIntegralRecord { get; set; } = new List<T_CustomerIntegralRecord>();


        /// <summary>
        /// 会员编号-有的甲方有自己的会员唯一号，一般是会员卡号啥的
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 会员手机号
        /// </summary>
        public string CustomerPhoneNum { get; set; }
        /// <summary>
        /// 会员微信OpenID？UnionID？
        /// </summary>
        public string CustomerWechatId { get; set; }
        /// <summary>
        /// 会员微信昵称
        /// </summary>
        public string CustomerWechatNickName { get; set; }
        /// <summary>
        /// 会员微信头像？
        /// </summary>
        public string CustomerWechatHeadPhoto { get; set; }
        /// <summary>
        /// 会员真实姓名
        /// </summary>
        public string CustomerRealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string CustomerSex { get; set; }
        /// <summary>
        /// 证件类型-身份证、护照啥的
        /// </summary>
        public string CustomerCertificateType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CustomerCertificateNum { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? CustomerBirthday { get; set; }
        /// <summary>
        /// 所属省
        /// </summary>
        public string CustomerAddressProvince { get; set; }
        /// <summary>
        /// 所属市
        /// </summary>
        public string CustomerAddressCity { get; set; }
        /// <summary>
        /// 所属区
        /// </summary>
        public string CustomerAddressDistrict { get; set; }
        /// <summary>
        /// 会员入会时间
        /// </summary>
        public DateTime? CustomerTakeTime { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string CustomerWechatNum { get; set; }
        /// <summary>
        /// 微博号
        /// </summary>
        public string CustomerWeiboNum { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string CustomerQQNum { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string CustomerEmailNum { get; set; }
        /// <summary>
        /// 会员当前积分
        /// </summary>
        public int? CustomerCurrentIntegral { get; set; }
        /// <summary>
        /// 会员标签-就一个字段，
        /// 里面相关联的多个文字标签
        /// 用分割符或者逗号分开即可。
        /// </summary>
        public string CustomerTag { get; set; }
        /// <summary>
        /// 账户是否有效    0有效，1无效
        /// </summary>
        public int? AccountEffect { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag1 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag2 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag3 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag4 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag5 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag6 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag7 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag8 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag9 { get; set; }
        /// <summary>
        /// 客户自定义标签Tag1 
        /// </summary>
        public string CustomizeTag10 { get; set; }
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