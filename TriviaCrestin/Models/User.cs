using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaCrestin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        public Guid socketId { get; set; }
    }
}