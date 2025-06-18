namespace GPOSE_ASSIST_MOBILE;

public partial class PAGE_GPOSE : ContentPage
{
	public PAGE_GPOSE()
	{
		InitializeComponent();
	}

    private void Layout_Loaded(object sender, EventArgs e)
    {

    }

    private void Layout_Unloaded(object sender, EventArgs e)
    {

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