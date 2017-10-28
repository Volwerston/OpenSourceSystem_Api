using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSourceSystem_Api.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public DateTime AddingDate { get; set; }
    }
}