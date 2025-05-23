﻿namespace GPOSE_ASSIST_MOBILE
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Layout_Loaded(object sender, EventArgs e)
        {
            bool BLN_RET;
            BLN_RET = GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_INIT("192.168.10.222", 1234);
            if (!BLN_RET)
            {
                CloseApp();
            }
        }

        private void Layout_Unloaded(object sender, EventArgs e)
        {
            GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_FIN();
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

        private void BTN_Clicked(object sender, EventArgs e)
        {
            Button BTN_SENDER;
            BTN_SENDER = (Button)sender;

            String STR_DES;
            STR_DES = SemanticProperties.GetDescription(BTN_SENDER);

            GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_SEND(STR_DES);
        }
    }
}
