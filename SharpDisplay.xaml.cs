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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSharpener
{
    /// <summary>
    /// Interaction logic for SharpDisplay.xaml
    /// </summary>
    public partial class SharpDisplay : UserControl
    {

        #region Dependency Properties
        public Path Vector { get { return (Path)GetValue(VectorProperty); } set { SetValue(VectorProperty, value); } }
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Path), typeof(SharpDisplay), new PropertyMetadata(null));

        public Brush VectorBrush { get { return (Brush)GetValue(VectorBrushProperty); } set { SetValue(VectorBrushProperty, value); } }
        public static readonly DependencyProperty VectorBrushProperty =
            DependencyProperty.Register("VectorBrush", typeof(Brush), typeof(SharpDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        #endregion


        public SharpDisplay()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if(this.Vector == null)
                {
                    this.Vector = Constants.DEFAULT_PATH;
                }
            };
        }

        
    }
}
