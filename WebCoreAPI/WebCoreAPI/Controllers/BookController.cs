using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCoreAPI.Models;

namespace WebCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        public DateTime ParseDate(string inputString)
        {
         
            DateTime dDate;
            if (DateTime.TryParseExact(inputString, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out dDate)) return dDate;
            else
            {
                dDate = new DateTime(Convert.ToInt32(inputString), 1, 1);
                return dDate;
            }
        }
        [HttpGet]
        public List<BookModel> GetBooks()
        {
           

            var request = WebRequest.Create("https://www.googleapis.com/books/v1/volumes?q=C#");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            var books = new List<BookModel>();
            foreach (var item in jsonData.items)
            {
                string dateString = item.volumeInfo.publishedDate;
                books.Add(new BookModel
                {
                    ID = item.id,
                    Title = item.volumeInfo.title,
                    Author = item.volumeInfo.authors[0],
                    Description = item.volumeInfo.description,
                    Image = item.volumeInfo.imageLinks.thumbnail,
                    PublishedDate = ParseDate(dateString)
                    /*PublishedDate = new DateTime(Convert.ToInt32(item.volumeInfo.publishedDate), 1, 1)*/

                });
            }    
            return books;
        }
        [HttpGet("{id}")]
        public BookModel BookDetail(string id)
        {
            var request = WebRequest.Create("https://www.googleapis.com/books/v1/volumes/"+id);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            string dateString = jsonData.volumeInfo.publishedDate;
            BookModel book = new BookModel
            {
                ID = jsonData.id,
                Title = jsonData.volumeInfo.title,
                Author = jsonData.volumeInfo.authors[0],
                Description = jsonData.volumeInfo.description,
                Image = jsonData.volumeInfo.imageLinks.thumbnail,
                PublishedDate = ParseDate(dateString)

            };
            return book;
        }
    }
}