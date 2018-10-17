using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Hypermedia.WebApi.Services
{
    public class Book
    {
        public string Title { get; internal set; }
        public int Id { get; internal set; }
        public string Description { get; internal set; }
        public BookStatus Status { get; set; }
        public bool IsFree { get; set; }
    }
}
