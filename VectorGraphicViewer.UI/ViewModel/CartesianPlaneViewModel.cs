using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Command;
using Line = System.Windows.Shapes.Line;

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
                if (value > 0)
                    OnGridChanged(nameof(CanvasWidth));
            }
        }

        private void OnGridChanged(string name)
        {
            OnPropertyChanged(name);
            DrawCartesianCoordinates();
        }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                if (value > 0)
                    OnGridChanged(nameof(CanvasHeight));
            }
        }

        private readonly ObservableCollection<Shape> _shapes;

        public IEnumerable<Shape> Shapes => _shapes;

        public bool HasShapes => _shapes.Any();

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        private void MainButtonClick()
        {
            DrawCartesianCoordinates();
        }

        public CartesianPlaneViewModel()
        {
            _shapes = new ObservableCollection<Shape>();
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

        private void DrawCartesianCoordinates()
        {
            _shapes.Clear();

            var xLine = new Line();
            xLine.Stroke = Brushes.Black;
            xLine.X1 = CanvasWidth / 2;
            xLine.X2 = CanvasWidth / 2;
            xLine.Y1 = 0;
            xLine.Y2 = CanvasHeight;
            xLine.StrokeThickness = 2;
            _shapes.Add(xLine);

            var yLine = new Line();
            yLine.Stroke = Brushes.Black;
            yLine.X1 = 0;
            yLine.X2 = CanvasWidth;
            yLine.Y1 = CanvasHeight / 2;
            yLine.Y2 = CanvasHeight / 2;
            yLine.StrokeThickness = 2;
            _shapes.Add(yLine);

            DrawShapes();
        }

        private void DrawShapes()
        {
            //// Polygon
            //var p = new Polygon();
            //p.Stroke = Brushes.Black;
            //p.Fill = Brushes.LightBlue;
            //p.StrokeThickness = 2;
            //p.Points = new PointCollection() { new Point(100, 100), new Point(45, 68), new Point(102, 135), new Point(220, 256) };
            //_shapes.Add(p);

            //// Elliptical
            //var ellipse = new EllipseGeometry();
            //ellipse.Center = new Point(150, 150);
            //ellipse.RadiusX = 60;
            //ellipse.RadiusY = 40;
            //var path2 = new Path();
            //path2.Stroke = Brushes.Red;
            //path2.Data = ellipse;
            //_shapes.Add(path2);
        }
    }
}