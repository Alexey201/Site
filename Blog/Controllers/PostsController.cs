using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Blog.Models;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        public static int PostId;
        public static PostsModel PostComm;
        public static PostsModel PostMod;
        
        public ActionResult Posts()
        {
            var posts = new PostsModel();
            posts.reg = Reg.r;
            posts.PerId = Reg.PersonId;
            posts.PItems = LoadPosts();
            return View(posts);
        }

        public ActionResult Comm(int id)
        {
            PostId = id;
            var posts = new PostsModel();
            
            posts.reg = Reg.r;
            posts.PerId = Reg.PersonId;
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();
            var command = new SqlCommand("SELECT * FROM [Posts] WHERE [PersonId] = " + id, conection);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                string name = reader["Name"] as string;
                string text = reader["Text"] as string;
                string date = reader["Date"] as string;
                string img = reader["Image"] as string;
                posts.PItems.Add(new PostsIModel(id, name, text, date, img));
            }

            reader.Close();
            reader.Dispose();
            
            var command1 = new SqlCommand("SELECT * FROM [Message] WHERE [ID] = " + id, conection);
            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                int id1 = reader1.GetInt32(reader1.GetOrdinal("PersonId"));
                string name = reader1["email"] as string;
                string text = reader1["message"] as string;
                //string date = reader["Date"] as string;
                posts.Items.Add(new MessageItemModel(name, text, id1));
            }
            PostComm = posts;
            reader1.Close();
            reader1.Dispose();
            
            conection.Close();
            conection.Dispose();

            return View(posts);
        }

        public ActionResult Del(int delid)
        {
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();

            var command = new SqlCommand("DELETE FROM [Message] WHERE [PersonId] = " + delid, conection);
            command.ExecuteNonQuery();
            
            Comm(PostId);
            conection.Close();
            conection.Dispose();
            
            return View("Comm", PostComm);
        }

        public ActionResult DelPost(int del)
        {
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();

            PostMod = new PostsModel();

            var command0 = new SqlCommand("DELETE FROM [Posts] WHERE [PersonId] = " + del, conection);
            command0.ExecuteNonQuery();

            var command1 = new SqlCommand("DELETE FROM [Message] WHERE [ID] = " + del, conection);
            command1.ExecuteNonQuery();

            conection.Close();
            conection.Dispose();

            PostMod.PItems = LoadPosts();
            PostMod.reg = Reg.r;
            PostMod.PerId = Reg.PersonId;
            return RedirectToAction("Posts", "Posts"); ;
        }

        [HttpPost]
        public ActionResult Posts(PostsModel model)
        {
            if (ModelState.IsValid)
            {
                var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                conection.Open();
                var command = new SqlCommand("INSERT INTO [Posts] VALUES (@Name, @Text, @Date, NULL)", conection);
                command.Parameters.Add(new SqlParameter("@Name", model.PostPModel.Name));
                command.Parameters.Add(new SqlParameter("@Text", model.PostPModel.Text));
                command.Parameters.Add(new SqlParameter("@Date", DateTime.Now));
                command.ExecuteNonQuery();
                conection.Close();
                conection.Dispose();
            }

            model.PItems = LoadPosts();
            model.reg = Reg.r;
            model.PerId = Reg.PersonId;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                file.SaveAs(path);

                var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                conection.Open();
                var command = new SqlCommand("UPDATE [Posts] SET [IMAGE] = (@Image) WHERE [PersonId] = " + PostId, conection);
                command.Parameters.Add(new SqlParameter("@Image", fileName));
                command.ExecuteNonQuery();
                conection.Close();
                conection.Dispose();
            }
            
            Comm(PostId);

            return View("Comm", PostComm);
        }

        [HttpPost]
        public ActionResult Comm(PostsModel model)
        {
            if (ModelState.IsValid)
            {
                var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                conection.Open();
                var command = new SqlCommand("INSERT INTO [Message] VALUES (@ID, @email, @message)", conection);
                command.Parameters.Add(new SqlParameter("@ID", PostId));
                command.Parameters.Add(new SqlParameter("@email", model.PostModel.Email));
                command.Parameters.Add(new SqlParameter("@message", model.PostModel.Message));
                command.ExecuteNonQuery();
                conection.Close();
                conection.Dispose();
                
            }

            Comm(PostId);

            return View(PostComm);
        }

        private ICollection<PostsIModel> LoadPosts()
        {
            ICollection<PostsIModel> posts = new Collection<PostsIModel>();
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();
            
            var command = new SqlCommand("SELECT * FROM [Posts]", conection);
            SqlDataReader reader = command.ExecuteReader();
            SqlDataReader reader1;
            
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("PersonID"));
                string name = reader["Name"] as string;
                string text = reader["Text"] as string;
                string date = reader["Date"] as string;
                string img = reader["Image"] as string;
                posts.Add(new PostsIModel(id, name, text, date, img));
            }
            
            reader.Close();
            reader.Dispose();
            conection.Close();
            conection.Dispose();
            return posts;
        }

    }
}
