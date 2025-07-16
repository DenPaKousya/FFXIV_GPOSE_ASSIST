using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPOSE_ASSIST_MOBILE
{
    static class MOD_APPL_COMMON
    {

        private static bool BLN_CONNECTED=false; // メンバ変数

        // プロパティ
        public static bool CONNECTED
        {
            get { return BLN_CONNECTED; } // getterの部分
            set { BLN_CONNECTED = value; } // setterの部分
        }

    }
}
