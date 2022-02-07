using System;
using System.IO;
using VectorGraphicViewer.ViewModel;
using Xunit;

namespace VectorGraphicViewer.Test
{
    public class CartesianPlaneViewModelTests
    {
        private CartesianPlaneViewModel _viewModel;

        public CartesianPlaneViewModelTests()
        {
            _viewModel = new CartesianPlaneViewModel();
        }

        [Fact]
        public void ShouldReadShapes()
        {
            _viewModel.ReadShapes(Path.Combine(Environment.CurrentDirectory, "test.json"));

            Assert.NotNull(_viewModel.Shapes);
        }
        

        [Fact]
        public void ShouldClearDestinationPathAfterClearClicked()
        {
            _viewModel.DestinationPath = "test";
            _viewModel.ClearCommand.Execute(null);

            Assert.Equal(_viewModel.DestinationPath, string.Empty);
        }
    }
}