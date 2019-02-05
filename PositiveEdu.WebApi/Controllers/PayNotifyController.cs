using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.Controllers
{
    public class PayNotifyController : Controller
    {
        public ActionResult AliPay()
        {
            Pay.PayBase py = new Pay.Ali.AliPay();
            return ProcessNotify(py, ConstValue.OrderPayWay.AliPay);
        }

        public ActionResult WxPay()
        {
            Pay.PayBase py = new Pay.Ali.AliPay();
            return ProcessNotify(py, ConstValue.OrderPayWay.WeChat);
        }

        private ActionResult ProcessNotify(Pay.PayBase py, string payWay)
        {
            bool pass = py.Notify(Request);

            if (pass)
            {
                if (py.NotifyTradeSuccess)
                {
                    using (PeContext db = new PeContext())
                    {

                        var order = db.MyOrder.Where(x => x.ORDER_NUMBER == py.NotifyOutTradeNo).FirstOrDefault();
                        if (order != null)
                        {
                            if (order.PAYMWNT_STATUS == ConstValue.OrderStatus.Unpaid)
                            {
                                order.PAY_WAY = payWay;
                                order.PAYMWNT_STATUS = ConstValue.OrderStatus.Paid;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }

            using (PeContext db = new PeContext())
            {
                PayNotifyLog log = new PayNotifyLog()
                {
                    payWay = ConstValue.OrderPayWay.AliPay,
                    orderNo = py.NotifyOutTradeNo,
                    tradeNo = py.NotifyTradeNo,
                    notifyMessage = py.NotifyMessage,
                    notifyTime = DateTime.Now,
                };
                db.PayNotifyLog.Add(log);
                db.SaveChanges();
            }

            return Content(py.NotifyMessage);
        }
    }
}