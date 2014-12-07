using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class MessageItemModel
    {
        public MessageItemModel()
        {
            
        }

        public MessageItemModel( string email, string message, int id)
        {
            this.Email = email;
            this.Message = message;
            this.Id = id;
        }

        public string Message { get; set; }
        [RegularExpression(".+@{1}[a-zA-Z0-9]+\\.{1,}.+", ErrorMessage = "Неправильный email!")]
        public string Email { get; set; }
        public int Id { get; set; }
    }
}