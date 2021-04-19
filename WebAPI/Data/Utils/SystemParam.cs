using Data.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils
{
    public class SystemParam
    {
        public const string CONVERT_DATETIME = "dd/MM/yyyy";
        public const string CONVERT_DATETIME_HAVE_HOUR = "HH:mm:ss dd/MM/yyyy";
        public const string CONVERT_DATETIME_HOUR = "HH:mm";

        public const int MAX_ROW_IN_LIST_WEB = 30;
        public const int PAGE_SIZE = 20;
        public const int ACTIVE = 1;
        public const int NOT_ACTIVE = 2;

        public const int IS_ORDER = 1;
        public const int IS_XU_LY = 2;
        public const int IS_HUY_DH = 3;

        public const int NEW_PRODUCT = 0;
        public const int BEST_SALE_PRODUCT = 1;
        public const int HOT_PRODUCT = 2;
        public const int ROLE_CUSTOMER = 2;
        public const int ROLE_ADMIN = 1;
        public const int WRONG_PASSWORD = 2;
        public const int EXISTING = 2;
        public const int RETURN_FALSE = 0;
        public const int RETURN_TRUE = 1;
        public const int ACTIVE_FALSE = 0;
        public const int ERROR = 0;
        public const int SUCCESS = 1;
        public const int DUPLICATE_NAME = 2;
        public const float KeyA = 11;
        public const float KeyB = 87;
        public const float KeyC = 48;
        public const bool BOOLEAN_TRUE = true;
        public const bool BOOLEAN_FALSE = false;
        public const string RESET_PASSWORD = "123456";

        public const string VANG = "VANG";
        public const string BAC = "BAC";
        public const string KIM_CUONG = "KIM_CUONG";
        public const string MEMBER = "MEMBER";

        public const string DK_VANG = "5,000,000";
        public const string DK_BAC = "1,000,000";
        public const string DK_KIM_CUONG = "15,000,000";

    }
}
