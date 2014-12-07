using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class RegModel
    {
        private ICollection<LoginIModel> items;

        public RegModel() 
        {
            this.items = new Collection<LoginIModel>();
        }


        public LoginIModel RegPModel { get; set; }
        public ICollection<LoginIModel> RItems
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
        public string UserMail { get; set; }

    }
}