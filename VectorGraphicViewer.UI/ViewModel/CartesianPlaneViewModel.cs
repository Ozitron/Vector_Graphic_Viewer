using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
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
        private readonly IDrawService _drawService;
        private double _canvasWidth;
        private double _canvasHeight;
        private ObservableCollection<Shape> _scaledShapes;
        private List<IShape> _shapes;
        private List<Shape> _getShapes;

        #endregion

        public IEnumerable<Shape> ScaledShapes => _scaledShapes;
        public bool HasShapes => _scaledShapes.Any();
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
            _shapes = _readService.Read(filePath);
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
            _drawService = new DrawService();
            _scaledShapes = new ObservableCollection<Shape>();
            ReadCommand = new RelayCommand(o => MainButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());

            _scaledShapes.CollectionChanged += OnScaledShapesChanged;
        }

        private void ClearButtonClick()
        {
            _scaledShapes.Clear();
            DrawCartesianCoordinates();
            DestinationPath = string.Empty;
            OnPropertyChanged(nameof(DestinationPath));
        }

        private void OnScaledShapesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasShapes));
        }

        private void DrawCartesianCoordinates()
        {
            _scaledShapes.Clear();
            _scaledShapes.Add(new Line
            {
                // x line
                Stroke = Brushes.Black,
                X1 = CanvasWidth / 2,
                X2 = CanvasWidth / 2,
                Y1 = 0,
                Y2 = CanvasHeight,
                StrokeThickness = 2
            });

            _scaledShapes.Add(new Line
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
            _getShapes = _drawService.GetScaledShapes(_shapes, new Point(CanvasWidth, CanvasHeight));


            foreach (var shape in _getShapes)
            {
                _scaledShapes.Add(shape);
            }

             
        }
    }
}