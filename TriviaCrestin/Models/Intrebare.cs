using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaCrestin.Models
{
    public class Intrebare
    {
        public virtual int Id { get; set; }
        public virtual short Tip { get; set; }
        public virtual short Categorie { get; set; }
        public virtual string Enunt { get; set; }
        public virtual short Punctaj { get; set; }
        public virtual int Editie { get; set; }
        public virtual string Raspuns { get; set; }
        public virtual IList<Referinta> Referinte { get; set; }

    }
    public class Referinta
    {

        public virtual int id { get; set; }
        public virtual string Carte { get; set; }
        public virtual int Capitol { get; set; }
        public virtual int verset { get; set; }
        public virtual string Altele { get; set; }
        //  public virtual Intrebare Id_Intrebare { get; set; }

    }
}