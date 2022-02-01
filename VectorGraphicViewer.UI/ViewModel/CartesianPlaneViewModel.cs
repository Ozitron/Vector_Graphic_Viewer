using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Model;
using Line = System.Windows.Shapes.Line;

namespace VectorGraphicViewer.UI.ViewModel
{
    internal class CartesianPlaneViewModel : INotifyPropertyChanged
    {
        private List<Line> helloString = new List<Line>();
        private Canvas _canvas;

        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand m_ButtonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return m_ButtonCommand;
            }
            set
            {
                m_ButtonCommand = value;
            }
        }

        public List<Line> HelloString
        {
            get
            {
                return helloString;
            }
            set
            {
                helloString = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Raises OnPropertychangedEvent when property changes
        /// </summary>
        /// <param name="name">String representing the property name</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public CartesianPlaneViewModel()
        {
            //ButtonCommand = new RelayCommand(new Action<object>(ShowMessage));
            ShapeModel helloWorldModel = new ShapeModel();
            //helloString = helloWorldModel.ImportantInfo;


            var myLine = new System.Windows.Shapes.Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = 50;
            myLine.Y1 = 1;
            myLine.Y2 = 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            helloString.Add(myLine);

            myLine = new System.Windows.Shapes.Line();
            myLine.Stroke = Brushes.Red;
            myLine.X1 = 100;
            myLine.X2 = 150;
            myLine.Y1 = 100;
            myLine.Y2 = 150;
            myLine.StrokeThickness = 2;
            helloString.Add(myLine);
        }
    }
}