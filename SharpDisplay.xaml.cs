using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public Brush HighlightBrush { get { return (Brush)GetValue(HighlightBrushProperty); } set { SetValue(HighlightBrushProperty, value); } }
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(SharpDisplay), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

        public double VectorWidth { get { return (double)GetValue(VectorWidthProperty); } set { SetValue(VectorWidthProperty, value); } }
        public static readonly DependencyProperty VectorWidthProperty =
            DependencyProperty.Register("VectorWidth", typeof(double), typeof(SharpDisplay), new FrameworkPropertyMetadata(default(double)));

        public double VectorHeight { get { return (double)GetValue(VectorHeightProperty); } set { SetValue(VectorHeightProperty, value); } }
        public static readonly DependencyProperty VectorHeightProperty =
            DependencyProperty.Register("VectorHeight", typeof(double), typeof(SharpDisplay), new FrameworkPropertyMetadata(default(double)));

        public Brush BackgroundOnHover { get { return (Brush)GetValue(BackgroundOnHoverProperty); } set { SetValue(BackgroundOnHoverProperty, value); } }
        public static readonly DependencyProperty BackgroundOnHoverProperty =
            DependencyProperty.Register("BackgroundOnHover", typeof(Brush), typeof(SharpDisplay), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightGreen)));
        #endregion


        public SharpDisplay()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.Vector == null)
                {
                    this.Vector = Constants.DEFAULT_PATH;
                }
            };
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion
    }
}
