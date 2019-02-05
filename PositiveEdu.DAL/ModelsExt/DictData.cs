namespace PositiveEdu.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �����ֵ�
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
    /// ȫ������ - CatA01 / ��ʦ��֤ - CatA02
    /// </summary>
    public class CourseCategoryA : DictData
    { }

    /// <summary>
    /// �γ̷���
    /// </summary>
    public class CourseCategoryB : DictData
    { }

    /// <summary>
    /// ְҵ����
    /// </summary>
    public class QualificationDict : DictData
    { }
}
