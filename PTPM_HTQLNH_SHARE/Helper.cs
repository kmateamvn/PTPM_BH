using PTPM_HTQLNH_SHARE.API;
using PTPM_HTQLNH_SHARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTPM_HTQLNH_SHARE
{
    public class Helper
    {
        private static Models.BTLPTPM_RES_DBEntities _db;
        public static BTLPTPM_RES_DBEntities db{
           get
            {
                if (_db == null) _db = new BTLPTPM_RES_DBEntities();
                return _db;
            }
       }
        // hàm tạo ngẫu nhiên token
        public static string generateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()); ;
        }
        public static User authorizeAccessToken(String accessToken)
        {
            // kiểm tra xác thực
            if (accessToken == null || accessToken.Trim()==String.Empty)
                return null;
            // xác nhận xác thực tìm ra user login
            var user = Helper.db.Users.Where(m => m.session == accessToken).FirstOrDefault();
            if (user == null)
                return null;

            return user;
        }
    }
}
