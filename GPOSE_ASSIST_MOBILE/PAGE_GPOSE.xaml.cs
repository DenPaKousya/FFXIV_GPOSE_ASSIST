using GPOSE_ASSIST_LIB;

namespace GPOSE_ASSIST_MOBILE;

public partial class PAGE_GPOSE : ContentPage
{


    #region 画面用列挙定数
    enum ENM_WINDOW_EXEC
    {
        SEND = 0,
        MAX
    }
    #endregion

    #region 画面用変数
    private bool BLN_WINDOW_EXEC_DO = false;
    private string STR_DESCRIPTION = "";

    #endregion

    #region 実行処理呼出元
    private void SUB_EXEC_DO(ENM_WINDOW_EXEC ENM_EXEC)
    {
        if (BLN_WINDOW_EXEC_DO)
        {
            return;
        }

        BLN_WINDOW_EXEC_DO = true;

        switch (ENM_EXEC)
        {
            case ENM_WINDOW_EXEC.SEND:
                SUB_SEND();
                break;
            case ENM_WINDOW_EXEC.MAX:
                break;
        }

        BLN_WINDOW_EXEC_DO = false;
    }
    #endregion

    #region 実行処理群
    private void SUB_SEND()
    {

        bool BLN_RET;
        BLN_RET = MOD_APPL_COMMON.COMMON_RECONNECT();

        if (!BLN_RET)
        {
            string STR_MSG;
            STR_MSG = "";
            STR_MSG += "接続に失敗しました。" + System.Environment.NewLine;
            STR_MSG += MOD_APPL_COMMON.LAST_ERROR + System.Environment.NewLine;

            DisplayAlert(ME.Title, STR_MSG, "OK");
            return;
        }
        GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_SEND(STR_DESCRIPTION);
    }
    #endregion

    public PAGE_GPOSE()
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

    private void BTN_Clicked(object sender, EventArgs e)
    {
        Button BTN_SENDER;
        BTN_SENDER = (Button)sender;
        String STR_DES;
        STR_DES = SemanticProperties.GetDescription(BTN_SENDER);
        
        STR_DESCRIPTION = STR_DES;
        SUB_EXEC_DO(ENM_WINDOW_EXEC.SEND);
    }
}