using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Command;
using VectorGraphicViewer.UI.Model;
using Line = VectorGraphicViewer.UI.Model.Line;
using Point = System.Windows.Point;

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

        private readonly ObservableCollection<Path> _shapes;

        public IEnumerable<Path> Shapes => _shapes;

        public bool HasShapes => _shapes.Any();

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        private void MainButtonClick()
        {
            DrawCartesianCoordinates();
        }

        public CartesianPlaneViewModel()
        {
            _shapes = new ObservableCollection<Path>();
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

            _shapes.Add(new Line(new Point(CanvasWidth / 2, 0),
                new Point(CanvasWidth / 2, CanvasHeight), Color.Black).GetShape());

            _shapes.Add(new Line(new Point(0, CanvasHeight / 2),
                new Point(CanvasWidth, CanvasHeight / 2), Color.Black).GetShape());

            DrawShapes();
        }

        private void DrawShapes()
        {
            //myLine = new System.Windows.Shapes.Line();
            //myLine.Stroke = Brushes.Black;
            //myLine.X1 = 0;
            //myLine.X2 = CanvasWidth;
            //myLine.Y1 = CanvasHeight / 2;
            //myLine.Y2 = CanvasHeight / 2;
            //myLine.StrokeThickness = 2;
            //_shapes.Add(myLine);

            // line
            //LineGeometry line = new LineGeometry();
            //line.StartPoint = new System.Windows.Point(100, 150);
            //line.EndPoint = new System.Windows.Point(200, 250);
            //Path path = new Path();
            //path.Stroke = Brushes.Red;
            //path.StrokeThickness = 10;
            //path.Data = line;
            //_shapes.Add(path);

            //var test =
            //    new Model.Line(new System.Windows.Point(100, 150), new System.Windows.Point(200, 250), Color.Aqua);
            //_shapes.Add(test.GetShape());

            //// eliptical
            ////var ellipse = new EllipseGeometry();
            ////ellipse.Center = new Point(150, 150);
            ////ellipse.RadiusX = 60;
            ////ellipse.RadiusY = 40;
            ////Path path2 = new Path();
            ////path2.Stroke = Brushes.Red;
            ////path2.Data = ellipse;
            ////_shapes.Add(path2);

            //LineGeometry line1 = new LineGeometry();
            //line1.StartPoint = new Point(75, 80);
            //line1.EndPoint = new Point(125, 150);
            //LineGeometry line2 = new LineGeometry();
            //line2.StartPoint = new Point(125, 150);
            //line2.EndPoint = new Point(150, 120);
            //LineGeometry line3 = new LineGeometry();
            //line3.StartPoint = new Point(150, 120);
            //line3.EndPoint = new Point(75, 80);
            //GeometryGroup geometryGroup = new GeometryGroup();
            //geometryGroup.Children.Add(line1);
            //geometryGroup.Children.Add(line2);
            //geometryGroup.Children.Add(line3);
            //Path path3 = new Path();
            //path3.Stroke = Brushes.BurlyWood;
            //path3.StrokeThickness = 10;
            //path3.Data = geometryGroup;
            //_shapes.Add(path3);

            var test = new Triangle(new Point(75, 80), new Point(125, 150), new Point(150, 120), new Color());

            //var test = new Quadrilateral(new Point(75, 80),
            //    new Point(125, 150),
            //    new Point(150, 120),
            //         new Point(200, 220),
            //    Color.Red
            //    );

            _shapes.Add(test.GetShape());
        }
    }
}