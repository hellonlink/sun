using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TGLog;
namespace PositiveEdu.Admin.Controllers
{



    public class UploadtestController : Controller
    {
        // GET: Uploadtest
        public ActionResult Index()
        {
            return View();
        }



        static int p1 = 0;
        [HttpPost]
        public ActionResult GetUpload(int? p)

        {



            if (p1 == 1000)
            {
                p1 = 0;
            }

            Task.Run(

     () =>
     {
         Thread.Sleep(60000);

         p1 = 1000;


     }

                    );













            ILog log1 = LogManager.GetLogger("ReflectionLayout");
            ILog log2 = LogManager.GetLogger("HashtableLayout");
            ILog log3 = LogManager.GetLogger("MyLayout");

            //log1.Debug("这是一个基本测试");
            //log2.Debug(new LogMessage("这是一个参数测试"));



            //log2.Error(new LogMessage(1, 2, "这是三个参数测试").ToHashtable());

            for (int i = 0; i < 30; i++)
            {

                log1.Debug(new LogMessage(1, "操作对象000", 2, "这是四个参数测试"));
                log2.Debug(new LogMessage(1, "操作对象000", 2, "这是四个参数测试"));
                log3.Debug(new LogMessage(1, "操作对象000", 2, "这是四个参数测试"));
            }



            HttpPostedFileBase file = Request.Files["myFile"];
            if (file != null)
            {
                try
                {
                    var filename = Path.Combine(Request.MapPath("~/App_Data"), file.FileName);
                    file.SaveAs(filename);

                    var ps = Import(filename, 0);
                    var d = ps.CreateDataReader();

                    while (d.Read())
                    {

                        var name = d["name"].ToString();
                        var sex = d["sex"].ToString();
                        var age = d["age"].ToString();

                    }
                    return Json(p1);

                }
                catch (Exception ex)
                {
                    return Json(p1);

                }

            }
            else
            {




                return Json(p1);
            }

        }




        /// <summary>读取excel
        /// 默认第一行为表头
        /// </summary>
        /// <param name="strFileName">excel文档绝对路径</param>
        /// <param name="rowIndex">内容行偏移量，第一行为表头，内容行从第二行开始则为1</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, int rowIndex)
        {
            DataTable dt = new DataTable();

            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = WorkbookFactory.Create(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            //
            IRow headRow = sheet.GetRow(0);
            if (headRow != null)
            {
                int colCount = headRow.LastCellNum;
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("COL_" + i);
                }
            }

            for (int i = (sheet.FirstRowNum + rowIndex); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                bool emptyRow = true;
                object[] itemArray = null;

                if (row != null)
                {
                    itemArray = new object[row.LastCellNum];

                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {

                        if (row.GetCell(j) != null)
                        {

                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                    {
                                        itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                    }
                                    else//其他数字类型
                                    {
                                        itemArray[j] = row.GetCell(j).NumericCellValue;
                                    }
                                    break;
                                case CellType.Blank:
                                    itemArray[j] = string.Empty;
                                    break;
                                case CellType.Formula:
                                    if (Path.GetExtension(strFileName).ToLower().Trim() == ".xlsx")
                                    {
                                        XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    else
                                    {
                                        HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    break;
                                default:
                                    itemArray[j] = row.GetCell(j).StringCellValue;
                                    break;

                            }

                            if (itemArray[j] != null && !string.IsNullOrEmpty(itemArray[j].ToString().Trim()))
                            {
                                emptyRow = false;
                            }
                        }
                    }





                }

                //非空数据行数据添加到DataTable
                if (!emptyRow)
                {
                    dt.Rows.Add(itemArray);


                }


            }

            if (dt.Columns.Count > 0)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dt.Columns[j].ColumnName = dt.Rows[0][j].ToString();
                }

                dt.Rows.Remove(dt.Rows[0]);
            }

            return dt;
        }

        /// <summary>
        /// js/bootstrap表格渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult JsTableShow()
        {
            return View();
        }





    }
}