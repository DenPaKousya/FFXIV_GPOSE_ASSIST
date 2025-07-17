using Microsoft.Maui.LifecycleEvents;

namespace GPOSE_ASSIST_MOBILE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            MOD_APPL_COMMON.COMMON_RESUME();
            base.OnResume();
        }

        protected override void OnSleep()
        {
            MOD_APPL_COMMON.COMMON_SLEEP();
            base.OnSleep();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
        }

    }
}