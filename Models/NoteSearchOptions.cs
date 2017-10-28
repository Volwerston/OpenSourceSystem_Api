using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSourceSystem_Api.Models
{
    public class NoteSearchOptions
    {
        public string Title { get; set; }
        public DateTime AddingDate { get; set; }
        public string Username { get; set; }
    }
}