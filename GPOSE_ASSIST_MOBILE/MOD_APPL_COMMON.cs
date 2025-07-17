using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPOSE_ASSIST_MOBILE
{
    static class MOD_APPL_COMMON
    {

        #region プロパティ用変数向構造体
        private static bool BLN_CONNECTED = false;
        private static string STR_LAST_ERROR = "";
        #endregion

        #region プロパティ
        public static bool CONNECTED
        {
            get { return BLN_CONNECTED; } // getterの部分
            set { BLN_CONNECTED = value; } // setterの部分
        }
        public static string LAST_ERROR
        {
            get { return STR_LAST_ERROR; } // getterの部分
            set { STR_LAST_ERROR = value; } // setterの部分
        }
        #endregion

        public static void COMMON_RESUME()
        {

            COMMON_RECONNECT();

        }

        public static bool COMMON_RECONNECT()
        {
            if (BLN_CONNECTED)
            {
                return true;
            }

            bool BLN_RET;
            BLN_RET = GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_INIT();


            if (!BLN_RET)
            {
                STR_LAST_ERROR = GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.STR_MODULE_LAST_ERROR;
                return false;
            }
            BLN_CONNECTED = true;
            return true;
        }

        public static void COMMON_SLEEP()
        {

            if (BLN_CONNECTED)
            {
                GPOSE_ASSIST_LIB.MOD_NETWORK_TCP.FUNC_CLIENT_FIN();
                BLN_CONNECTED = false;
            }

        }

    }
}
