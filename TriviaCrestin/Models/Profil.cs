using Fleck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TriviaCrestin.Models
{
    public class Profil
    {
        public IWebSocketConnection Socket { get; set; }
        public Color Color { get; set; }
        public string UserName { get; set; }
    }
}