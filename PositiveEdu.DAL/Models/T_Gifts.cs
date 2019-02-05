﻿using System;
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
        /// <summary>
        /// 礼品ID
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 会员主表,多对多的关系
        /// </summary>
        public virtual ICollection<T_Customer> T_Customer { get; set; } = new List<T_Customer>();
        /// <summary>
        /// 活动奖项表,多对多的关系
        /// </summary>
        public virtual ICollection<T_Reward> T_Reward { get; set; } = new List<T_Reward>();
        /// <summary>
        /// 礼品子表,一对多的关系
        /// </summary>
        public virtual ICollection<T_GiftsChild> T_GiftsChild { get; set; } = new List<T_GiftsChild>();

        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName { get; set; }
        /// <summary>
        /// 礼品形式-实体礼品/
        /// 虚拟礼品 0虚 1实
        /// </summary>
        public int? GiftType { get; set; }
        /// <summary>
        /// 礼品是否Coupon-
        ///不是Coupon      0
        ///第三方Coupon    1
        ///自主Coupon      2
        ///实际上凡是以唯一编码的方式送人的都可以算作Coupon
        /// </summary>
        public int? IsCoupon { get; set; }
        /// <summary>
        /// 礼品编号-编号指的不是CouponNo哦，
        /// 一种Coupon只能算一个礼品，
        /// 100张这种Coupon只能算这个礼品库存100
        /// </summary>
        public int? GiftNo { get; set; }
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
        ///0否，1是
        /// </summary>
        public int? IsShelf { get; set; }
        /// <summary>
        /// 用于批量生成CouponNo的开头编码-最自主券才有用
        /// </summary>
        public string OpenCodeCouponNo { get; set; }
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