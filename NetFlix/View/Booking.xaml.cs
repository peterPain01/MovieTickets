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

namespace NetFlix.View
{

    public partial class Booking : Window
    {
         Color NORMAL_BORDER_COLOR = Colors.Green;
         Color MEDIUM_BORDER_COLOR = Colors.Pink;
         Color VIP_BORDER_COLOR = Colors.Red;

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
            for (int i = 0; i < 9; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void AddButtonsToGrid()
        {
            if (MainGrid != null)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        Button button = new Button
                        {

                            Content = ((row * 10) + col + 1).ToString(), // Button text (1 to 100)
                            Margin = new Thickness(8), // Add some margin between buttons
                            FontSize = 13,
                            Width = 30,
                            Height = 30,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fdfcf0")),
                            BorderThickness = new Thickness(1),
                            Cursor = Cursors.Hand
                        };
                        if (row < 2)
                        {
                            button.Template = CreateControlTemplate(1);
                            button.Style = CreateButtonStyle(1);
                        }
                        else if (row < 7)
                        {
                            button.Template = CreateControlTemplate(2);
                            button.Style = CreateButtonStyle(2);
                        }

                        else
                        {
                            button.Template = CreateControlTemplate(3);
                            button.Style = CreateButtonStyle(3);
                        }


                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, col);
                        MainGrid.Children.Add(button);
                    }
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
            if(type == 1)
                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Green));
            if(type == 2)
                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Colors.Pink));
            if(type == 3)
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
    }
}
