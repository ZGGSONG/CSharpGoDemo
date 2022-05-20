using ScadaTool.Helper;
using static ScadaTool.Helper.GoCSharpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScadaTool.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {

            bool tmp = false;
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler((obj, args) =>
            {
                icoPath = tmp == true ? "Resources/icon_red.ico" : "Resources/none.ico";
                tmp = tmp == true ? false : true;
            });
            timer.Start();

            #region Command
            ExitApplicationCommand = new RelayCommand((_) =>
            {
                return true;
            }, (_) =>
            {
                ViewModelHelper.Instance().UpdateUI(() =>
                {
                    Application.Current.Shutdown();
                });
            });

            ShowWindowCommand = new RelayCommand((_) =>
            {
                return true;
            }, (_) =>
            {
                ViewModelHelper.Instance().UpdateUI(() =>
                {
                    Application.Current.MainWindow.Show();
                    Application.Current.MainWindow.Activate();
                });
            });

            // 服务
            StartServiceCommand = new RelayCommand((_) =>
            {
                return true;
            }, (_) =>
            {
                string result = GoCSharpHelper.Instance().GoStringToCSharpString(run());
                if (result.Equals("success"))
                {
                    MessageBox.Show("服务已开启\n返回结果: " + result);
                    icoPath = "Resources/icon.ico";
                    timer.Dispose();
                }
                else
                {
                    MessageBox.Show("服务开启失败\n返回结果: " + result);
                }
            });
            #endregion

            
        }

        /// <summary>
        /// 引入Go项目库
        /// </summary>
        /// <returns></returns>
        [DllImport("scadatool_go.dll", EntryPoint = "run")]
        extern static GoString run();


        #region 属性
        private string mToolTip = "服务未启动";
        public string toolTip
        {
            get => mToolTip;
            set { mToolTip = value; NotifyChanged("toolTip"); }
        }
        private string mIcoPath = "Resources/icon_red.ico";

        public string icoPath
        {
            get => mIcoPath;
            set { mIcoPath = value; NotifyChanged("icoPath"); }
        }

        public ICommand ExitApplicationCommand { get; private set; }
        public ICommand ShowWindowCommand { get; private set; }
        public ICommand HidenApplicationCommand { get; private set; }
        public ICommand StartServiceCommand { get; private set; }
        #endregion
    }
}
