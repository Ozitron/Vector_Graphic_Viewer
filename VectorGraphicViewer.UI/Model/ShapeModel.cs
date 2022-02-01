using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VectorGraphicViewer.UI.Model
{
    public class ShapeModel
    {
        //private List<string> repositoryData;

        private Canvas lineRepositoryData;

        public ShapeModel()
        {
            lineRepositoryData = GetData();
        }

        public Canvas ImportantInfo
        {
            get
            {
                return lineRepositoryData;
            }
        }



        /// <summary>
        /// Simulates data retrieval from a repository
        /// </summary>
        /// <returns>List of strings</returns>
        private Canvas GetData()
        {
            //lineRepositoryData = new List<Line>{ new Line(new PointF(10, 20), new PointF(30, 40), Color.Aqua)};

            return lineRepositoryData;
        }

    }
}