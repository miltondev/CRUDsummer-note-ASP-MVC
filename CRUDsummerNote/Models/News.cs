using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDsummerNote.Models
{
    public class News
    {

        [Key]
        public int Id { get;set;}
        public String Tittle { get; set;}
        [AllowHtml]
        public String Content { get; set; }
    }
}