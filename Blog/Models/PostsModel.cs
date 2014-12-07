using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PostsModel
    {
        private ICollection<PostsIModel> items;
        private ICollection<MessageItemModel> items1;

        public PostsModel()
        {
            this.items = new Collection<PostsIModel>();
            this.items1 = new Collection<MessageItemModel>();
        }

        public PostsIModel PostPModel { get; set; }
        public MessageItemModel PostModel { get; set; }
        public bool reg { get; set; }
        public int PerId { get; set; }
        

        public ICollection<PostsIModel> PItems
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
        public ICollection<MessageItemModel> Items
        {
            get
            {
                return this.items1;
            }
            set
            {
                this.items1 = value;
            }
        }
    }
}