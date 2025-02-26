namespace GPOSE_ASSIST_MOBILE
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

           

        }

        private void Layout_Loaded(object sender, EventArgs e)
        {
            bool BLN_RET;
            BLN_RET = GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_INIT("192.168.52.212", 1234);
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


        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            DisplayAlert("Alert", "You have been alerted", "OK");
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_SEND(count.ToString());
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
