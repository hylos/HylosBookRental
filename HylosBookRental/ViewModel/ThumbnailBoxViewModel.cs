using HylosBookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HylosBookRental.ViewModel
{
    public class ThumbnailBoxViewModel
    {
        public IEnumerable<ThumbnailModel> Thumbnails { get; set; }
    }
}