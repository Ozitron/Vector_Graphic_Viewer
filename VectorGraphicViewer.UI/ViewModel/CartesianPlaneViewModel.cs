using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Command;
namespace VectorGraphicViewer.UI.ViewModel
{
    internal class CartesianPlaneViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Polygon> _shapes;

        public IEnumerable<Polygon> Shapes => _shapes;

        public bool HasReservations => _shapes.Any();

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        private void MainButtonClick()
        {
            //var points = new Point[] { new Point(100, 200), new Point(150, 250), new Point(50, 100) };

            //pointz = new List<Point>() { new Point(50, 100), new Point(150, 200), new Point(200, 250) };

            //var myLine = new Line();
            //myLine.Stroke = Brushes.LightSteelBlue;
            //myLine.X1 = 50;
            //myLine.X2 = 100;
            //myLine.Y1 = 150;
            //myLine.Y2 = 200;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //myLine.StrokeThickness = 2;
            //_shapes.Add(myLine);

            //myLine = new Line();
            //myLine.Stroke = Brushes.Black;
            //myLine.X1 = 50;
            //myLine.X2 = 100;
            //myLine.Y1 = 250;
            //myLine.Y2 = 300;
            //myLine.StrokeThickness = 2;
            //_shapes.Add(myLine);

            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(new Point(-15, -20));
            myPointCollection.Add(new Point(15, -20));
            myPointCollection.Add(new Point(0, 21));
            myPointCollection.Add(new Point(0, 30));
            myPointCollection.Add(new Point(-23, 10));

            Polygon myPolygon = new Polygon();
            myPolygon.Points = myPointCollection;
            myPolygon.Fill = Brushes.Blue;
            myPolygon.Width = 200;
            myPolygon.Height = 200;
            myPolygon.Stretch = Stretch.Fill;
            myPolygon.Stroke = Brushes.Black;
            myPolygon.StrokeThickness = 2;

            _shapes.Add(myPolygon);
        }
        
        public CartesianPlaneViewModel()
        {
            _shapes = new ObservableCollection<Polygon>();
            ReadCommand = new RelayCommand(o => MainButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());

            _shapes.CollectionChanged += OnShapesChanged;
        }

        private void ClearButtonClick()
        {
            _shapes.Clear();
        }

        private void OnShapesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasReservations));
        }
    }

    public class WindowBehaviors
    {

        public static double GetActualWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ActualWidthProperty);
        }

        public static void SetActualWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ActualWidthProperty, value);
        }

        public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.RegisterAttached("ActualWidth", typeof(double), typeof(WindowBehaviors), new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(ActualWidthChanged)) { BindsTwoWayByDefault = true });


        private static void ActualWidthChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Window w = source as Window;
            if (w != null)
            {
                w.SizeChanged += (se, ev) =>
                {
                    SetActualWidth((DependencyObject)se, ev.NewSize.Width);
                };
            }
        }
    }
}