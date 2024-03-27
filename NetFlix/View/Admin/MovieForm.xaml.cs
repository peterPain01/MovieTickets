using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NetFlix.View.Admin
{
    public partial class MovieForm : Window
    {
        //  Register Dependency
        public static readonly DependencyProperty FormHeadingProperty =
            DependencyProperty.Register("FormHeading", typeof(String), typeof(MovieForm));

        public String FormHeading
        {
            get { return (String)GetValue(FormHeadingProperty); }
            set { SetValue(FormHeadingProperty, value); }
        }


        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("Command", typeof(ICommand), typeof(MovieForm));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        // End Register Dependency 

        public MovieForm()
        {
            InitializeComponent();
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // create file => binding file path 
        // set Build Action, Extension 
        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var uploadButton = (Button)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                if (uploadButton.Name == "HorizontalUploadBtn")
                {
                    HorizontalPreview.Source = bitmap;
                    PosterUrlSrcHidden.Text = openFileDialog.FileName; 
                }
                else if (uploadButton.Name == "VerticalUploadBtn")
                {
                    VerticalPreview.Source = bitmap;
                    PosterVerticalUrlSrcHidden.Text = openFileDialog.FileName;
                }
            }
        }

        private void AdminForm_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void AdminForm_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
