using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PositiveEdu.DAL;

namespace PositiveEdu.ConsoleHelper.InitData
{
    public class InitMenu : BaseInit
    {
        public override void Run()
        {
            using (PeContext db = new PeContext())
            {
                InitRoots(db);
            }
        }

        private static void InitRoots(PeContext db)
        {
            ROLE_FUNCTIONS muAdmin = new ROLE_FUNCTIONS()
            {
                NAME = "账户及权限",
                TYPE = 0,
                Controller = "Admin",
                SORT = 1,
            };
            db.ROLE_FUNCTIONS.Add(muAdmin);

            ROLE_FUNCTIONS muCustomer = new ROLE_FUNCTIONS()
            {
                NAME = "会员管理",
                TYPE = 0,
                Controller = "Customer",
                SORT = 2,
            };
            db.ROLE_FUNCTIONS.Add(muCustomer);

            ROLE_FUNCTIONS muMainPage = new ROLE_FUNCTIONS()
            {
                NAME = "首页管理",
                TYPE = 0,
                Controller = "MainPage",
                SORT = 3,
            };
            db.ROLE_FUNCTIONS.Add(muMainPage);

            ROLE_FUNCTIONS muCourseCategory = new ROLE_FUNCTIONS()
            {
                NAME = "课程分类",
                TYPE = 0,
                Controller = "CourseCategory",
                SORT = 4,
            };
            db.ROLE_FUNCTIONS.Add(muCourseCategory);

            ROLE_FUNCTIONS muCourse = new ROLE_FUNCTIONS()
            {
                NAME = "线上课程",
                TYPE = 0,
                Controller = "Course",
                SORT = 5,
            };
            db.ROLE_FUNCTIONS.Add(muCourse);

            ROLE_FUNCTIONS muQualifications = new ROLE_FUNCTIONS()
            {
                NAME = "资质管理",
                TYPE = 0,
                Controller = "Qualifications",
                SORT = 6,
            };
            db.ROLE_FUNCTIONS.Add(muQualifications);

            ROLE_FUNCTIONS muNews = new ROLE_FUNCTIONS()
            {
                NAME = "新闻管理",
                TYPE = 0,
                Controller = "News",
                SORT = 7,
            };
            db.ROLE_FUNCTIONS.Add(muNews);

            ROLE_FUNCTIONS muMarket = new ROLE_FUNCTIONS()
            {
                NAME = "市场合作",
                TYPE = 0,
                Controller = "Market",
                SORT = 8,
            };
            db.ROLE_FUNCTIONS.Add(muMarket);

            ROLE_FUNCTIONS muAboutUs = new ROLE_FUNCTIONS()
            {
                NAME = "关于我们",
                TYPE = 0,
                Controller = "AboutUs",
                SORT = 9,
            };
            db.ROLE_FUNCTIONS.Add(muAboutUs);

            ROLE_FUNCTIONS muDocument = new ROLE_FUNCTIONS()
            {
                NAME = "资料下载",
                TYPE = 0,
                Controller = "Document",
                SORT = 10,
            };
            db.ROLE_FUNCTIONS.Add(muDocument);

            ROLE_FUNCTIONS muParentAssessment = new ROLE_FUNCTIONS()
            {
                NAME = "家长评估",
                TYPE = 0,
                Controller = "ParentAssessment",
                SORT = 11,
            };
            db.ROLE_FUNCTIONS.Add(muParentAssessment);

            db.SaveChanges();
        }
    }
}
