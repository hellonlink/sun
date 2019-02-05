using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.ConsoleHelper.InitData
{
    public class InitCourse : BaseInit
    {
        public override void Run()
        {
            using (PeContext context = new PeContext())
            {
                AddCourse("CatA01", "CatB01", 10, context);
                AddCourse("CatA01", "CatB02", 7, context);
                AddCourse("CatA01", "CatB03", 5, context);
                AddCourse("CatA02", "CatB01", 3, context);
                AddCourse("CatA02", "CatB02", 12, context);
                AddCourse("CatA02", "CatB03", 6, context);
            }
        }

        private void AddCourse(string catA, string catB, int count, PeContext context)
        {
            for (int i = 1; i <= count; i++)
            {
                OnlineCourse oc = new OnlineCourse()
                {
                    name = "孩子的SEL社会情感课" + i.ToString("00"),
                    simpleIntro = "认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定。",
                    intro = "认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定\r\n\r\n认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定\r\n\r\n认识到人与人之间是不同的，学会在困扰之下冷静思考，并作出对自己和他人负责任的决定",
                    lecturer = "简·尼尔森 & 琳·洛特",
                    fromAge = 8,
                    toAge = 12,
                    price = 5800,
                    img = "/Images/temp/kc_img.jpg",
                    categoryA = catA,
                    categoryB = catB,
                    createdTime = DateTime.Now,
                    updatedTime = DateTime.Now,
                    publishedTime = DateTime.Now,
                    viewCount = 10,
                };

                oc.OnlineCourseData = new List<OnlineCourseData>();

                for (int j = 1; j <= 3; j++)
                {
                    OnlineCourseData item = new OnlineCourseData()
                    {
                        name = oc.name + "_在线课程" + j.ToString("00"),
                        dataLink = "http://7xk2h9.com1.z0.glb.clouddn.com/no1.mp4",
                        dataType = ConstValue.CourseDataType.Video,
                        sort = j,
                    };
                    oc.OnlineCourseData.Add(item);
                }

                OnlineCourseData tryItem = new OnlineCourseData()
                {
                    name = oc.name + "_在线试看",
                    dataLink = "http://7xlvto.media1.z0.glb.clouddn.com/PDC%E8%AE%A4%E8%AF%81%E8%AF%95%E7%9C%8B1.mp4",
                    dataType = ConstValue.CourseDataType.TryVideo,
                    sort = 1,
                };
                oc.OnlineCourseData.Add(tryItem);

                context.OnlineCourse.Add(oc);
            }

            context.SaveChanges();
        }
    }
}
