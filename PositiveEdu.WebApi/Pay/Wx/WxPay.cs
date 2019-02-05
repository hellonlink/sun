using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Text;
using PositiveEdu.DAL;
using PositiveEdu.Common;

namespace PositiveEdu.WebApi.Pay.Wx
{
    public class WxPay: PayBase
    {
        #region 预付
        public override bool TradePrecreate(MyOrder order)
        {
            WxPayData data = new WxPayData();
            data.SetValue("body", order.OnlineCourse.name);//商品描述
            data.SetValue("attach", "");//附加数据
            data.SetValue("out_trade_no", order.ORDER_NUMBER);//订单号
            data.SetValue("total_fee", (int)(order.PRICE_PAY * 100));//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", order.OnLineCourseId);//商品ID

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            DoWaitProcess(result);

            return true;
        }

        /// <summary>
        /// 生成二维码并展示到页面上
        /// </summary>
        private void DoWaitProcess(WxPayData result)
        {
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
                                                                //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;

            //将字符串生成二维码图片
            Bitmap bt = qrCodeEncoder.Encode(url, Encoding.Default);

            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString() + ".jpg";
            string vPath = string.Format("/Images/QrCode/WxPay/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filename);
            string pPath = WebFunctions.MapServerResourcePath(vPath);
            WebFunctions.CreateDirectory(pPath);

            bt.Save(pPath);

            var relativelyPath = WebFunctions.ResourceMapRoot + vPath;
            qrCodeUrl = WebFunctions.ToAbsoluteResourceUrl(relativelyPath.TrimStart('~'));

            //轮询订单结果
            //根据业务需要，选择是否新起线程进行轮询
        }

        #endregion

        #region 通知回调
        public override bool Notify(HttpRequestBase request)
        {
            Notify noify = new Wx.Notify(request);
            WxPayData notifyData = noify.GetNotifyData();

            //检查openid和product_id是否返回
            if (!notifyData.IsSet("openid") || !notifyData.IsSet("product_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "回调数据异常");
                Log.Info(this.GetType().ToString(), "The data WeChat post is error : " + res.ToXml());

                notifyMessage = res.ToXml();
                return false;
            }

            //调统一下单接口，获得下单结果
            string openid = notifyData.GetValue("openid").ToString();
            string product_id = notifyData.GetValue("product_id").ToString();
            WxPayData unifiedOrderResult = new WxPayData();
            try
            {
                unifiedOrderResult = UnifiedOrder(openid, product_id);
            }
            catch (Exception ex)//若在调统一下单接口时抛异常，立即返回结果给微信支付后台
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + res.ToXml());
                notifyMessage = res.ToXml();
                return false;
            }

            //若下单失败，则立即返回结果给微信支付后台
            if (!unifiedOrderResult.IsSet("appid") || !unifiedOrderResult.IsSet("mch_id") || !unifiedOrderResult.IsSet("prepay_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + res.ToXml());
                notifyMessage = res.ToXml();
                return false;
            }

            //统一下单成功,则返回成功结果给微信支付后台
            WxPayData data = new WxPayData();
            data.SetValue("return_code", "SUCCESS");
            data.SetValue("return_msg", "OK");
            data.SetValue("appid", WxPayConfig.APPID);
            data.SetValue("mch_id", WxPayConfig.MCHID);
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
            data.SetValue("prepay_id", unifiedOrderResult.GetValue("prepay_id"));
            data.SetValue("result_code", "SUCCESS");
            data.SetValue("err_code_des", "OK");
            data.SetValue("sign", data.MakeSign());

            Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + data.ToXml());
            notifyMessage = data.ToXml();
            return true;
        }

        private WxPayData UnifiedOrder(string openId, string productId)
        {
            //统一下单
            WxPayData req = new WxPayData();
            req.SetValue("body", "test");
            req.SetValue("attach", "test");
            req.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            req.SetValue("total_fee", 1);
            req.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            req.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            req.SetValue("goods_tag", "test");
            req.SetValue("trade_type", "NATIVE");
            req.SetValue("openid", openId);
            req.SetValue("product_id", productId);
            WxPayData result = WxPayApi.UnifiedOrder(req);
            return result;
        }
        #endregion

        public override void LoopQuery()
        {
            throw new NotImplementedException();
        }
    }
}