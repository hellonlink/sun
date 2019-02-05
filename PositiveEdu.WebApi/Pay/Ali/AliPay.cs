using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;
using System.Threading;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Com.Alipay.Model;
using Com.Alipay.Domain;
using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using Com.Alipay.Business;

using PositiveEdu.DAL;
using PositiveEdu.Common;
using System.Collections.Specialized;

namespace PositiveEdu.WebApi.Pay.Ali
{
    public class AliPay : PayBase
    {
        #region 预付
        public override bool TradePrecreate(MyOrder order)
        {
            IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(AliPayConfig.serverUrl, AliPayConfig.appId, AliPayConfig.merchant_private_key, AliPayConfig.version,
                          AliPayConfig.sign_type, AliPayConfig.alipay_public_key, AliPayConfig.charset);

            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(order);
            string out_trade_no = builder.out_trade_no;
            AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, AliPayConfig.notifyUrl);

            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:
                    DoWaitProcess(precreateResult);
                    return true;
                case ResultEnum.FAILED:
                    precreateMessage = precreateResult.response.Body;
                    return false;
                case ResultEnum.UNKNOWN:
                    if (precreateResult.response == null)
                        precreateMessage = "配置或网络异常，请检查后重试";
                    else
                        precreateMessage = "系统异常，请更新外部订单后重新发起请求";
                    return false;
                default:
                    precreateMessage = "未定义的结果";
                    return false;
            }
        }

        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(MyOrder order)
        {
            string out_trade_no = order.ORDER_NUMBER;

            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号
            builder.seller_id = AliPayConfig.pid;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //订单总金额
            builder.total_amount = order.PRICE_PAY.Value.ToString();
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = order.NAME;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "001";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = order.Customer.MOBILE;

            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = order.OnlineCourse.id.ToString();
            goods.goods_name = order.OnlineCourse.name;
            goods.price = order.OnlineCourse.price.ToString();
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;

        }

        /// <summary>
        /// 生成二维码并展示到页面上
        /// </summary>
        /// <param name="precreateResult">二维码串</param>
        private void DoWaitProcess(AlipayF2FPrecreateResult precreateResult)
        {
            //打印出 preResponse.QrCode 对应的条码
            Bitmap bt;
            string enCodeString = precreateResult.response.QrCode;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            qrCodeEncoder.QRCodeScale = 3;
            qrCodeEncoder.QRCodeVersion = 8;
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);

            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString() + ".jpg";
            string vPath = string.Format("/Images/QrCode/AliPay/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), filename);
            string pPath = WebFunctions.MapServerResourcePath(vPath);
            WebFunctions.CreateDirectory(pPath);

            bt.Save(pPath);

            var relativelyPath = WebFunctions.ResourceMapRoot + vPath;
            qrCodeUrl = WebFunctions.ToAbsoluteResourceUrl(relativelyPath.TrimStart('~'));

            //轮询订单结果
            //根据业务需要，选择是否新起线程进行轮询
            //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(LoopQuery);
            //Thread myThread = new Thread(ParStart);
            //object o = precreateResult.response.OutTradeNo;
            //myThread.Start(o);
        }
        #endregion

        #region 通知回调
        public override bool Notify(HttpRequestBase request)
        {
            SortedDictionary<string, string> sPara = GetRequestPost(request);

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //Notify aliNotify = new Notify();
                Notify aliNotify = new Notify(AliPayConfig.charset, AliPayConfig.sign_type, AliPayConfig.pid, AliPayConfig.mapiUrl, AliPayConfig.alipay_public_key);

                //对异步通知进行验签
                bool verifyResult = aliNotify.Verify(sPara, request.Form["notify_id"], request.Form["sign"]);
                //对验签结果
                //bool isSign = Aop.Api.Util.AlipaySignature.RSACheckV2(sPara, Config.alipay_public_key ,Config.charset,Config.sign_type,false );

                if (verifyResult && CheckParams(request)) //验签成功 && 关键业务参数校验成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = request.Form["out_trade_no"];
                    notifyOutTradeNo = out_trade_no;

                    //支付宝交易号
                    string trade_no = request.Form["trade_no"];
                    notifyTradeNo = trade_no;

                    //交易状态
                    //在支付宝的业务通知中，只有交易通知状态为TRADE_SUCCESS或TRADE_FINISHED时，才是买家付款成功。
                    string trade_status = request.Form["trade_status"];
                    if (trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED")
                    {
                        notifyTradeSuccess = true;
                        notifyMessage = "success";
                    }
                    else
                    {
                        notifyTradeSuccess = false;
                        notifyMessage = "tradeFail";
                    }

                    return true;
                }
                else//验证失败
                {
                    notifyMessage = "validateFail";
                    return false;
                }
            }
            else
            {
                notifyMessage = "无通知参数";
                return true;
            }
        }

        /// <summary>
        /// 对支付宝异步通知的关键参数进行校验
        /// </summary>
        /// <returns></returns>
        private bool CheckParams(HttpRequestBase request)
        {
            bool ret = true;

            //获得商户订单号out_trade_no
            string out_trade_no = request.Form["out_trade_no"];
            //TODO 商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，

            //获得支付总金额total_amount
            string total_amount = request.Form["total_amount"];
            //TODO 判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），

            //获得卖家账号seller_email
            string seller_email = request.Form["seller_email"];
            //TODO 校验通知中的seller_email（或者seller_id) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id / seller_email）

            //获得调用方的appid；
            //如果是非授权模式，appid是商户的appid；如果是授权模式（token调用），appid是系统商的appid
            string app_id = request.Form["app_id"];
            //TODO 验证app_id是否是调用方的appid；。

            //验证上述四个参数，完全吻合则返回参数校验成功
            return ret;

        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost(HttpRequestBase request)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], request.Form[requestItem[i]]);
            }

            return sArray;
        }
        #endregion

        public override void LoopQuery()
        {
            throw new NotImplementedException();
        }
    }
}