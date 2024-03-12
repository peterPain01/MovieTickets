using System;
using System.Collections.Generic;
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

namespace Netflix.View
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        public Booking()
        {
            InitializeComponent();
            InitializeMainGrid();
            AddButtonsToGrid(); 
        }

        private void InitializeMainGrid()
        {
            MainGrid.HorizontalAlignment = HorizontalAlignment.Center; 
            MainGrid.VerticalAlignment = VerticalAlignment.Center; 

            // Define rows and columns
            for (int i = 0; i < 10; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Set MainGrid as content of the window
            Content = MainGrid;
        }
        private void AddButtonsToGrid()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button button = new Button
                    {
                        Content = ((row * 10) + col + 1).ToString(), 
                        Margin = new Thickness(2), 
                        Padding = new Thickness(5, 10, 5, 10), 
                        FontSize = 12 
                    };

                    Grid.SetRow(button, row); 
                    Grid.SetColumn(button, col); 

                    // Add button to the grid
                    MainGrid.Children.Add(button);
                }
            }
        }
    }
}
