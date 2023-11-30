using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace AppleAcres
{
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        DispatcherTimer showtmr = new DispatcherTimer();
        DispatcherTimer prepare = new DispatcherTimer();
        Storyboard applesanim = new Storyboard();

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            if (!settings.Values.ContainsKey("best"))
            {
                settings.Values.Add("best", best);
            }
            else
            {
                best = (int)settings.Values["best"];
            }
            prepare.Interval = new TimeSpan(0);
            prepare.Tick += prepare_Tick;
            showtmr.Interval = new TimeSpan(0, 0, 0, 0, 700);
            showtmr.Tick += showtmr_Tick;
            applesanim.Completed += applesanim_Completed;
            cideranimstarter.Completed += cideranimstarter_Completed;
            prepare.Start();
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            readytmr.Stop();
            prepare.Stop();
            showtmr.Stop();
            showtmr.Tick -= showtmr_Tick;
            prepare.Tick -= prepare_Tick;

            cider.RenderTransform = null;
            cideranimstarter.Stop();

            applesanim.Stop();
            applesanim.Children.Clear(); 
            applesanim.Completed -= applesanim_Completed;

            apple1.RenderTransform = null;
            apple2.RenderTransform = null;
            apple3.RenderTransform = null;
            apple4.RenderTransform = null;
            apple5.RenderTransform = null;

            Frame.GoBack();
            e.Handled = true;
        }

        void cideranimstarter_Completed(object sender, object e)
        {          
            timeout = true;
            if (lastappleanimended && applesanimended && !itsnavigated)
                showtmr.Start();
        }
        

        public static Windows.Storage.ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public static int best;
        public static int score = 0;
        int count = 0;
        double distance;
        double lastdistance;
        double baskettop;
        double lastRightAppleTop;
        double lastLeftAppleTop;
        double rightAppleRight;
        double leftAppleRight;
        Random rand = new Random();
        BitmapImage ajsourcekickright = new BitmapImage();
        BitmapImage ajsourcestayright = new BitmapImage();
        BitmapImage ajsourcekickleft = new BitmapImage();
        BitmapImage ajsourcestayleft = new BitmapImage();
        DoubleAnimation apple1anim = new DoubleAnimation();
        DoubleAnimation apple2anim = new DoubleAnimation();
        DoubleAnimation apple3anim = new DoubleAnimation();
        DoubleAnimation apple4anim = new DoubleAnimation();
        DoubleAnimation apple5anim = new DoubleAnimation();
        DoubleAnimation cideranim = new DoubleAnimation();
        Storyboard cideranimstarter = new Storyboard();
        bool waitforchance = false;
        bool rightbtntapped = false;
        bool leftbtntapped = false;
        bool failed = false;     
        bool fivefisrtapplesgone = false;
        bool timeout = false;
        bool lastappleanimended = true;
        bool applesanimended = true;
        bool itsnavigated = false;

        DispatcherTimer readytmr = new DispatcherTimer();
        bool ready = false;
        bool firstload = true;

        void prepare_Tick(object sender, object e)
        {            
            prepare.Stop();          
            ///////////////
            cideranimstarter.Stop();
            cideranimstarter.Children.Clear();
            itsnavigated = false;
            timeout = false;
            lastappleanimended = true;
            applesanimended = true;
            rightbtntapped = false;
            leftbtntapped = false;
            failed = false;
            count = 0;
            fivefisrtapplesgone = false;
            score = 0;
            curscore.Text = score.ToString();
            apple1.Source = goodapple.Source;
            apple2.Source = goodapple.Source;
            apple3.Source = goodapple.Source;
            apple4.Source = goodapple.Source;
            apple5.Source = goodapple.Source;
            waitforchance = false;
            aj.Margin = new Thickness(0, 0, 0, 0);
            ///////////////

            curscore.FontSize = 0.12 * bck.ActualWidth;            
            

            ajsourcekickright.UriSource = new Uri("ms-appx:///Assets/ajkickingright.png");
            ajsourcestayright.UriSource = new Uri("ms-appx:///Assets/ajstayright.png");
            ajsourcekickleft.UriSource = new Uri("ms-appx:///Assets/ajkickingleft.png");
            ajsourcestayleft.UriSource = new Uri("ms-appx:///Assets/ajstayleft.png");
            
            basket.Width = bck.ActualWidth / 4;
            basket.Height = basket.Width;
            basket.Margin = new Thickness(0, 0, bck.ActualWidth * 3 / 4 - basket.Width / 2, 0);

            leftbtn.Width = bck.ActualWidth / 2;
            rightbtn.Width = leftbtn.Width;

            leftbtn.Height = bck.ActualHeight;
            rightbtn.Height = leftbtn.Height;

            aj.Width = bck.ActualWidth / 2;
            aj.Height = aj.Width;

            baskettop = 0.6 * bck.ActualHeight;

            apple1.Width = 0.45 * basket.Width;
            apple1.Height = apple1.Width;
            apple2.Width = 0.45 * basket.Width;
            apple2.Height = apple1.Width;
            apple3.Width = 0.45 * basket.Width;
            apple3.Height = apple1.Width;
            apple4.Width = 0.45 * basket.Width;
            apple4.Height = apple1.Width;
            apple5.Width = 0.45 * basket.Width;
            apple5.Height = apple1.Width;

            rightAppleRight = bck.ActualWidth / 4 - apple1.Width / 2;
            leftAppleRight = bck.ActualWidth * 3 / 4 - apple1.Width / 2;

            lastLeftAppleTop = 0.07 * bck.ActualHeight - apple1.Height / 2;
            lastRightAppleTop = 0.1 * bck.ActualHeight - apple1.Height / 2;

            distance = 0.19 * bck.ActualHeight;
            lastdistance = 0.37 * bck.ActualHeight - apple1.Width;
                        
            //////////apple1
            if (rand.Next(2) == 1)
            {
                apple1.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            else
            {
                apple1.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            apple1anim.To = distance;
            apple1anim.Duration = new TimeSpan(0, 0, 0, 0, 100);
            Storyboard.SetTarget(apple1anim, apple1);
            Storyboard.SetTargetProperty(apple1anim, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            //////////apple1

            //////////apple2
            if (rand.Next(2) == 1)
            {
                apple2.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            else
            {
                apple2.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            apple2anim.To = distance;
            apple2anim.Duration = new TimeSpan(0, 0, 0, 0, 100);
            Storyboard.SetTarget(apple2anim, apple2);
            Storyboard.SetTargetProperty(apple2anim, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            //////////apple2

            //////////apple3
            if (rand.Next(2) == 1)
            {
                apple3.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            else
            {
                apple3.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            apple3anim.To = distance;
            apple3anim.Duration = new TimeSpan(0, 0, 0, 0, 100);
            Storyboard.SetTarget(apple3anim, apple3);
            Storyboard.SetTargetProperty(apple3anim, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            //////////apple3

            //////////apple4
            if (rand.Next(2) == 1)
            {
                apple4.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            else
            {
                apple4.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            apple4anim.To = distance;
            apple4anim.Duration = new TimeSpan(0, 0, 0, 0, 100);
            Storyboard.SetTarget(apple4anim, apple4);
            Storyboard.SetTargetProperty(apple4anim, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            //////////apple4

            //////////apple5
            if (rand.Next(2) == 1)
            {
                apple5.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            else
            {
                apple5.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
                lastRightAppleTop -= distance;
                lastLeftAppleTop -= distance;
            }
            apple5anim.To = distance;
            apple5anim.Duration = new TimeSpan(0, 0, 0, 0, 100);
            Storyboard.SetTarget(apple5anim, apple5);
            Storyboard.SetTargetProperty(apple5anim, "(UIElement.RenderTransform).(TranslateTransform.Y)");
            //////////apple5

            lastLeftAppleTop = 0.07 * bck.ActualHeight - apple1.Height / 2 - distance;
            lastRightAppleTop = 0.1 * bck.ActualHeight - apple1.Height / 2 - distance;
                     
            aj.Source = ajsourcekickleft;
            aj.Source = ajsourcestayleft;
            aj.Source = ajsourcekickright;
            aj.Source = ajsourcestayright;           

            barrelContainer.Width = 0.2 * bck.ActualWidth; 
            barrelContainer.Height = 0.11 * bck.ActualHeight; 
            barrelContainer.Margin = new Thickness(0, curscore.ActualHeight + curscore.Margin.Top, 0, 0); 
            barrel.Width = 0.2 * bck.ActualWidth;
            barrel.Height = 0.11 * bck.ActualHeight; 

            barrelBackContainer.Width = 0.2 * bck.ActualWidth; 
            barrelBackContainer.Height = 0.11 * bck.ActualHeight; 
            barrelBackContainer.Margin = new Thickness(0, curscore.ActualHeight + curscore.Margin.Top, 0, 0); 
            barrelback.Width = 0.2 * bck.ActualWidth;
            barrelback.Height = 0.11 * bck.ActualHeight;

            ciderContainer.Width = 0.2 * bck.ActualWidth; 
            ciderContainer.Height = 0.11 * bck.ActualHeight -0.08 * barrelContainer.Height;
            ciderContainer.Margin = new Thickness(0, curscore.ActualHeight + curscore.Margin.Top, 0, 0);
            cider.Width = 0.2 * bck.ActualWidth;
            cider.Height = 0.11 * bck.ActualHeight - 0.08 * barrelContainer.Height;


            cideranim.To = 0; 
            cideranim.Duration = new TimeSpan(0, 0, 0,0,500); 
            Storyboard.SetTarget(cideranim, cider); 
            Storyboard.SetTargetProperty(cideranim, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)"); 
            cideranimstarter.Children.Add(cideranim);
            if (firstload)
            {
                firstload = false;
                readytmr.Interval = new TimeSpan(0,0,0,0,500);
                readytmr.Tick += readytmr_Tick;
                readytmr.Start();
            }
            else
                ready = true;
        }
        
        void readytmr_Tick(object sender, object e)
        {
            readytmr.Stop();
            apple1.Opacity = 1;
            apple2.Opacity = apple1.Opacity;
            apple3.Opacity = apple1.Opacity;
            apple4.Opacity = apple1.Opacity;
            apple5.Opacity = apple1.Opacity;
            aj.Opacity = 1;
            basket.Opacity = 1;
            tree.Opacity = 1;
            barrelBackContainer.Opacity = 1;
            barrelContainer.Opacity = 1;
            ciderContainer.Opacity = 1;
            curscore.Opacity = 1;
            bck.Background = bckimage.Background;
            ready = true;
        }
        
        private void leftbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!waitforchance && !timeout && ready)
            {
                if (!rightbtntapped && !leftbtntapped)
                {
                    cider.RenderTransform = null;
                    cideranimstarter.Stop();
                    cider.RenderTransform = new ScaleTransform();
                    cideranimstarter.Begin();
                    if (MainMenu.isSoundOn)
                    {
                        kick.Pause();
                        kick.Position = new TimeSpan(0, 0, 0, 0, 0);
                        kick.Play();
                    }
                    leftbtntapped = true;
                    aj.Margin = new Thickness(0, 0, bck.ActualWidth - aj.Width, 0);
                    basket.Margin = new Thickness(0, 0, bck.ActualWidth / 4 - basket.Width / 2, 0);                  
                    aj.Source = ajsourcekickleft;

                    apple1.RenderTransform = new TranslateTransform();
                    apple2.RenderTransform = new TranslateTransform();
                    apple3.RenderTransform = new TranslateTransform();
                    apple4.RenderTransform = new TranslateTransform();
                    apple5.RenderTransform = new TranslateTransform();


                    Storyboard lastappleanim = new Storyboard();
                    lastappleanim.Completed += lastappleanim_Completed;

                    DoubleAnimation lastapple = new DoubleAnimation();

                    lastapple.Duration = new TimeSpan(0, 0, 0, 0, 100);
                    lastapple.To = lastdistance;

                    if (apple1.Margin.Top > baskettop)
                    {
                        if (apple1.Margin.Right > bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple1);
                    }
                    else
                        applesanim.Children.Add(apple1anim);

                    if (apple2.Margin.Top > baskettop)
                    {
                        if (apple2.Margin.Right > bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple2);
                    }
                    else
                        applesanim.Children.Add(apple2anim);

                    if (apple3.Margin.Top > baskettop)
                    {
                        if (apple3.Margin.Right > bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple3);
                    }
                    else
                        applesanim.Children.Add(apple3anim);

                    if (apple4.Margin.Top > baskettop)
                    {
                        if (apple4.Margin.Right > bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple4);
                    }
                    else
                        applesanim.Children.Add(apple4anim);

                    if (apple5.Margin.Top > baskettop)
                    {
                        if (apple5.Margin.Right > bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple5);
                    }
                    else
                        applesanim.Children.Add(apple5anim);
                    
                    if (count != 3)
                        count++;
                    else
                    {
                        fivefisrtapplesgone = true;
                        Storyboard.SetTargetProperty(lastapple, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                        lastappleanim.Children.Add(lastapple);
                        if (failed && MainMenu.isSoundOn)
                            applebreak.Play();
                        lastappleanimended = false;
                        lastappleanim.Begin();
                    }
                    applesanimended = false;
                    applesanim.Begin();
                    }
            }
        }
        private void rightbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!waitforchance && !timeout)
            {
                if (!leftbtntapped && !rightbtntapped)
                {
                    cider.RenderTransform = null;
                    cideranimstarter.Stop();
                    cider.RenderTransform = new ScaleTransform();
                    cideranimstarter.Begin();
                    if (MainMenu.isSoundOn)
                    {
                        kick.Pause();
                        kick.Position = new TimeSpan(0, 0, 0, 0, 0);
                        kick.Play();
                    }
                    rightbtntapped = true;
                    aj.Margin = new Thickness(0, 0, 0, 0);
                    basket.Margin = new Thickness(0, 0, bck.ActualWidth * 3 / 4 - basket.Width / 2, 0);
                    aj.Source = ajsourcekickright;

                    apple1.RenderTransform = new TranslateTransform();
                    apple2.RenderTransform = new TranslateTransform();
                    apple3.RenderTransform = new TranslateTransform();
                    apple4.RenderTransform = new TranslateTransform();
                    apple5.RenderTransform = new TranslateTransform();

                    Storyboard lastappleanim = new Storyboard();
                    lastappleanim.Completed += lastappleanim_Completed;

                    DoubleAnimation lastapple = new DoubleAnimation();
                    lastapple.To = lastdistance;
                    lastapple.Duration = new TimeSpan(0, 0, 0, 0, 100);

                    if (apple1.Margin.Top > baskettop)
                    {
                        if (apple1.Margin.Right < bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple1);
                    }
                    else
                        applesanim.Children.Add(apple1anim);

                    if (apple2.Margin.Top > baskettop)
                    {
                        if (apple2.Margin.Right < bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple2);
                    }
                    else
                        applesanim.Children.Add(apple2anim);

                    if (apple3.Margin.Top > baskettop)
                    {
                        if (apple3.Margin.Right < bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple3);
                    }
                    else
                        applesanim.Children.Add(apple3anim);

                    if (apple4.Margin.Top > baskettop)
                    {
                        if (apple4.Margin.Right < bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple4);
                    }
                    else
                        applesanim.Children.Add(apple4anim);

                    if (apple5.Margin.Top > baskettop)
                    {
                        if (apple5.Margin.Right < bck.ActualWidth / 2)
                            failed = true;
                        Storyboard.SetTarget(lastapple, apple5);
                    }
                    else
                        applesanim.Children.Add(apple5anim);                   
                    if (count != 3)
                        count++;
                    else
                    {
                        fivefisrtapplesgone = true;
                        Storyboard.SetTargetProperty(lastapple, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                        lastappleanim.Children.Add(lastapple);   
                        if (failed && MainMenu.isSoundOn)
                            applebreak.Play();
                        lastappleanim.Begin();
                    }
                    applesanim.Begin();
                }
            }
        }
        void applesanim_Completed(object sender, object e)
        {
            if (applesanim.Children.Contains(apple1anim))
            {               
                apple1.RenderTransform = null;
                apple1.Margin = new Thickness(0, apple1.Margin.Top + distance, apple1.Margin.Right, 0);
            }
            if (applesanim.Children.Contains(apple2anim))
            {
                apple2.Margin = new Thickness(0, apple2.Margin.Top + distance, apple2.Margin.Right, 0);
                apple2.RenderTransform = null;
            }
            if (applesanim.Children.Contains(apple3anim))
            {
                apple3.RenderTransform = null;
                apple3.Margin = new Thickness(0, apple3.Margin.Top + distance, apple3.Margin.Right, 0);
            }
            if (applesanim.Children.Contains(apple4anim))
            {
                apple4.RenderTransform = null;
                apple4.Margin = new Thickness(0, apple4.Margin.Top + distance, apple4.Margin.Right, 0);
            }
            if (applesanim.Children.Contains(apple5anim))
            {
                apple5.RenderTransform = null;
                apple5.Margin = new Thickness(0, apple5.Margin.Top + distance, apple5.Margin.Right, 0);
            }
            if (!fivefisrtapplesgone)
            {
                if (rightbtntapped && !leftbtntapped)
                {
                    rightbtntapped = false;
                    aj.Source = ajsourcestayright;
                }
                if (leftbtntapped && !rightbtntapped)
                {
                    leftbtntapped = false;
                    aj.Source = ajsourcestayleft;
                }
                applesanim.Stop();
                applesanim.Children.Clear();
            }
            applesanimended = true;
        }

        void lastappleanim_Completed(object sender, object e)
        {
            lastappleanimended = true;
            if (!applesanim.Children.Contains(apple1anim))
            {

                apple1.RenderTransform = null;
                apple1.Margin = new Thickness(0, bck.ActualHeight - apple1.Width, apple1.Margin.Right, 0);
                applesanim.Stop();
                applesanim.Children.Clear();
                if (!failed && !timeout)
                {
                    spawn(apple1);
                    score++;
                    curscore.Text = score.ToString();
                }
                else
                {
                    if (rightbtntapped && !leftbtntapped)
                    {
                        rightbtntapped = false;
                        aj.Source = ajsourcestayright;
                    }
                    if (leftbtntapped && !rightbtntapped)
                    {
                        leftbtntapped = false;
                        aj.Source = ajsourcestayleft;
                    }
                    apple1.Source = applefailed.Source; waitforchance = true; cideranimstarter.Pause(); showtmr.Start();
                }
            }
            else
                if (!applesanim.Children.Contains(apple2anim))
                {
                    apple2.Margin = new Thickness(0, bck.ActualHeight - apple1.Width, apple2.Margin.Right, 0);
                    apple2.RenderTransform = null;
                    applesanim.Stop();
                    applesanim.Children.Clear();
                    if (!failed && !timeout)
                    {
                        spawn(apple2);
                        score++;
                        curscore.Text = score.ToString();
                    }
                    else
                    {
                        if (rightbtntapped && !leftbtntapped)
                        {
                            rightbtntapped = false;
                            aj.Source = ajsourcestayright;
                        }
                        if (leftbtntapped && !rightbtntapped)
                        {
                            leftbtntapped = false;
                            aj.Source = ajsourcestayleft;
                        }

                        apple2.Source = applefailed.Source; waitforchance = true; cideranimstarter.Pause(); showtmr.Start();
                    }
                }
                else
                    if (!applesanim.Children.Contains(apple3anim))
                    {
                        apple3.RenderTransform = null;
                        apple3.Margin = new Thickness(0, bck.ActualHeight - apple1.Width, apple3.Margin.Right, 0);
                        applesanim.Stop();
                        applesanim.Children.Clear();
                        if (!failed && !timeout)
                        {
                            spawn(apple3);
                            score++;
                            curscore.Text = score.ToString();
                        }
                        else
                        {
                            if (rightbtntapped && !leftbtntapped)
                            {
                                rightbtntapped = false;
                                aj.Source = ajsourcestayright;
                            }
                            if (leftbtntapped && !rightbtntapped)
                            {
                                leftbtntapped = false;
                                aj.Source = ajsourcestayleft;
                            }
                            apple3.Source = applefailed.Source; waitforchance = true; cideranimstarter.Pause(); showtmr.Start();
                        }
                    }
                    else
                        if (!applesanim.Children.Contains(apple4anim))
                        {
                            apple4.RenderTransform = null;
                            apple4.Margin = new Thickness(0, bck.ActualHeight - apple1.Width, apple4.Margin.Right, 0);
                            applesanim.Stop();
                            applesanim.Children.Clear();
                            if (!failed && !timeout)
                            {
                                spawn(apple4);
                                score++;
                                curscore.Text = score.ToString();
                            }
                            else
                            {
                                if (rightbtntapped && !leftbtntapped)
                                {
                                    rightbtntapped = false;
                                    aj.Source = ajsourcestayright;
                                }
                                if (leftbtntapped && !rightbtntapped)
                                {
                                    leftbtntapped = false;
                                    aj.Source = ajsourcestayleft;
                                }

                                apple4.Source = applefailed.Source; waitforchance = true; cideranimstarter.Pause(); showtmr.Start();
                            }
                        }
                        else
                            if (!applesanim.Children.Contains(apple5anim))
                            {
                                apple5.RenderTransform = null;
                                apple5.Margin = new Thickness(0, bck.ActualHeight - apple1.Width, apple5.Margin.Right, 0);
                                applesanim.Stop();
                                applesanim.Children.Clear();
                                if (!failed && !timeout)
                                {
                                    spawn(apple5);
                                    score++;
                                    curscore.Text = score.ToString();
                                }
                                else
                                {
                                    if (rightbtntapped && !leftbtntapped)
                                    {
                                        rightbtntapped = false;
                                        aj.Source = ajsourcestayright;
                                    }
                                    if (leftbtntapped && !rightbtntapped)
                                    {
                                        leftbtntapped = false;
                                        aj.Source = ajsourcestayleft;
                                    }

                                    apple5.Source = applefailed.Source; waitforchance = true; cideranimstarter.Pause(); showtmr.Start();
                                }
                            }
        }
        void spawn(Image someapple)
        {
            if (rand.Next(2) == 1)
            {
                someapple.Margin = new Thickness(0, lastRightAppleTop, rightAppleRight, 0);
            }
            else
            {
                someapple.Margin = new Thickness(0, lastLeftAppleTop, leftAppleRight, 0);
            }

            if (!rightbtntapped && leftbtntapped)
            {
                leftbtntapped = false;
                aj.Source = ajsourcestayleft;
            }
            if (!leftbtntapped && rightbtntapped)
            {
                rightbtntapped = false;
                aj.Source = ajsourcestayright;
            }
        }       
        void showtmr_Tick(object sender, object e)
        {
            showtmr.Stop();
            showtmr.Tick -= showtmr_Tick;           
            prepare.Tick -= prepare_Tick;
            applesanim.Completed -= applesanim_Completed;
            itsnavigated = true;
            Frame.Navigate(typeof(TryAgainPage), bck.ActualHeight.ToString());          
        }       
    }
}
