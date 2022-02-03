using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Business.Service;
using VectorGraphicViewer.UI.Command;
using VectorGraphicViewer.UI.Model.Base;
using Line = System.Windows.Shapes.Line;

namespace VectorGraphicViewer.UI.ViewModel
{
    internal class CartesianPlaneViewModel : ViewModelBase
    {
        #region fields
        
        private string _destinationPath;
        private readonly IReadService _readService;
        private double _canvasWidth;
        private double _canvasHeight;
        private readonly ObservableCollection<Shape> _shapes;
        private IEnumerable<IShape> _shapeList;

        #endregion
        
        public IEnumerable<Shape> Shapes => _shapes;
        public bool HasShapes => _shapes.Any();
        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public string DestinationPath
        {
            get => _destinationPath;
            set
            {
                _destinationPath = value;
                OnPropertyChanged(nameof(DestinationPath));

                if (!string.IsNullOrEmpty(_destinationPath))
                    ReadShapes(DestinationPath);
            }
        }
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

        private void ReadShapes(string filePath)
        {
            _shapeList = _readService.Read(filePath);
        }

        private void OnGridChanged(string name)
        {
            OnPropertyChanged(name);

            if (CanvasHeight > 0)
                DrawCartesianCoordinates();
        }

        private void MainButtonClick()
        {
            DrawShapes();
            //DrawCartesianCoordinates();
        }

        public CartesianPlaneViewModel()
        {
            _readService = new ReadService();
            _shapes = new ObservableCollection<Shape>();
            ReadCommand = new RelayCommand(o => MainButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());

            _shapes.CollectionChanged += OnShapesChanged;
        }

        private void ClearButtonClick()
        {
            _shapes.Clear();
            DrawCartesianCoordinates();
            DestinationPath = string.Empty;
            OnPropertyChanged(nameof(DestinationPath));
        }

        private void OnShapesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasShapes));
        }

        private void DrawCartesianCoordinates()
        {
            _shapes.Clear();
            _shapes.Add(new Line
            {
                // x line
                Stroke = Brushes.Black,
                X1 = CanvasWidth / 2,
                X2 = CanvasWidth / 2,
                Y1 = 0,
                Y2 = CanvasHeight,
                StrokeThickness = 2
            });

            _shapes.Add(new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                X2 = CanvasWidth,
                Y1 = CanvasHeight / 2,
                Y2 = CanvasHeight / 2,
                StrokeThickness = 2
            });

            //DrawShapes();
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