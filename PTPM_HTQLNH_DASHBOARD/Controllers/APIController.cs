using PTPM_HTQLNH_SHARE;
using PTPM_HTQLNH_SHARE.API;
using PTPM_HTQLNH_SHARE.API.Constance;
using PTPM_HTQLNH_SHARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTPM_HTQLNH_DASHBOARD.Controllers
{
    
    public class APIController : Controller
    {
        [HttpPost]
        public ActionResult Authorize(LoginODT login)
        {
            if (login == null || login.account == null || login.password == null)
            {
                return response(new Response { error = 1, message = Messages.LOGIN_NOT_FILL });
            }
            var passwordHash = login.password;

            var data = Helper.db.Users
                                .Where(m => m.login_name == login.account && m.password == passwordHash)
                                .FirstOrDefault(); 

            if (data == null)
                return response(new Response { error = 1, message = Messages.AUTH_FAILED });

            data.session = Helper.generateToken();
            var res = new { account = data.login_name, is_admin = data.is_admin, token = data.session };
            Helper.db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            Helper.db.SaveChanges();
            return response(new Response { error = 0,
                                           message = Messages.LOGIN_SUCCESS,
                                           data = res
            });
        }
        public ActionResult GetRestaurants()
        {
            var user = getUserRequest();
            if (user==null)
                return response(new Response { error = 1, message = Messages.AUTH_FAILED });


            var restaurants = (from u_r in Helper.db.User_Restaurant
                               join r in Helper.db.Restaurants on u_r.res_id equals r.id
                               where u_r.user_id == user.id
                               select r
                               ).ToList();

            return response(new Response { error = 0,
                                           message = Messages.QUERY_SUCCESS,
                                           data = restaurants
                      });

        }

        public ActionResult GetUserInformation()
        {

            var user = getUserRequest();
            if (user == null)
                return response(new Response { error = 1, message = Messages.AUTH_FAILED });

            int userId;
            if (Request.QueryString["user"] == null) 
                userId = user.id;
            else
                userId = Convert.ToInt32(Request.QueryString["user"]);

            var info = Helper.db.Employees.Where(m => m.login_id == userId).FirstOrDefault();
            if (info==null)
                return response(new Response { error = 1, message = Messages.QUERY_INFO_NOT_FOUND });

            return response(new Response { error = 0, message = Messages.QUERY_SUCCESS,data = info });
        }

        public ActionResult GetListMenuAsGroup()
        {
            var user = getUserRequest();
            if (user == null)
                return response(new Response { error = 1, message = Messages.AUTH_FAILED });

            var resId =  Request.QueryString["resId"];
            if (resId==null)
                return response(new Response { error = 1, message = Messages.QUERY_ISSUE });

            var menu = (from res in Helper.db.Restaurants
                        join r_u in Helper.db.User_Restaurant on res.id equals r_u.res_id
                        join mg in Helper.db.MenuGroups on res.id equals mg.res_id
                        where r_u.user_id == user.id && res.id == resId && mg.status == 1
                        select new
                        {
                            name = mg.name,
                            order = mg.order,
                            id = mg.id,
                            menu = (Helper.db.Menus.Where(m => m.group_id == mg.id).
                            Select(m => new
                            {
                                model = m.model,
                                name = m.name,
                                logo = m.logo,
                                tag_image = m.tag_image,
                                id = m.id

                            }).ToList())
                            
                        }).ToList();

            if (menu.Count<1)
                return response(new Response { error = 1, message = Messages.QUERY_NO_RESULT });

            return response(new Response { error = 0, message = Messages.QUERY_SUCCESS,data= menu });
        }
        private ActionResult response(Response data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        private User getUserRequest()
        {
            var accessToken = Request.QueryString["accessToken"] ?? Request.Headers["accessToken"];
            // xác thực access token
            var user = Helper.authorizeAccessToken(accessToken);
            return user;
        }
    }
}