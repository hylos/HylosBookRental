﻿using HylosBookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HylosBookRental.ViewModel
{
    public class BookViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Book Book { get; set; }
    }
}