using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private double _canvasWidth;
        private double _canvasHeight;
        private List<IShape> _shapes;
        private ObservableCollection<Shape> _scaledShapes;
        private readonly IReadService _readService;
        private readonly IDrawService _drawService;

        #endregion

        public ObservableCollection<Shape> ScaledShapes
        {
            get => _scaledShapes;
            set
            {
                _scaledShapes = value;
                OnPropertyChanged(nameof(ScaledShapes));
            }
        }

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public string DestinationPath { get; set; }

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
            
            ScaledShapes.Clear();
            DrawScaledShapes();

        }

        private void ReadButtonClick()
        {
            DrawScaledShapes();
            ReadShapes(DestinationPath);
            DrawScaledShapes();

            //DrawCartesianCoordinates();
        }

        public CartesianPlaneViewModel()
        {
            _readService = new ReadService();
            _drawService = new DrawService();
            _scaledShapes = new ObservableCollection<Shape>();
            ReadCommand = new RelayCommand(o => ReadButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());
        }

        private void ClearButtonClick()
        {
            ScaledShapes.Clear();
            DrawScaledShapes();
            DestinationPath = string.Empty;
            OnPropertyChanged(nameof(DestinationPath));
        }


        private void DrawScaledShapes()
        {
            var shapes = _drawService.GetScaledShapes(_shapes, new Point(CanvasWidth, CanvasHeight));
            shapes.AddRange(_scaledShapes);
            ScaledShapes = new ObservableCollection<Shape>(shapes);
        }
    }
}