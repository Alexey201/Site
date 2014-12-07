using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class MessageModel
    {
        private ICollection<MessageItemModel> items;

        public MessageModel()
        {
            this.items = new Collection<MessageItemModel>();
        }

        public MessageItemModel PostModel { get; set; }

        public ICollection<MessageItemModel> Items
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
    }
}