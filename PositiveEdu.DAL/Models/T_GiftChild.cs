﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    /// 礼品子表
    /// </summary>
    [Table("T_GiftChild")]

    public class T_GiftsChild
    {

        /// <summary>
        /// 礼品子表ID
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 对应的礼品ID-
        /// </summary>
        public int? T_GiftsId { get; set; }
        public T_Gifts T_Gifts { get; set; }
        /// <summary>
        /// Coupon编码-
        /// </summary>
        public string CouponNo { get; set; }
        /// <summary>
        /// 生成时间/导入时间-
        /// </summary>
        public DateTime? GenerationTime { get; set; }
        /// <summary>
        /// 生效时间-
        /// </summary>
        public DateTime? EffectiveTime { get; set; }
        /// <summary>
        /// 失效时间-
        /// </summary>
        public DateTime? FailureTime { get; set; }
        /// <summary>
        /// 会员ID- 就是这个虚拟礼品送给哪个会员了
        /// ，或者积分商城哪个会员兑换了
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 赠送/兑换时间-
        /// </summary>
        public DateTime? ExchangeTime { get; set; }
        /// <summary>
        /// 赠送/兑换类型-  就是区分下 
        /// 0这个是活动送出去的，
        /// 1还是人家主动积分商城里面换的
        /// </summary>
        public int?ExchangeType { get; set; }
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