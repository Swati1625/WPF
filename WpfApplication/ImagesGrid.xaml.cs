using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for ImagesGrid.xaml
    /// </summary>
    public partial class ImagesGrid : Window
    {
        public ImagesGrid()
        {
            InitializeComponent();
            BindImages();
        }
        private void BindImages()
        {
            List<Image> imgList = new List<Image>();
            List<string> lstFileNames = new List<string>(System.IO.Directory.EnumerateFiles("D:\\Project\\WpfApplication\\WpfApplication\\Images", "*.jpg"));
            foreach (string fileName in lstFileNames)
            {
                var imgTemp = new Image();
                imgTemp.Source = new BitmapImage(new Uri(fileName));
                imgTemp.Height = imgTemp.Width = 100;
                imgList.Add(imgTemp);
            }
            ImageList.ItemsSource = imgList;
        }
    }
}
