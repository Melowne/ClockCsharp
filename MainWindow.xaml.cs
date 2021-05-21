using System;
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
using System.Windows.Threading;

namespace auhr3
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        Line sec, min, hour; List<Label> lis = new List<Label>();
        Ellipse circle = new Ellipse();
        public MainWindow()
        {

            InitializeComponent(); this.Topmost = true; circle.Width = 100; circle.Height = 100;

            sec = new Line(); min = new Line(); hour = new Line();
            grid.Children.Add(sec);
            grid.Children.Add(min);
            grid.Children.Add(hour);

            Panel.SetZIndex(sec, 2); Panel.SetZIndex(min, 2); Panel.SetZIndex(hour, 2); nums();
            timer = new DispatcherTimer(); timer.Tick += Timer_Tick; timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        //fillNums
        private void nums()
        {
            int grade = 120; double x, y;
            Label num = new Label();
            for (int i = 1; i <= 12; i++)
            {
                num = new Label(); num.Foreground = Brushes.Black;
                x = circle.Height * 2 - 8 - Math.Cos(grade * Math.PI / 180) * (2 * circle.Height - 20);
                y = circle.Height * 2 - 5 - Math.Sin(grade * Math.PI / 180) * (2 * circle.Height - 20);
                num.Content = i;
                num.Margin = new Thickness(x, y, 0, 0); grade += 30;
                Panel.SetZIndex(num, 1);
                lis.Add(num);
                grid.Children.Add(lis[i - 1]);
            }
        }
        //updateNumPositions
        private void lischange(List<Label> l)
        {
            double x, y, fz = 0, radius = circle.Height / 2.20, rady = 0; int grade1 = 120;
            for (int i = 0; i < l.Count; i++)
            {
                if (this.Height > 300 && this.Width > 300)
                {
                    fz = 18; rady = 20;
                    radius = circle.Height / 2 - 30;
                }
                else { fz = 14; rady = 12; radius = circle.Height / 2 - 20; }
                x = circle.Height / 2 - rady + 5 - Math.Cos(grade1 * Math.PI / 180) * radius;
                y = circle.Height / 2 - rady - Math.Sin(grade1 * Math.PI / 180) * radius;
                lis[i].Margin = new Thickness(x, y, 0, 0); grade1 += 30;
                lis[i].Foreground = Brushes.Black; lis[i].FontSize = circle.Height / fz;

            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timecheck();
        }
        //updateClockHands
        private void timecheck()
        {
            DateTime time = DateTime.Now;
            makepointer(sec, (int)win.Height / 55, Colors.WhiteSmoke, circle.Height / 2 - Math.Cos((90 + time.Second * 6) * Math.PI / 180) * (circle.Height / 2.4)
             , circle.Height / 2 - Math.Sin((90 + time.Second * 6) * Math.PI / 180) * (circle.Height / 2.4));
            makepointer(min, (int)win.Height / 50, Colors.DimGray, circle.Height / 2 - Math.Cos((90 + time.Minute * 6) * Math.PI / 180) * (circle.Height / 2.3)
             , circle.Height / 2 - Math.Sin((90 + time.Minute * 6) * Math.PI / 180) * (circle.Height / 2.3));
            makepointer(hour, (int)win.Height / 45, Colors.LightGray, circle.Height / 2 - Math.Cos((90 + (time.Hour % 12) * 30 + time.Minute / 2) * Math.PI / 180) * (circle.Height / 3.5)
             , circle.Height / 2 - Math.Sin((90 + (time.Hour % 12) * 30 + time.Minute / 2) * Math.PI / 180) * (circle.Height / 3.5));
        }
        //makeClockhands
        private void makepointer(Line l, int size, Color col, double x1, double y1)
        {
            l.Width = circle.Height; l.Height = circle.Height;
            l.X2 = circle.Height / 2; l.Y2 = circle.Height / 2;
            l.Stroke = new SolidColorBrush(col);
            l.X1 = x1;
            l.Y1 = y1;
            l.StrokeThickness = size;
        }
        //aktualisierung fenstergroesse
        ImageBrush imgBrush = new ImageBrush();
        private void win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var pngUri = new Uri("pack://application:,,,/auhr3;component/seph.png");
            imgBrush.ImageSource = new BitmapImage(pngUri); imgBrush.Stretch = Stretch.Fill;
            DateTime time = DateTime.Now;
            if (win.Height > 100)
            {
                grid.Children.Remove(circle);
                if (win.Width > 180 && win.Height > 180) { circle.Width = win.Height - 40; circle.Height = win.Height - 40; }
                imgBrush.Opacity = 0.8; circle.Fill = imgBrush; ; grid.Children.Add(circle);
                timecheck();
                lischange(lis);
            }
        }
    }
}
