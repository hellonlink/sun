using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PositiveEdu.DAL;

namespace PositiveEdu.ConsoleHelper.InitData
{
    public class InitDocument : BaseInit
    {
        public override void Run()
        {
            using (PeContext db = new PeContext())
            {
                for (int i = 1; i <= 10; i++)
                {
                    Guid saveId = Guid.NewGuid();
                    string saveFile = saveId.ToString().ToUpper().Replace("-", string.Empty);
                    Document doc = new Document()
                    {
                        title = "正面管教家长网络课程学习指南" + i.ToString("00"),
                        fileName = "正面管教家长网络课程学习指南" + i.ToString("00") + ".docx",
                        filePath = "/Document/" + saveFile,
                        extName = "doc",
                        fileSize = 3013678,
                        adminId = 7,
                        downCount = 0,
                        createdTime = DateTime.Now,
                        saveId = saveId,
                        intro = "家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课件家长讲师资质课程附赠配套课件",
                    };

                    db.Document.Add(doc);
                }
                db.SaveChanges();
            }
        }
    }
}
