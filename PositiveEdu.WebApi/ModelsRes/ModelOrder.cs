using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.ModelsRes
{
    public class ModelOrder
    {
        public int id { get; set; }

        public string orderNumber { get; set; }

        public ModelCourse Course { get; set; }

        public decimal Price { get; set; }

        public DateTime? orderDate { get; set; }

        public ModelOrder() { }

        public ModelOrder(MyOrder item)
        {
            this.id = item.MYORDER_ID;
            this.orderNumber = item.ORDER_NUMBER;
            this.Price = (decimal)item.PRICE_PRODUCT;
            this.orderDate = item.DATE;
            this.Course = new ModelCourse(item.OnlineCourse);
        }
    }
}