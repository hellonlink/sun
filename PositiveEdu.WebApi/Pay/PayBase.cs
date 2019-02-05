using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PositiveEdu.DAL;

namespace PositiveEdu.WebApi.Pay
{
    public abstract class PayBase
    {
        protected string qrCodeUrl;
        public string QrCodeUrl
        {
            get { return qrCodeUrl; }
        }

        protected string precreateMessage;
        public string PrecreateMessage
        {
            get { return precreateMessage; }
        }

        public abstract bool TradePrecreate(MyOrder order);


        protected string notifyMessage;
        public string NotifyMessage
        {
            get { return notifyMessage; }
        }

        protected string notifyOutTradeNo;
        public string NotifyOutTradeNo
        {
            get { return notifyOutTradeNo; }
        }

        protected string notifyTradeNo;
        public string NotifyTradeNo
        {
            get { return notifyTradeNo; }
        }

        protected bool notifyTradeSuccess;
        public bool NotifyTradeSuccess
        {
            get { return notifyTradeSuccess; }
        }

        public abstract bool Notify(HttpRequestBase request);

        public abstract void LoopQuery();
    }
}