using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace VectorGraphicViewer.UI.View
{
    public partial class CartesianPlaneView : Window
    {
        public CartesianPlaneView()
        {
            InitializeComponent();
        }

        private void PickFile_OnClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Browse Files",
                Filter = "Json files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                this.FilePathTextBox.Text = dialog.FileName;
                
                this.FilePathTextBox
                    .GetBindingExpression(TextBox.TextProperty)
                    ?.UpdateSource();
            }
        }
    }
}
