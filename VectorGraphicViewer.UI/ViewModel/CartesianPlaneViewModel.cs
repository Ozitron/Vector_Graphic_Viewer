using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Business.Service;
using VectorGraphicViewer.UI.Command;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.ViewModel
{
    internal class CartesianPlaneViewModel : ViewModelBase
    {
        #region fields

        private double _canvasWidth;
        private double _canvasHeight;
        private List<IShape> _shapes;
        private ObservableCollection<object> _scaledShapes;
        private readonly IReadService _readService;
        private readonly IDrawService _drawService;
        private bool _isScaled;

        #endregion

        public ObservableCollection<object> ScaledShapes
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
                if (value > 0 && _isScaled)
                    OnGridChanged(nameof(CanvasWidth));

                _isScaled = !_isScaled;
            }
        }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                if (value > 0 && _isScaled)
                    OnGridChanged(nameof(CanvasHeight));

                _isScaled = !_isScaled;
            }
        }

        private void ReadShapes(string filePath)
        {
            _shapes = _readService.Read(filePath);
        }

        private void OnGridChanged(string name)
        {
            ScaledShapes.Clear();
            DrawScaledShapes();
        }

        private void ReadButtonClick()
        {
            DrawScaledShapes();
            ReadShapes(DestinationPath);
            DrawScaledShapes();
            
        }

        public CartesianPlaneViewModel()
        {
            _readService = new ReadService();
            _drawService = new DrawService();
            _scaledShapes = new ObservableCollection<object>();
            ReadCommand = new RelayCommand(o => ReadButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());
        }

        private void ClearButtonClick()
        {
            if (_shapes != null) _shapes.Clear();
            ScaledShapes.Clear();
            DrawScaledShapes();
            DestinationPath = string.Empty;
            OnPropertyChanged(nameof(DestinationPath));
        }


        private void DrawScaledShapes()
        {
            ScaledShapes.Clear();
            var shapes = _drawService.GetScaledShapes(_shapes, new Point(CanvasWidth, CanvasHeight));
            shapes.AddRange(_scaledShapes);
            ScaledShapes = new ObservableCollection<object>(shapes);
        }
    }
}