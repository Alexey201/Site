using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class LoginIModel
    {
        public LoginIModel()
        {
        }
        
        public LoginIModel(int id, string login, string pass, string mail)
        {
            this.Id = id;
            this.Login = login;
            this.Pass = pass;
            this.Mail = mail;
        }

        public int Id;
        public string Login { get; set; }
        public string Pass { get; set; }
        [RegularExpression(".+@{1}[a-zA-Z0-9]+\\.{1,}.+", ErrorMessage = "Неправильный email!")]
        public string Mail { get; set; }
    }

    
}