using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppleAcres
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
        }
        DispatcherTimer prepare = new DispatcherTimer();
        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
       async  protected override void OnNavigatedTo(NavigationEventArgs e)
        {
         
            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            prepare.Interval = new TimeSpan(0);
            prepare.Tick += Prepare_Tick;
            prepare.Start();
        }

        public static bool isSoundOn = true;

        private void Prepare_Tick(object sender, object e)
        {
            prepare.Stop();
            start.Margin = new Thickness(0, menubck.ActualHeight * 17 / 32, 0, 0);
            sound.Margin = new Thickness(0, menubck.ActualHeight * 17 / 32 + start.ActualHeight, 0, 0);
            info.Width = menubck.ActualWidth / 5;
            if (!GamePage.settings.Values.ContainsKey("sound"))
            {
                GamePage.settings.Values.Add("sound", isSoundOn);
            }
            else
            {
                isSoundOn = (bool)GamePage.settings.Values["sound"];
            }
            if (isSoundOn)
                sound.Text = "SOund On";
            else
                sound.Text = "SOund Off";
        }

        private void start_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void sound_Tapped(object sender, TappedRoutedEventArgs e)
        {
            isSoundOn = !isSoundOn;
            GamePage.settings.Values["sound"] = isSoundOn;
            if (isSoundOn)
                sound.Text = "SOund On";
            else
                sound.Text = "SOund Off";
        }

        private void info_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TextPage));
        }
    }
}
