using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;

namespace AppleAcres
{
    public sealed partial class TryAgainPage : Page
    {
        public TryAgainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            table.Text = "PreVIOus best - " + GamePage.best.ToString() + "\nCurrent scOre - " + GamePage.score.ToString();      
            if (GamePage.score > GamePage.best)
            {
                GamePage.best = GamePage.score;
                GamePage.settings.Values["best"] = GamePage.best;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {          
            if (MainMenu.isSoundOn)
                theEnd.AutoPlay = true;
            else
                theEnd.AutoPlay = false;
        }

        private void rateStore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new System.Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        private void shareFB_Tapped(object sender, TappedRoutedEventArgs e)
        {
           // (new MessageDialog("COMING SOON")).ShowAsync();
        }

        private void shareTWITTER_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //(new MessageDialog("COMING SOON")).ShowAsync();
        }

        private void shareVK_Tapped(object sender, TappedRoutedEventArgs e)
        {
           //(new MessageDialog("COMING SOON")).ShowAsync();
        }

        private void RestartBTN_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}