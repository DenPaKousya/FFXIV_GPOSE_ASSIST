namespace GPOSE_ASSIST_MOBILE;

public partial class PAGE_HOT_BAR_2 : ContentPage
{
	public PAGE_HOT_BAR_2()
	{
		InitializeComponent();
	}

    private void Layout_Loaded(object sender, EventArgs e)
    {
    }

    private void Layout_Unloaded(object sender, EventArgs e)
    {
        MOD_APPL_COMMON.COMMON_PAGE_UNLOAD();
    }

}