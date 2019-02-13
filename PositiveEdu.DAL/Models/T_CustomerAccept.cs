using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PositiveEdu.DAL
{
    /// <summary>
    ///会员收件信息表
    /// </summary>
    [Table("T_CustomerAccept")]

    public class T_CustomerAccept
    {

        /// <summary>
        /// 会员收件信息表ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int? T_CustomerId { get; set; }
        public T_Customer T_Customer { get; set; }
        /// <summary>
        /// 收件信息名称- 
        /// 例如家  公司啊 之类
        /// </summary>
        public string AcceptInformationName { get; set; }
        /// <summary>
        /// 收件省
        /// </summary>
        public string AcceptAddressProvince { get; set; }
        /// <summary>
        /// 收件市
        /// </summary>
        public string AcceptAddressCity { get; set; }
        /// <summary>
        /// 收件区
        /// </summary>
        public string AcceptAddressDistrict { get; set; }
        /// <summary>
        /// 收件邮编
        /// </summary>
        public string AcceptAddressPostCode { get; set; }
        /// <summary>
        /// 收件详细地址
        /// </summary>
        public string AcceptAddressDetail { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string AcceptName { get; set; }
        /// <summary>
        /// 收件人联系电话
        /// </summary>
        public DateTime? AcceptPhoneNum { get; set; }
        /// <summary>
        /// 是否默认收件信息
        /// 0 否 1是
        /// </summary>
        public int? IdDefaultAccept { get; set; }
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