using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPOSE_ASSIST_MOBILE.CLS
{
    public class CLS_PREFERENCES
    {

        #region 外部向構造体

        public struct SRT_APPLICATION_PREFERENCES
        {
            public String IP;
        }

        #endregion

        #region 外部向変数

        public SRT_APPLICATION_PREFERENCES MY_PREFERENCES;

        #endregion

        #region 内部向定数

        private String CST_KEY_IP="IP";

        private String CST_DEFAULT_VALUE_IP = "192.168.52.212";

        #endregion

        public SRT_APPLICATION_PREFERENCES FUNC_GET_MY_PREFERENCES()
        {
            SRT_APPLICATION_PREFERENCES SRT_RET;

            String STR_TEMP;
            STR_TEMP = "";

            STR_TEMP = Preferences.Default.Get(CST_KEY_IP, CST_DEFAULT_VALUE_IP);
            SRT_RET.IP = STR_TEMP;

            return SRT_RET;
        }

        public void FUNC_SET_MY_PREFERENCES(SRT_APPLICATION_PREFERENCES SRT_SET)
        {
            String STR_TEMP;
            STR_TEMP = "";

            STR_TEMP = SRT_SET.IP;
            Preferences.Default.Set(CST_KEY_IP, STR_TEMP);

        }

    }
}
