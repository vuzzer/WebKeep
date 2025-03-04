using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebKeep.Entites;

namespace WebKeep.Services
{
    public static class ServiceDB
    {
        public static SQLiteConnection ConnexionDB { get; set; }
        private const string NOM_BD = "WebKeepTest1.db3";

        public static void ConfigurerBD()
        {
            var cheminBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), NOM_BD);
            ConnexionDB = new SQLiteConnection(cheminBD);

            ConnexionDB.CreateTable<Compte>();
            ConnexionDB.CreateTable<Site>();
        }
    }
}
