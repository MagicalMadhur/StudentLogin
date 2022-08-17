using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using StudentLogin.Models;
namespace StudentLogin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            string id = Request.QueryString["id"];

            IEnumerable<Stud> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44330/api/");

            var consumedata = hc.GetAsync("Values/" + id + "");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<Stud>>();
                results.Wait();
                ec = results.Result;
            }
            return View(ec);
        }
        public ActionResult Edit(int id)
        {
            IEnumerable<Stud> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44330/api/");

            var consumedata = hc.GetAsync("Values/" + id + "");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<Stud>>();
                results.Wait();
                ec = results.Result;
            }
            return View(ec);
        }
        [HttpPost]
        public ActionResult Edit(Stud data)
        {
            int ID = data.ID;
            string Name = data.Name;
            string Email = data.Email;
            string mobileno = data.mobileno;
            DataTable dt = new DataTable();
            string query = @"execute update_data @ID=" + ID + ",@Name='" + Name + "',@Email='" + Email + "', @mobileno='" + mobileno + "'";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Redirect("/admin/index");
        }
        public ActionResult Delete(int id)
        {
            DataTable dt = new DataTable();
            string query = @"execute delete_data @ID=" + id + "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Redirect("/admin/index");
        }
    }
}