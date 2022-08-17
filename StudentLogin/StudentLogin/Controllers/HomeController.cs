using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentLogin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Index(Stud data)
        {
            try { 
            string StudentName = data.Name;
            string Email = data.Email;
            string MobileNo = data.mobileno;
            int flag = 1;
            DataTable dt = new DataTable();
            string query = @"execute Insert_data @StudentName= '" + StudentName + "', @Email= '" + Email + "', @MobileNo= " + MobileNo + ", @flag = " + flag + "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            return Redirect("/admin/index");
            }
            catch (Exception)
            {
                return Redirect("/home/error");
             }

        }
        

        public ActionResult Error()
            {
                return View();
            }
        
    }
}