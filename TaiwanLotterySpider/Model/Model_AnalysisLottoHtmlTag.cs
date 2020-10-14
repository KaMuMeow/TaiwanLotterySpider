using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiwanLotterySpider.Model
{
    public static class Model_AnalysisLottoHtmlTag
    {

        private static string WContent="";
        private enum LottoTypeAmount {今彩539=5,大樂透=6,威力彩=6};
        private static LottoTypeAmount GetLottoType;
        //儲存該月份的開獎日期和開獎球號
        public static List<Tuple<string, string>> AnalysisResult { private set; get; }
        public static void Analysis(string LoadContent,string SelectLotto)
        {
            //讀取要分析的頁面資訊
            WContent = LoadContent;
            //儲存結果初始化
            AnalysisResult = new List<Tuple<string, string>>();

            //將字串資訊轉換成Enum
            Enum.TryParse<LottoTypeAmount>(SelectLotto, out GetLottoType);

            //假設一月31天，每天開獎則有最多31期要搜尋。
            for (int i = 0; i <= 31;i++)
            {
                Tuple<string,string> Result;
                //取得該期的開獎時間
                string GetDate = GetHistoryDate(i);
                //已經找完該月的所有開獎期數了
                if (GetDate == "") break;
                Result = new Tuple<string, string>(GetDate, GetHistoryNumber(GetDate,i));
                AnalysisResult.Add(Result);
            }
        }

        /// <summary>
        /// 找尋月內的開獎日期
        /// </summary>
        /// <param name="Number">日期的順序從0開始</param>
        /// <returns></returns>
        private static string GetHistoryDate(int Number)
        {
            string GetDate = ""; //儲存開獎日期
            string StartTag = string.Format("Date_{0}\">",Number); //開獎日期的起始標籤
            string EndTag = "</span>"; //開獎日期的結尾標籤

            int StartIdx = WContent.IndexOf(StartTag)+StartTag.Length; //計算開獎日期標籤的起始索引值
            int EndIdx = WContent.IndexOf(EndTag, StartIdx); //找到開獎日期標籤的結尾索引值
            int GetLength = EndIdx - StartIdx; //計算標籤間的內容長度

            //如果找不到該期的開獎日期，代表已經找尋完畢
            if(StartIdx!=StartTag.Length-1)
            {
                GetDate = WContent.Substring(StartIdx, GetLength); //取出內容
            }

            return GetDate;
        }
        private static string GetHistoryNumber(string Date,int Phase)
        {
            string Result = "";
            string StartTag = "No{0}_{1}\">"; //今彩、大樂透、威力彩的共同標籤格式都為 No*_X ,*=順序、X=該月第幾期(最新是第0期)
            //為了通用只抓前面共有的No，此外標籤總長度為7、範例 No1_9"> 
            string EndTag = "</span>";

            int StartIdx = WContent.IndexOf(Date); //使用開獎日期做基本定位
            int EndIdx = 0;
            int GetLength = 0;

            StartIdx = WContent.IndexOf("大小順序", StartIdx); //用剛剛找到的基本標籤找到下一個位址

            for(int i=1;i<=(int)GetLottoType;i++)
            {
                string TmpStartTag = string.Format(StartTag, i, Phase);
                //取得開獎號碼的起頭Tag索引值
                StartIdx = WContent.IndexOf(TmpStartTag, StartIdx)+ TmpStartTag.Length;
                //開獎號碼的結尾Tag索引值
                EndIdx = WContent.IndexOf(EndTag, StartIdx);
                //取出號碼的長度
                GetLength = EndIdx - StartIdx;
                //將號碼依序串起來，並且判斷是否抵達結尾
                Result += WContent.Substring(StartIdx, GetLength) + (i == (int)GetLottoType ? "#" : ",");
            }

            return Result;
        }
    }
}
