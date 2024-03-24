using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetFlix.CustomControls
{
    public partial class DateItem : UserControl
    {
        public DateItem()
        {
            InitializeComponent();
        }

        private void InitialDateTime(DateTime datetime)
        {
            Month = datetime.Month.ToString();
            Day = datetime.Day.ToString();
            DayOfWeek = datetime.DayOfWeek;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitialDateTime(Datetime);
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DateItem));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
           DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(DateItem));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public string Tag
        {
            get { return (string)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }
        public static readonly DependencyProperty TagProperty = DependencyProperty.Register("Tag", typeof(string), typeof(DateItem));


        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }
        public static readonly DependencyProperty DayProperty = DependencyProperty.Register("Day", typeof(string), typeof(DateItem)); 
        
        public string Month
        {
            get { return (string)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }
        public static readonly DependencyProperty MonthProperty = DependencyProperty.Register("Month", typeof(string), typeof(DateItem));

        public DayOfWeek DayOfWeek
        {
            get { return (DayOfWeek)GetValue(DayOfWeekProperty); }
            set { SetValue(DayOfWeekProperty, value); }
        }
        public static readonly DependencyProperty DayOfWeekProperty = DependencyProperty.Register("DayOfWeek", typeof(DayOfWeek), typeof(DateItem));

        public DateTime Datetime
        {
            get { return (DateTime)GetValue(DatetimeProperty); }
            set { 
                SetValue(DatetimeProperty, value); 
                InitialDateTime(Datetime);
            }
        }
        public static readonly DependencyProperty DatetimeProperty = DependencyProperty.Register("Datetime", typeof(DateTime), typeof(DateItem));

    }
}
