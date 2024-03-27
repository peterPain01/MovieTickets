using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetFlix.CustomControls
{
    public partial class PaginateControl : UserControl
    {
        public PaginateControl()
        {
            InitializeComponent();
        }

        private Button lastButton;

        // Register Dependency to reuseable - I just want reuse UI so i dont do it
        private void InitPaginateButton()
        {
            var numberOfPages = (int)(Math.Ceiling(((AdminDashBoardVM)DataContext).AllMovies.Count / (double)6));
            for (int i = 1; i <= numberOfPages; i++)
            {
                Button numberButton = new Button();
                numberButton.Style = FindResource("paginateButton") as Style;
                numberButton.Content = i.ToString();
                paginateStackPanel.Children.Add(numberButton);
                if (numberButton.Content.ToString() == "1")
                {
                    lastButton = numberButton;
                    lastButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3675ef"));
                    lastButton.Foreground = new SolidColorBrush(Colors.White);
                }
                numberButton.Click += (e, s) =>
                {
                    if (numberButton != lastButton)
                    {
                        numberButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3675ef"));
                        numberButton.Foreground = new SolidColorBrush(Colors.White);

                        lastButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C7682"));
                        lastButton.Background = new SolidColorBrush(Colors.Transparent);
                        lastButton = numberButton;
                    }
                    ((AdminDashBoardVM)DataContext).PaginateMovieCommand.Execute(Convert.ToInt32(numberButton.Content));
                };

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitPaginateButton(); 
        }
    }
}
