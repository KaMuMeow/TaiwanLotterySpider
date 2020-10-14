using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using TaiwanLotterySpider.Model;
namespace TaiwanLotterySpider
{
    public partial class MainForm : Form
    {
        private delegate void Dele_ShowLog(string Log);
        private delegate void Dele_RefreshBtn_StartStatus();
        public MainForm()
        {
            InitializeComponent();
        }
        List<Tuple<string, string>> GetHistoryPhase;
        int StartYear = 0;
        int EndYear = 0;
        string LottoType = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            Btn_Export.Click += (s, E) => { Export(); };
        }

        private void Btn_GetHttp_Click(object sender, EventArgs e)
        {
            GetHistoryPhase = new List<Tuple<string, string>>(); //初始化儲存的期數
                                                                 //取得要抓的年份區間
            List_ShowLog.Items.Clear();
            try
            {
                StartYear = int.Parse(Text_Year.Text);
                EndYear = int.Parse(Text_Purpose.Text);
            }
            catch
            {
                MessageBox.Show("請輸入正常的民國年份", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (EndYear < StartYear)
            {
                MessageBox.Show("目標年份必須大於等於起始年份", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Btn_GetHttp.Enabled = false;
            Btn_Export.Enabled = false;
            LottoType = Com_Select.Text;
            ShowLog(string.Format("開始抓取{0}的歷史開獎",LottoType));

            Thread StaetThread = new Thread(new ThreadStart(Thread_GetHistoryPhase));
            StaetThread.Start();
        }

        private void Thread_GetHistoryPhase()
        {
            //開始依照年份跟月份依序取得歷史開獎紀錄的網頁
            for (int year = StartYear; year <= EndYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    ShowLog(string.Format("開始抓取{0}年{1}月的資料", year, month));
                    string GetContent = Model_TaiwanLotterySpider.GetHistoryWeb(LottoType, year, month);
                    Model_AnalysisLottoHtmlTag.Analysis(GetContent, LottoType);
                    ShowLog(string.Format("{0}年{1}月抓取完成", year, month));
                    //如果分析完期數是空的，代表已經跑完則跳出迴圈
                    if (Model_AnalysisLottoHtmlTag.AnalysisResult.Count == 0)
                        break;
                    else
                    {
                        //將每個月抓到的開獎結果取出，並且以倒序的形式放入總儲存結果內。
                        for (int i = Model_AnalysisLottoHtmlTag.AnalysisResult.Count - 1; i >= 0; i--)
                        {
                            GetHistoryPhase.Add(Model_AnalysisLottoHtmlTag.AnalysisResult[i]);
                        }
                    }
                    int SleepTime = new Random(DateTime.Now.GetHashCode()).Next(1,3);
                    if(month!=12)
                        ShowLog(string.Format("休息{0}秒後執行下次抓取", SleepTime));
                    Thread.Sleep(SleepTime*1000);
                }
            }
            //使用委派更新抓取按鈕的狀態
            RefreshBtn_StartStatus();
        }

        private void Export()
        {
            string DirPath = "";
            string SaveName = string.Format("{0}_{1}-{2}.txt", LottoType, StartYear, EndYear);
            using (StreamWriter Write = new StreamWriter(SaveName))
            {
                string Content = "";
                if(Chk_IncludeDate.Checked)
                {
                    for (int i = 0; i < GetHistoryPhase.Count; i++)
                    {
                        Content = "---Date:" + GetHistoryPhase[i].Item1+"---"+Environment.NewLine+GetHistoryPhase[i].Item2;
                        Write.WriteLine(Content);
                    }
                }
                else
                {
                    for (int i = 0; i < GetHistoryPhase.Count; i++)
                    {
                        Content = GetHistoryPhase[i].Item2;
                        Write.WriteLine(Content);
                    }
                }
            }
            MessageBox.Show("匯出完成!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string GetRootPath =Directory.GetCurrentDirectory();
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = GetRootPath;
            prc.Start();
        }

        //使用委派來顯示目前的進度
        private void ShowLog(string Log)
        {
            if(List_ShowLog.InvokeRequired)
            {
                List_ShowLog.Invoke(new Dele_ShowLog(ShowLog),Log);
            }else
            {
                List_ShowLog.Items.Add(Log);
                List_ShowLog.Items.Add("----------");
                List_ShowLog.SelectedIndex = List_ShowLog.Items.Count - 1;
            }
        }

        private void RefreshBtn_StartStatus()
        {
            if (Btn_GetHttp.InvokeRequired)
                Btn_GetHttp.Invoke(new Dele_RefreshBtn_StartStatus(RefreshBtn_StartStatus));
            else
            {
                Btn_GetHttp.Enabled = true;
                ShowLog("抓取結束!!，可以匯出囉");
                Btn_Export.Enabled = true;
                if (Chk_AutoExport.Checked)
                    Export();
            }
        }

    }
}
