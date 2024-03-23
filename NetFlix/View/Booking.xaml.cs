using NetFlix.CustomControls;
using NetFlix.EnityModel;
using NetFlix.Model;
using NetFlix.ViewModel;
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
using ToastNotifications.Core;

namespace NetFlix.View
{

    public partial class Booking : UserControl
    {
        Color NORMAL_BORDER_COLOR = Colors.Green;
        Color MEDIUM_BORDER_COLOR = Colors.Pink;
        Color VIP_BORDER_COLOR = Colors.Red;

        public static readonly DependencyProperty SeatsProperty =
            DependencyProperty.Register("Seats", typeof(ObservableCollection<Seat>), typeof(Booking));

        public ObservableCollection<Seat> Seats
        {
            get { return (ObservableCollection<Seat>)GetValue(SeatsProperty); }
            set { SetValue(SeatsProperty, value); }
        }

        public Booking()
        {
            InitializeComponent();
        }

        private void InitializeMainGrid()
        {
            MainGrid.HorizontalAlignment = HorizontalAlignment.Center;
            MainGrid.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < 12; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 10; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void AddButtonsToGrid(ObservableCollection<Seat> Seats)
        {
            if (MainGrid != null && Seats != null)
            {
                foreach (Seat seat in Seats)
                {
                    Button button = new Button
                    {
                        Content = (seat.row + (seat.number).ToString()).ToString(), // Button text (1 to 100)
                        Margin = new Thickness(5), // Add some margin between buttons
                        FontSize = 13,
                        Width = 30,
                        Height = 30,
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fdfcf0")),
                        BorderThickness = new Thickness(1),
                        Cursor = Cursors.Hand,
                    };
                    button.Click += (s, e) =>
                    {
                        if (((MovieViewModel)DataContext).SelectedSeats.Any(s => s.row == seat.row && s.number == seat.number))
                        {
                            button.Background = new SolidColorBrush(Colors.Transparent);
                        }
                         else   button.Background = new SolidColorBrush(Colors.DarkRed);
                        ((MovieViewModel)DataContext).SeatClickedCommand.Execute(seat);
                            
                    };
                    if (seat.Status.Equals("Sold"))
                    {
                        button.FontSize = 16;
                        button.IsEnabled = false;
                        button.Content = 'X';
                    }
                    else
                    {
                        if (seat.row < 'C')
                        {
                            button.Template = CreateControlTemplate(1);
                            button.Style = CreateButtonStyle(1);
                        }
                        else if (seat.row < 'J')
                        {
                            button.Template = CreateControlTemplate(2);
                            button.Style = CreateButtonStyle(2);
                        }

                        else
                        {
                            button.Template = CreateControlTemplate(3);
                            button.Style = CreateButtonStyle(3);
                        }
                    }

                    Grid.SetRow(button, seat.row - 'A');
                    Grid.SetColumn(button, seat.number);
                    MainGrid.Children.Add(button);
                }
            }
        }

        // type value 
        // 1: normal 
        // 2: medium
        // 3: vip 
        private ControlTemplate CreateControlTemplate(int type)
        {
            ControlTemplate controlTemplate = new ControlTemplate(typeof(Button));
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            border.SetValue(Border.BorderThicknessProperty, new Thickness(0.7));
            if (type == 1)
                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Green));
            if (type == 2)
                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Pink));
            if (type == 3)
                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Red));
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            border.AppendChild(contentPresenter);
            controlTemplate.VisualTree = border;

            return controlTemplate;
        }


        private Style CreateButtonStyle(int type)
        {
            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Transparent));
            Trigger mouseOverTrigger = new Trigger { Property = UIElement.IsMouseOverProperty, Value = true };
            if (type == 1)
                mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Green));

            if (type == 2)
                mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Pink));
            if (type == 3)
                mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Red));

            buttonStyle.Triggers.Add(mouseOverTrigger);

            return buttonStyle;
        }


        private void bookingItem_Unloaded(object sender, RoutedEventArgs e)
        {
            if (MainGrid != null)
            {
                MainGrid.Children.Clear();
            }
        }

        private ToastViewModel _vm = new ToastViewModel();
        private void bookingItem_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                InitializeMainGrid();
                AddButtonsToGrid(Seats);
            }
        }
    }
}
