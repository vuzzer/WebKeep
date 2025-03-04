using WebKeep.Services;

namespace WebKeep
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceDB.ConfigurerBD();

            MainPage = new AppShell();
        }
    }
}
