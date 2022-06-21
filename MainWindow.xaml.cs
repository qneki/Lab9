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
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;


namespace imgeditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        Ellipse Point = new Ellipse();
        Ellipse Ellipse = new Ellipse();
        Rectangle rect = new Rectangle();
        Line Line = new Line();
        bool line = false;
        bool point = false;
        bool rectangle = false;
        bool ellipse = false;


        public MainWindow()
        {
            InitializeComponent();

        }

        private bool isDirty = false;
        private void txt_TextChanged(object sender, RoutedEventArgs e)
        {
            isDirty = true;
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            img.Source = null;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void mnuopen_click(object sender, RoutedEventArgs e)
        {

            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
               "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
               "Portable Network Graphic (*.png)|*.png";

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                img.Source = new BitmapImage(fileUri);


            }
        }
        private void mnusave_as_click(object sender, RoutedEventArgs e)
        {

            SaveToPng(img, "image.png");

        }
        private void mnusave_click(object sender, RoutedEventArgs e)
        {
            SaveToPng(img, "image.png");
        }
        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }
        private void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }
        private void mnuexit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
           
        }
        private void mnupoint_click(object sender, RoutedEventArgs e)
        {
            point = true;
            rectangle = false;
            ellipse = false;
            line = false;
        }
        private void mnurect_click(object sender, RoutedEventArgs e)
        {
            rectangle = true;
            point = false;
            ellipse = false;
            line = false;
        }
        private void mnuline_click(object sender, RoutedEventArgs e)
        {
            line = true;
            rectangle = false;
            point = false;
            ellipse = false;
        }
        private void mnuellipse_click(object sender, RoutedEventArgs e)
        {
            ellipse = true;
            rectangle = false;
            point = false;
            line = false;
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
            
            Point.Fill = Brushes.Sienna;
            Point.Width = 30;
            Point.Height = 30;
            Point.StrokeThickness = 2;

            rect.Fill = Brushes.Blue;
            rect.Width = 60;
            rect.Height = 60;
            rect.StrokeThickness = 2;

            Ellipse.Fill = Brushes.Red;
            Ellipse.Width = 100;
            Ellipse.Height = 100;
            Ellipse.StrokeThickness = 2;

            
            Line.X1 = 50;
            Line.Y1 = 50;
            Line.X2 = 200;
            Line.Y2 = 200;
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            Line.StrokeThickness = 2;
            Line.Stroke = redBrush;


            if (point == true)
            {

                    Cnv.Children.Add(Point);
                    Canvas.SetLeft(Point, e.GetPosition(img).X);
                    Canvas.SetTop(Point, e.GetPosition(img).Y);

            }
                else if (rectangle == true)
                {
                    Cnv.Children.Add(rect);
                    Canvas.SetLeft(rect, e.GetPosition(img).X);
                    Canvas.SetTop(rect, e.GetPosition(img).Y);
                }
                else if (ellipse == true)
                {

                    Cnv.Children.Add(Ellipse);
                    Canvas.SetLeft(Ellipse, e.GetPosition(img).X);
                    Canvas.SetTop(Ellipse, e.GetPosition(img).Y);

                }else if(line == true)
            {
                Cnv.Children.Add(Line);
                Canvas.SetLeft(Line, e.GetPosition(img).X);
                Canvas.SetTop(Line, e.GetPosition(img).Y);
            }
            
            

        }

    }
}
