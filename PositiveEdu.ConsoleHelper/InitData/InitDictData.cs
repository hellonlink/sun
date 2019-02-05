using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PositiveEdu.DAL;
using System.Data.Entity.Migrations;

namespace PositiveEdu.ConsoleHelper.InitData
{
    public class InitDictData : BaseInit
    {
        public override void Run()
        {
            using (PeContext context = new PeContext())
            {
                InitCourseCategory1(context);
                InitCourseCategory2(context);
            }
        }

        private void InitCourseCategory1(PeContext context)
        {
            CourseCategoryA catA1 = new CourseCategoryA()
            {
                id = "CatA01",
                name = "全龄适用",
                weight = 1,
            };

            CourseCategoryA catA2 = new CourseCategoryA()
            {
                id = "CatA02",
                name = "讲师认证",
                weight = 2,
            };

            context.CourseCategoryA.AddOrUpdate(x => x.id, catA1, catA2);
            context.SaveChanges();
        }

        private void InitCourseCategory2(PeContext context)
        {
            CourseCategoryB catB1 = new CourseCategoryB()
            {
                id = "CatB01",
                name = "家长课堂",
                weight = 1,
            };

            CourseCategoryB catB2 = new CourseCategoryB()
            {
                id = "CatB02",
                name = "讲师认证家长课堂",
                weight = 2,
            };

            CourseCategoryB catB3 = new CourseCategoryB()
            {
                id = "CatB03",
                name = "指导课家长课堂",
                weight = 3,
            };

            context.CourseCategoryB.AddOrUpdate(x => x.id, catB1, catB2, catB3);
            context.SaveChanges();
        }
    }
}
