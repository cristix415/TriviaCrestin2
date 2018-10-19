using Fleck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TriviaCrestin.Models;

namespace TriviaCrestin
{
    public partial class ServerTrivia : System.Web.UI.Page
    {
        List<Intrebare> _listIntrebari;
        Intrebare _randomIntrebare;
        static Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Brown, Color.BlueViolet };

        List<Profil> allSockets = new List<Profil>();
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.Where(intr=>intr.Id>38).ToList();
            }





            FleckLog.Level = LogLevel.Debug;

            var server = new WebSocketServer("ws://127.0.0.1:8181");
            int raspuns = 0;
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    var user = socket.ConnectionInfo.Path.Remove(0, 1);

                    Profil conexiune = new Profil { Socket = socket, UserName = user, Color = GetRandomColor() };

                    allSockets.Add(conexiune);
                    allSockets.ToList().ForEach(s => s.Socket.Send(conexiune.UserName + " a intrat."));
                };
                socket.OnClose = () =>
                {
                    System.Diagnostics.Debug.WriteLine("Close!");
                    var conexiune = allSockets.Where(s => s.Socket == socket).FirstOrDefault();
                    allSockets.Remove(conexiune);
                    allSockets.ToList().ForEach(s => s.Socket.Send(conexiune.UserName + " a iesit."));
                };
                socket.OnMessage = message =>
                {
                    Mesaj mesaj = new JavaScriptSerializer().Deserialize<Mesaj>(message);
                    System.Diagnostics.Debug.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Socket.Send(FormatMessage(mesaj)));
                    if (mesaj.Content == _randomIntrebare.Raspuns.ToString())
                    {                     
                        allSockets.ToList().ForEach(s => s.Socket.Send(mesaj.User + "WIN!!!"));
                    }
                };

            });

            while (true)
            {              
                System.Threading.Thread.Sleep(5000);
                Random rnd = new Random();
                int r = rnd.Next(_listIntrebari.Count);

                _randomIntrebare = _listIntrebari[r];
                allSockets.ToList().ForEach(s => s.Socket.Send(_randomIntrebare.Enunt));

            }
        }

        private string FormatMessage(Mesaj mesaj)
        {
            var profil = allSockets.Where(s => s.UserName == mesaj.User).FirstOrDefault();
            string detrimis = "<span style = \"color:" + ColorTranslator.ToHtml(profil.Color)
            + "\">" +
                   mesaj.User + "</span> @ " + DateTime.Now.ToString() +
                   ": " + mesaj.Content;

            return detrimis;

        }
        static Color GetRandomColor()
        {
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }
    }
}