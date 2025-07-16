//using Android.App;

using GPOSE_ASSIST_LIB;

namespace GPOSE_ASSIST_MOBILE
{
    public partial class MainPage : ContentPage
    {
        
        enum ENM_WINDOW_EXEC
        {
            CONNECT = 0,
            MAX
        }

        private bool BLN_WINDOW_EXEC_DO = false;

        private void SUB_EXEC_DO(ENM_WINDOW_EXEC ENM_EXEC)
        {
            if (BLN_WINDOW_EXEC_DO) 
            {
                return;
            }

            BLN_WINDOW_EXEC_DO = true;

            switch (ENM_EXEC)
            {
                case ENM_WINDOW_EXEC.CONNECT:
                    SUB_CONNECT();
                    break;
                case ENM_WINDOW_EXEC.MAX:
                    break;
            }

            BLN_WINDOW_EXEC_DO = false;
        }

        private void SUB_CONNECT()
        {

            string STR_IP;
            STR_IP = ENT_IP.Text;
            bool BLN_RET;
            BLN_RET = GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_INIT(STR_IP, 1234);
            if (!BLN_RET)
            {
                string STR_MSG;
                STR_MSG = "";
                STR_MSG += "接続に失敗しました。" + System.Environment.NewLine;
                STR_MSG += MOD_NETWORK_TCP.STR_MODULE_LAST_ERROR + System.Environment.NewLine;

                DisplayAlert(ME.Title, STR_MSG, "OK");
                return;
            }

            MOD_APPL_COMMON.CONNECTED = true;

            Shell.Current.GoToAsync("////PAGE_GPOSE");
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Layout_Loaded(object sender, EventArgs e)
        {

            CLS.CLS_PREFERENCES CLS_PRE;
            CLS_PRE = new CLS.CLS_PREFERENCES();

            CLS.CLS_PREFERENCES.SRT_APPLICATION_PREFERENCES SRT_PRE;
            SRT_PRE = CLS_PRE.FUNC_GET_MY_PREFERENCES();

            ENT_IP.Text = SRT_PRE.IP;
        }

        private void Layout_Unloaded(object sender, EventArgs e)
        {
            //GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_FIN();
        }

        private void CloseApp()
        {
#if IOS
        // iOSは、「Application.Quit();」で終了できません。
        // そのため、以下で終了させます。
        Environment.Exit(0);
#else
            App.Current?.Quit();
#endif
        }

        private void BTN_CONNECT_Clicked(object sender, EventArgs e)
        {
            Button BTN_SENDER;
            BTN_SENDER = (Button)sender;

            BTN_SENDER.IsEnabled = false;
            SUB_EXEC_DO(ENM_WINDOW_EXEC.CONNECT);
            BTN_SENDER.IsEnabled = true;
        }
    }
}
