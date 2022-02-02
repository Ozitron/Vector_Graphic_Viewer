using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Command;

namespace VectorGraphicViewer.UI.ViewModel
{
    internal class CartesianPlaneViewModel : ViewModelBase
    {
        private double _canvasWidth;
        private double _canvasHeight;

        public double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                _canvasWidth = value;
                OnGridChanged(nameof(CanvasWidth));

            }
        }

        private void OnGridChanged(string name)
        {
            OnPropertyChanged(name);
            DrawCartesian();
        }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                OnGridChanged(nameof(CanvasHeight));
            }
        }

        private readonly ObservableCollection<Line> _shapes;

        public IEnumerable<Line> Shapes => _shapes;

        public bool HasShapes => _shapes.Any();

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        

        private void MainButtonClick()
        {
            DrawCartesian();
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

            //PointCollection myPointCollection = new PointCollection();
            //myPointCollection.Add(new Point(-15, -20));
            //myPointCollection.Add(new Point(15, -20));
            //myPointCollection.Add(new Point(0, 21));
            //myPointCollection.Add(new Point(0, 30));
            //myPointCollection.Add(new Point(-23, 10));

            //Polygon myPolygon = new Polygon();
            //myPolygon.Points = myPointCollection;
            //myPolygon.Fill = Brushes.Blue;
            //myPolygon.Width = 200;
            //myPolygon.Height = 200;
            //myPolygon.Stretch = Stretch.Fill;
            //myPolygon.Stroke = Brushes.Black;
            //myPolygon.StrokeThickness = 2;

            //_shapes.Add(myPolygon);
        }

        public CartesianPlaneViewModel()
        {
            _shapes = new ObservableCollection<Line>();
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
            OnPropertyChanged(nameof(HasShapes));
        }

        private void DrawCartesian()
        {
            _shapes.Clear();

            //Font drawFont = new Font("Calibri", 9);
            //SolidBrush drawBrush = new SolidBrush(Color.Black);

            //// Draw cartesian coordinate plane central lines
            //_graphics.DrawLine(_pen, 0, _center.Y, _pictureBox.Width, _center.Y);
            //_graphics.DrawLine(_pen, _center.X, 0, _center.X, _pictureBox.Height);
            //_pen.DashStyle = DashStyle.Dot;


            var myLine = new Line();
            myLine.Stroke = Brushes.Black;
            myLine.X1 = CanvasWidth / 2;
            myLine.X2 = CanvasWidth / 2;
            myLine.Y1 = 0;
            myLine.Y2 = CanvasHeight;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            _shapes.Add(myLine);

            myLine = new Line();
            myLine.Stroke = Brushes.Black;
            myLine.X1 = 0;
            myLine.X2 = CanvasWidth;
            myLine.Y1 = CanvasHeight / 2;
            myLine.Y2 = CanvasHeight / 2;
            myLine.StrokeThickness = 2;
            _shapes.Add(myLine);

            // Draw X line and print scaled line numbers
            //int count = 1;
            //while (count * interval < _pictureBox.Width / 2.0)
            //{
            //    // draw vertical dotted lines
            //    _graphics.DrawLine(_pen, _center.X - count * interval, 0, _center.X - count * interval, _pictureBox.Height);
            //    _graphics.DrawLine(_pen, _center.X + count * interval, 0, _center.X + count * interval, _pictureBox.Height);

            //    // print line X numbers
            //    number = (count * interval / _scaleFactor);
            //    _graphics.DrawString($"{number:0.0}", drawFont, drawBrush, (_center.X + (count * interval)) - 15, _center.Y);
            //    _graphics.DrawString($"-{number:0.0}", drawFont, drawBrush, (_center.X - (count * interval)) - 15, _center.Y);

            //    count++;
            //}

            //// Draw Y line and print scaled line numbers
            //count = 1;
            //while (count * interval < _pictureBox.Height / 2.0)
            //{
            //    // draw horizontal dotted lines
            //    _graphics.DrawLine(_pen, 0, _center.Y - (count * interval), _pictureBox.Width, _center.Y - (count * interval));
            //    _graphics.DrawLine(_pen, 0, _center.Y + (count * interval), _pictureBox.Width, _center.Y + (count * interval));

            //    // print line Y numbers
            //    number = (count * interval / _scaleFactor);
            //    _graphics.DrawString($"{number:0.0}", drawFont, drawBrush, _center.X, _center.Y - (count * interval) - 12);
            //    _graphics.DrawString($"-{number:0.0}", drawFont, drawBrush, _center.X, _center.Y + (count * interval) - 12);

            //    count++;
            //}
        }
    }
}