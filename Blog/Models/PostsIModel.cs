using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PostsIModel
    {
        public PostsIModel()
        {

        }

        public PostsIModel(int id, string name, string text, string date, string image)
        {
            this.id = id;
            this.Name = name;
            this.Text = text;
            this.Date = date;
            this.Image = image;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
    }
}