namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 数据字典
    /// </summary>
    [Table("DictData")]
    public class DictData
    {
        public string id { get; set; }

        public string name { get; set; }

        public string value { get; set; }

        public int weight { get; set; }
    }

    /// <summary>
    /// 全龄适用 - CatA01 / 讲师认证 - CatA02
    /// </summary>
    public class CourseCategoryA : DictData
    { }

    /// <summary>
    /// 课程分类
    /// </summary>
    public class CourseCategoryB : DictData
    { }

    /// <summary>
    /// 职业资质
    /// </summary>
    public class QualificationDict : DictData
    { }
}
