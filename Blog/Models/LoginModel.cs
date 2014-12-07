using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class LoginModel
    {
        private ICollection<LoginIModel> items;

        public LoginModel() 
        {
            this.items = new Collection<LoginIModel>();
        }

        public LoginIModel PostLModel { get; set; }

        public ICollection<LoginIModel> LItems
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
            }
        }

        public string UserLogin { get; set; }
        public string UserPass { get; set; }
     




    }
}