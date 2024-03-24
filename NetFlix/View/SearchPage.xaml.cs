using NetFlix.EnityModel;
using NetFlix.ViewModel;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NetFlix.View
{
    public partial class SearchPage : UserControl
    {
        const int PAGE_SIZE = 3;
        public SearchPage()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            initPageButton();
            initGenresOption();
        }
        private void initPageButton()
        {
            int totalItems = (int)((SearchViewModel)DataContext).TotalRecords;
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);
            int currentPage = (int)((SearchViewModel)DataContext).CurrentPage;

            for (int i = 1; i <= totalPages; i++)
            {
                Button button = new Button();
                button.Content = $"{i}";
                button.Tag = i;
                button.Margin = new Thickness(8, 4, 8, 4);
                button.BorderThickness = new Thickness(1);
                UpdateButtonColor(button, i == currentPage);

                button.Click += (sender, e) =>
                {
                    int pageNumber = (int)((Button)sender).Tag;

                    ((SearchViewModel)DataContext).PaginateCommand.Execute(pageNumber);

                    UpdateButtonColors();
                };
                PaginationPanel.Children.Add(button);
            }
        }

        private void UpdateButtonColors()
        {
            int currentPage = (int)((SearchViewModel)DataContext).CurrentPage;

            foreach (Button button in PaginationPanel.Children)
            {
                int pageNumber = (int)button.Tag;
                bool isCurrentPage = pageNumber == currentPage;
                UpdateButtonColor(button, isCurrentPage);
            }
        }

        private void UpdateButtonColor(Button button, bool isCurrentPage)
        {
            if (isCurrentPage)
            {
                Color hexColor = (Color)ColorConverter.ConvertFromString("#e71a0f");
                button.Background = new SolidColorBrush(hexColor);
            }
            else
            {
                button.Background = Brushes.Transparent;
            }
        }

        private void initGenresOption()
        {
            int count = (int)((SearchViewModel)DataContext).Genres.Count;
            for (int i = 0; i < count; ++i)
            {
                Genre genre = ((SearchViewModel)DataContext).Genres[i];

                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = genre.Name;
                comboBoxItem.Tag = genre.GenreId;
                cbGenres.Items.Add(comboBoxItem);
            }
        }
    }
}
