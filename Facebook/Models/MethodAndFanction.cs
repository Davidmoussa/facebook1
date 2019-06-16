using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class MethodAndFanction
    {

        public static string TimeAgo( DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }

            return result;
        }
       public static void setUserName (string Value )
        {
            HttpCookie cookie = new HttpCookie("USERNAME", Value);
            // set the cookie's expiration date
            cookie.Expires = DateTime.Now.AddDays(10);
            // set the cookie on client's browser
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string getUserName ()
        {
            if (HttpContext.Current.Request.Cookies["USERNAME"].Value != null && HttpContext.Current.Request.Cookies["USERNAME"] != null)
            {
                // if the mvcvalue exists as a cookie, use the cookie to get its value
                return  HttpContext.Current.Request.Cookies["USERNAME"].Value;
            } else return null;
        }

        public static void setUserID(string Value)
        {
            HttpCookie cookie = new HttpCookie("USERID", Value);
            // set the cookie's expiration date
            cookie.Expires = DateTime.Now.AddDays(10);
            // set the cookie on client's browser
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
      public  static string getUserId()
        {
            if (HttpContext.Current.Request.Cookies["USERID"].Value != null&& HttpContext.Current.Request.Cookies["USERID"]!=null)
            {
                // if the mvcvalue exists as a cookie, use the cookie to get its value
                return HttpContext.Current.Request.Cookies["USERID"].Value.ToString();
            }
            else return null;
        }
    }
}