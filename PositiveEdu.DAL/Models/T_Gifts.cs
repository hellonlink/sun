using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 礼品主表
    /// </summary>
    [Table("T_Gifts")]

    public class T_Gifts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Gifts()
        {
            //兑换礼品
            T_ExchangeGifts = new HashSet<T_ExchangeGifts>();
            //奖项字表，奖的类型 对应 礼品的类型
            T_RewardChild = new HashSet<T_RewardChild>();
            //礼品子表 
            T_GiftsChild = new HashSet<T_GiftsChild>();

        }
        /// <summary>
        /// 礼品ID
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        ///       会员主表    ,礼品兑换表 多对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ExchangeGifts> T_ExchangeGifts { get; set; } = new List<T_ExchangeGifts>();
        /// <summary>
        /// 活动奖项表,   活动奖项子表  多对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RewardChild> T_RewardChild { get; set; } = new List<T_RewardChild>();
        /// <summary>
        /// 礼品子表,一对多的关系
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_GiftsChild> T_GiftsChild { get; set; } = new List<T_GiftsChild>();

        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName { get; set; }
        /// <summary>
        /// 礼品形式-实体礼品/
        /// 虚拟礼品 1虚 0实
        /// </summary>
        public int? GiftType { get; set; }
        /// <summary>
        /// 礼品是否Coupon-

        ///第三方Coupon    1
        ///自主Coupon      0
        ///实际上凡是以唯一编码的方式送人的都可以算作Coupon
        /// </summary>
        public int? IsCoupon { get; set; }
        /// <summary>
        /// 礼品兑换券  0
        /// 现金抵用券    1
        ///折扣券      2
        /// </summary>
        public int? SaveType { get; set; }

        /// <summary>
        /// 消费金额限制：>=  
        /// </summary>
        public int? MoneyLimit { get; set; }

        /// <summary>
        /// 抵用金额
        /// </summary>
        public int? SaveMoney { get; set; }
        /// <summary>
        /// 礼品编号-编号指的不是CouponNo哦，
        /// 一种Coupon只能算一个礼品，
        /// 100张这种Coupon只能算这个礼品库存100
        /// </summary>
        public string GiftNo { get; set; }
        /// <summary>
        /// 礼品简介-一段纯文本，非必填，
        /// 这类介绍性的字段或图片展示类字段 
        /// 只有在甲方想基于本平台的数据做礼品信息展示的时候才会用到（例如积分商城啥的），如果本平台只作为活动自动送礼品的话实际是没用的。
        /// 也可作为Coupon的使用规则说明来用。
        /// </summary>
        public string GiftIntroductionText { get; set; }
        /// <summary>
        /// 礼品图文介绍-一段富文本，非必填，说明同“礼品简介”
        /// </summary>
        public string GiftIntroductionPT { get; set; }
        /// <summary>
        /// 礼品主图片-一张图片，非必填，
        /// 一般用作礼品详情页展示，说明同“礼品简介”
        /// </summary>
        public string GiftMainPicture { get; set; }
        /// <summary>
        /// 礼品缩略图片-一张图，非必填，
        /// 一般用于礼品列表展示，说明同“礼品简介”
        /// </summary>
        public string GiftThumbnailPicture { get; set; }
        /// <summary>
        /// 礼品库存量-数字  兑换一个少一个 
        /// 或者送一个少一个  完了就没了
        /// </summary>
        public int? GiftInventory { get; set; }
        /// <summary>
        /// 是否上架/有效- 这个一般也是在积分商城才有用
        ///1否，0是
        /// </summary>
        public int? IsShelf { get; set; }
        /// <summary>
        /// 是否积分商城可兑换：
        ///1否，0是
        /// </summary>
        public int? IsExchange { get; set; }
        /// <summary>
        /// 礼品所属分类
        /// </summary>
        public int? T_GiftsTagId { get; set; }
        /// <summary>
        /// 用于批量生成CouponNo的开头编码-最自主券才有用
        /// </summary>
        public string OpenCodeCouponNo { get; set; }
        /// <summary>
        /// 使用时间限制：开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 使用时间限制：结束时间
        /// </summary>
        public DateTime? StopTime { get; set; }
        /// <summary>
        /// 兑换积分- 用户兑换此礼品所需的积分（如果有积分商城的话）
        /// </summary>
        public int? RedeemPoints { get; set; }
        /// <summary>
        /// 备用字段 
        /// </summary>
        public string Tag1 { get; set; }
        /// <summary>
        /// 备用字段 
        /// </summary>
        public string Tag2 { get; set; }
        /// <summary>
        /// 备用字段 
        /// </summary>
        public string Tag3 { get; set; }
        /// <summary>
        /// 备用字段 
        /// </summary>
        public string Tag4 { get; set; }
        /// <summary>
        /// 备用字段 
        /// </summary>
        public string Tag5 { get; set; }
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