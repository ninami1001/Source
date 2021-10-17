using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Zadatak1.Models;
using System.Data.Entity;

namespace Zadatak1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public System.Web.Http.Results.JsonResult<List<ResponseModel>> Get()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/users");

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            String rawresp = readStream.ReadToEnd();
            var result = JsonConvert.DeserializeObject<List<UsersJson>>(rawresp);
            response.Close();
            readStream.Close();
            //Posts 

            HttpWebRequest requestPosts = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/posts");
            // Set some reasonable limits on resources used by this request
            requestPosts.MaximumAutomaticRedirections = 4;
            requestPosts.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            requestPosts.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse responsePosts = (HttpWebResponse)requestPosts.GetResponse();

            // Get the stream associated with the response.
            Stream receiveStreamPosts = responsePosts.GetResponseStream();
            // Pipes the stream to a higher level stream reader with the required encoding format.
            StreamReader readStreamPosts = new StreamReader(receiveStreamPosts, Encoding.UTF8);
            String rawrespPosts = readStreamPosts.ReadToEnd();
            var resultPosts = JsonConvert.DeserializeObject<List<Posts>>(rawrespPosts);
            responsePosts.Close();
            readStreamPosts.Close();
            using (var context = new Model_Zadatak())
            {
                context.Posts.RemoveRange(context.Posts);
                context.SaveChanges();
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
                context.Address.RemoveRange(context.Address);
                context.SaveChanges();
                context.Company.RemoveRange(context.Company);
                context.SaveChanges();
                context.Geo.RemoveRange(context.Geo);
                context.SaveChanges();
            }
            using (var context = new Model_Zadatak())

            {

                foreach (var item in result)
                {
                    var geo = new Geo
                    {
                        Lng = item.address.geo.lng,
                        Lat = item.address.geo.lat,
                    };
                    context.Geo.Add(geo);
                    context.SaveChanges();
                    var address = new Address
                    {
                        Street = item.address.street,
                        Suite = item.address.street,
                        City = item.address.street,
                        Zipcode = item.address.street,
                        Geo = geo.ID,
                    };
                    context.Address.Add(address);
                    context.SaveChanges();
                    var company = new Company
                    {
                        Bs = item.company.bs,
                        CatchPhrase = item.company.catchPhrase,
                        Name = item.company.name,
                    };
                    context.Company.Add(company);
                    context.SaveChanges();
                    var users = new Users
                    {
                        ID = item.id,
                        UserName = item.username,
                        UserPassword = RandomString(8),
                        EmailAddress = item.email,
                        Name = item.name,
                        Phone = item.phone,
                        Website = item.website,
                        Company = company.ID,
                        Adress = address.ID,
                    };
                    context.Users.Add(users);
                    context.SaveChanges();
                }
                foreach (var item in resultPosts)
                {
                    var post = new Posts
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        Title = item.Title,
                        Body = item.Body,
                    };
                    context.Posts.Add(post);
                    context.SaveChanges();
                }
            }


            List<ResponseModel> querylist = new List<ResponseModel>();

            using (Model_Zadatak context = new Model_Zadatak())
            {

                var query = context.Users.Include(m => m.Posts).Select(value => new ResponseModel
                {
                    posts = value.Posts.Select(value1 => new ResponsePosts
                    {
                        Title = value1.Title,
                        Body = value1.Body
                    }).ToList(),
                    name = value.Name,
                    username = value.UserName,
                    email = value.EmailAddress

                }).ToList();
                 return Json(query);
            }

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
      

    }
}
