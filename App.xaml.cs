﻿using Netflix.Utils;
using NetFlix.View;
using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace NetFlix
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore._navigationStore = new NavigationStore();   
            NavigationStore._navigationStore.CurrentViewModel = new LoginViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(NavigationStore._navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
