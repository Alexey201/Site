using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using Blog.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //private RegModel rmodel;
         
        public ActionResult Index()
        {
            var lmodel = new LoginModel();
            //rmodel = new RegModel();
            var pmodel = new PostsModel();

            //lmodel.LItems = LoadLogins();
            lmodel.LItems = LoadLogins();
            Reg.r = false;
            Reg.PersonId = 0;
            
            return View(lmodel);
        }

        public ActionResult Registration()
        {
            var rmodel = new RegModel();
            rmodel.RItems = LoadLogins();
            Reg.r = false;
            Reg.PersonId = 0;

            return View(rmodel);
        }
        
        public ActionResult Home()
        {
            var model = new MessageModel();

            model.Items = LoadItems();

            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(RegModel rmodel)
        {
            


            if (ModelState.IsValid)
            {
                var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                conection.Open();
                var command = new SqlCommand("INSERT INTO [Users] VALUES (@Login, @Pass, @email)", conection);
                command.Parameters.Add(new SqlParameter("@Login", rmodel.RegPModel.Login));
                command.Parameters.Add(new SqlParameter("@Pass", rmodel.RegPModel.Pass));
                command.Parameters.Add(new SqlParameter("@email", rmodel.RegPModel.Mail));
                command.ExecuteNonQuery();
                conection.Close();
                conection.Dispose();

                MailSender.send(rmodel.RegPModel.Mail, "Здравствуйте, уважаемый " + rmodel.RegPModel.Login +
                    ". Вы зарегистрировались на моем блоге. Ваш пароль: " + rmodel.RegPModel.Pass);

                Reg.r = true;
            }
            
            rmodel.RItems = LoadLogins();
            if (Reg.r) return RedirectToAction("Index", "Home");
            else
                return View("Index", "Home");


        }
      
        [HttpPost]
        public ActionResult Home(MessageModel model)
        {
            if (ModelState.IsValid)
            {
                var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
                conection.Open();
                var command = new SqlCommand("INSERT INTO [Message] VALUES (@email, @message)", conection);
                command.Parameters.Add(new SqlParameter("@email", model.PostModel.Email));
                command.Parameters.Add(new SqlParameter("@message", model.PostModel.Message));
                command.ExecuteNonQuery();
                conection.Close();
                conection.Dispose();
            }

            model.Items = LoadItems();

            return View(model);
        }
        //-----------
        [HttpPost]
        public ActionResult Index(LoginModel lmodel)
        {
            lmodel.LItems = LoadLogins();
            bool b = false;
            if (ModelState.IsValid)
            {
                foreach (var item in lmodel.LItems)
                {
                    if ((item.Login == lmodel.UserLogin) && (item.Pass == lmodel.UserPass))
                    {
                        
                        Reg.r = true;
                        Reg.PersonId = item.Id;
                        break;
                    }
                    else Reg.r = false;

                }

                
                
            }


            if (Reg.r) return RedirectToAction("Posts", "Posts");
            else 
            return View(lmodel);
        }
        
       
        //-----------
        private ICollection<MessageItemModel> LoadItems()
        {
            ICollection<MessageItemModel> items = new Collection<MessageItemModel>();
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();
            var command = new SqlCommand("SELECT * FROM [Message]", conection);

            SqlDataReader reader = command.ExecuteReader();
            /*
            while (reader.Read())
            {
               
                string email = reader["email"] as string;
                string message = reader["message"] as string;
                items.Add(new MessageItemModel( email, message));
            }
            */
            reader.Close();
            reader.Dispose();
            conection.Close();
            conection.Dispose();
            return items;
        }
        private ICollection<LoginIModel> LoadLogins()
        {
            ICollection<LoginIModel> logins = new Collection<LoginIModel>();
            var conection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            conection.Open();
            var command = new SqlCommand("SELECT * FROM [Users]", conection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("PersonId"));
                string log = reader["Login"] as string;
                string pas = reader["Pass"] as string;
                string mail = reader["email"] as string;
                logins.Add(new LoginIModel(id, log, pas, mail));
            }

            reader.Close();
            reader.Dispose();
            conection.Close();
            conection.Dispose();
            return logins;
        } 

    }
}
