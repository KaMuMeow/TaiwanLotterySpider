using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
namespace TaiwanLotterySpider.Model
{
    public static class Model_TaiwanLotterySpider
    {
        //抓取台灣彩券歷史資料時，還需要先取得這三筆固定資訊
        private static string ViewStateGenerator = "";
        private static string EventValidation = "";
        private static string ViewState = "";
        //三個必須取得的參數因為格式是相同的，將三個需要的TAG存成陣列方便取用
        private static string[] ParameterTagArray = new string[]{ "__VIEWSTATE", "__VIEWSTATEGENERATOR", "__EVENTVALIDATION" };

        //表內分別是今彩539、大樂透、威立彩的標籤Tag名稱，%24為$的url轉換字符
        private static string[] LottoTypeArray = { "D539Control_history1%24",
                                              "Lotto649Control_history%24",
                                              "SuperLotto638Control_history1%24",};
        private static string TaiwanLotteryUrl = @"https://www.taiwanlottery.com.tw/lotto/{0}/history.aspx";
        //台灣各類型彩券的網址名稱
        private static string[] LottoUrlType = { "Dailycash", "Lotto649", "SuperLotto638" };
        private static string NowSearchType = "";
        public static string GetHistoryWeb (string LottoType,int Year,int Month)
        {
            string WContent = "";
            string AimUrl = "";
            string LottoName = "";
            switch(LottoType)
            {
                case "今彩539":
                    AimUrl = string.Format(TaiwanLotteryUrl, LottoUrlType[0]);
                    LottoName = LottoTypeArray[0];
                    break;
                case "大樂透":
                    AimUrl = string.Format(TaiwanLotteryUrl, LottoUrlType[1]);
                    LottoName = LottoTypeArray[1];
                    break;
                case "威力彩":
                    AimUrl = string.Format(TaiwanLotteryUrl, LottoUrlType[2]);
                    LottoName = LottoTypeArray[2];
                    break;
            }


            //有更換樂透類型時，或者剛啟動時需要再次取得必要的三參數
            if (NowSearchType == "" || NowSearchType != LottoType)
            {
                NowSearchType = LottoType;
                //先對目標歷史開獎的網頁進行第一次頁面取得
                WContent = PostWebContent(AimUrl);
                //分析該頁面所隱藏的三個必要參數，取得後才可以正常取得歷史開獎資訊
                GetNecessaryParameter(WContent);
            }

            //要取得三個必要參數 __VIEWSTATEGENERATOR,__EVENTVALIDATION,__VIEWSTATE
            string SearchContent = "__VIEWSTATEGENERATOR=" + ViewStateGenerator + "&"
                + "__EVENTVALIDATION=" + EventValidation + "&"
                + "__VIEWSTATE=" + ViewState + "&"
                + LottoName + "chk=radYM&"
                + LottoName + string.Format("dropYear={0}&", Year)
                + LottoName + string.Format("dropMonth={0}&",Month)
                + LottoName + "btnSubmit=%E6%9F%A5%E8%A9%A2";

            WContent = PostWebContent(AimUrl, SearchContent);

            return WContent;
        }
        

        private static string PostWebContent(string AimHttps,string PostDate="")
        {
            //對指定的網址傳送需求
            HttpWebRequest WRequest = (HttpWebRequest)WebRequest.Create(AimHttps);
            //建立基本的連線資訊
            WRequest.Method = "POST";
            WRequest.KeepAlive = true;
            WRequest.ContentType = "application/x-www-form-urlencoded";
            WRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            WRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";

            //若是有需要POST的參數則將POST參數傳送
            byte[] PostData = Encoding.UTF8.GetBytes(PostDate);
            WRequest.ContentLength = PostData.Length;
            using (Stream StreamReceive = WRequest.GetRequestStream()) //建立資料流，才可將資料傳送給網站
            {
                StreamReceive.Write(PostData, 0, PostData.Length); //將資料流傳給網站
            }

            //取得網站的回應結果
            HttpWebResponse response = (HttpWebResponse)WRequest.GetResponse();//建立網站給予回應
            string WContent = "";
            using (StreamReader Read = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("Utf-8")))
            {
                WContent = Read.ReadToEnd();
            }

            return WContent;
        }

        /// <summary>
        /// 輸入台灣彩卷的歷史開獎網頁內容，分析並抓取出三個必要的參數
        /// </summary>
        /// <param name="HtmlContent">台灣彩卷的歷史開獎網頁內容</param>
        private static void GetNecessaryParameter(string HtmlContent)
        {
            //取得__VIEWSTATE的內容
            int StartIndex = 0;
            int EndIndex = 0;
            int GetLength = 0;
            string GetParameter = "";
            foreach(string GetTag in ParameterTagArray)
            {
                StartIndex = HtmlContent.IndexOf("id=\""+GetTag+"\""); //抓出標籤的初步位置
                EndIndex = HtmlContent.IndexOf("\" />", StartIndex); //抓出Value的 "> 結尾符號
                StartIndex = HtmlContent.IndexOf("value=\"", StartIndex); //在抓出Value的 =" 起頭符號
                GetLength = EndIndex - (StartIndex + 7); //計算Value之間的長度
                //取出Value內的值
                //由於取出的參數值會有/ 或者 +，需要將其轉換成URL可以讀取得元素，否則POST出去時會產生問題
                GetParameter = HtmlContent.Substring(StartIndex + 7, GetLength).Replace("/", "%2F").Replace("+", "%2B");
                
                //參數是固定的，按照順序依序賦值。
                if (GetTag == ParameterTagArray[0])
                    ViewState = GetParameter;
                else if (GetTag == ParameterTagArray[1])
                    ViewStateGenerator = GetParameter;
                else if (GetTag == ParameterTagArray[2])
                    EventValidation = GetParameter;
            }


            //Console.WriteLine("ViewState={0}", ViewState);
            //Console.WriteLine("EventValidation={0}", EventValidation);
            //Console.WriteLine("ViewStateGenerator={0}", ViewStateGenerator);
        }
    }
}
