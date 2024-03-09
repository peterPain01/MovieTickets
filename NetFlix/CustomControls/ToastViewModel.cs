using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace NetFlix.CustomControls
{
    public class ToastViewModel : INotifyPropertyChanged
    {
        private readonly Notifier _notifier;
        private readonly int NOTIFY_HEIGHT = 62; 
        private readonly int NOTIFY_WIDTH = 120; 
        public ToastViewModel()
        {

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: Application.Current.MainWindow.Width / 2 - NOTIFY_WIDTH,
                    offsetY: Application.Current.MainWindow.Height - NOTIFY_HEIGHT);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(6),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = false;
                cfg.DisplayOptions.Width = 250;
            });

            _notifier.ClearMessages(new ClearAll());
        }

        public void OnUnloaded()
        {
            _notifier.Dispose();
        }

        public void ShowInformation(string message)
        {
            _notifier.ShowInformation(message);
        }

        public void ShowInformation(string message, MessageOptions opts)
        {
            _notifier.ShowInformation(message, opts);
        }

        public void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message);
        }

        public void ShowSuccess(string message, MessageOptions opts)
        {
            _notifier.ShowSuccess(message, opts);
            Task.Delay(3000).ContinueWith((task) =>
            {
                OnUnloaded();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        internal void ClearMessages(string msg)
        {
            _notifier.ClearMessages(new ClearByMessage(msg));
        }

        public void ShowWarning(string message, MessageOptions opts)
        {
            _notifier.ShowWarning(message, opts);
        }

        public void ShowError(string message)
        {
            _notifier.ShowError(message);
            
            Task.Delay(3000).ContinueWith((task) =>
            {
                OnUnloaded();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void ShowError(string message, MessageOptions opts)
        {
            _notifier.ShowError(message, opts);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearAll()
        {
            _notifier.ClearMessages(new ClearAll());
        }
    }
}
