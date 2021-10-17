using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class ResponseModel
    {
      
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public List<ResponsePosts> posts { get; set; }
        }     
      

        public class ResponsePosts
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public int UserId { get; set; }
        }

   
}