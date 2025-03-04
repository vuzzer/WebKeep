using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebKeep.Entites
{
    public class Site
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Lien { get; set; }
        public string? Note { get; set; }
        public int IdCompte { get; set; }
        public DateTime Date { get; set; }
    }
}
