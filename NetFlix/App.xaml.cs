using NetFlix.View;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NetFlix
{

    public partial class App : Application
    {

        //protected void ApplicationStart(object sender, StartupEventArgs e)
        //{
        //    var loginView = new LoginView();
        //    loginView.Show();
        //    loginView.IsVisibleChanged += (s, ev) =>
        //    {
        //        if (loginView.IsVisible == false && loginView.IsLoaded)
        //        {
        //            var mainViewUser = new MainViewUser();
        //            mainViewUser.Show();
        //            loginView.Close();
        //        }
        //    };
        //}
        //protected override void OnStartup(StartupEventArgs e) 
        //{
        //    mainwindow = new mainwindow()
        //    {
        //        datacontext = new mainviewmodel()
        //    };
        //    mainwindow.show();
        //    base.onstartup(e);
        //}
    }
}
