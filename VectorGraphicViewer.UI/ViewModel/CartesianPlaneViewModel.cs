using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using VectorGraphicViewer.Business;
using VectorGraphicViewer.Business.Service;
using VectorGraphicViewer.Command;
using VectorGraphicViewer.Model.Base;

namespace VectorGraphicViewer.ViewModel
{
    public class CartesianPlaneViewModel : ViewModelBase
    {
        #region Fields

        private double _canvasWidth;
        private double _canvasHeight;
        private ObservableCollection<object> _scaledShapes;
        private bool _isScaled;

        #endregion

        #region Properties

        public ObservableCollection<object> ScaledShapes
        {
            get => _scaledShapes;
            set
            {
                _scaledShapes = value;
                OnPropertyChanged(nameof(ScaledShapes));
            }
        }

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

        public ICommand ReadCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        #endregion

        #region Constructor
        public CartesianPlaneViewModel()
        {
            ScaledShapes = new ObservableCollection<object>();
            ReadCommand =  new RelayCommand(o => ReadButtonClick());
            ClearCommand = new RelayCommand(o => ClearButtonClick());
        }

        #endregion

        #region PrivateMethods

        public List<IShape> Shapes { get; set; }

        public async void ReadShapes(string filePath)
        {
            Shapes = (List<IShape>)await ReadData.Read(filePath);
        }

        private void OnGridChanged(string name)
        {
            ScaledShapes.Clear();
            DrawScaledShapes();
        }

        private void ReadButtonClick()
        {
            ReadShapes(DestinationPath);
            DrawScaledShapes();
        }

        private void ClearButtonClick()
        {
            if (Shapes != null) Shapes.Clear();
            ScaledShapes.Clear();
            DrawScaledShapes();
            DestinationPath = string.Empty;
            OnPropertyChanged(nameof(DestinationPath));
        }

        private async void DrawScaledShapes()
        {
            ScaledShapes.Clear();
            var shapes = await DrawService.GetScaledShapes(Shapes, new Point(CanvasWidth, CanvasHeight));
            shapes.AddRange(_scaledShapes);
            ScaledShapes = new ObservableCollection<object>(shapes);
        }

        #endregion
    }
}