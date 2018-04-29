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

namespace PrettyNSharp
{
    /// <summary>
    /// Interaction logic for SharpDisplay.xaml
    /// </summary>
    public partial class SharpDisplay : UserControl, INotifyPropertyChanged
    {
        #region Dependency Properties
        // SVG design diplayed by SharpDisplay
        public Path Vector { get { return (Path)GetValue(VectorProperty); } set { SetValue(VectorProperty, value); } }
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Path), typeof(SharpDisplay), new PropertyMetadata(null));

        //Size of the SVG design
        public AdvancedSize VectorSize { get { return (AdvancedSize)GetValue(VectorSizeProperty); } set { SetValue(VectorSizeProperty, value); } }
        public static readonly DependencyProperty VectorSizeProperty =
            DependencyProperty.Register("VectorSize", typeof(AdvancedSize), typeof(SharpDisplay), new PropertyMetadata(new AdvancedSize(), new PropertyChangedCallback(onVectorSizeChanged)));

        //Fill color of the SVG design
        public Brush VectorBrush { get { return (Brush)GetValue(VectorBrushProperty); } set { SetValue(VectorBrushProperty, value); } }
        public static readonly DependencyProperty VectorBrushProperty =
            DependencyProperty.Register("VectorBrush", typeof(Brush), typeof(SharpDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        #endregion

        #region GUI Properties
        private Stretch _VectorStretch;
        public Stretch VectorStretch
        {
            get { return this._VectorStretch; }
            set { if (!Object.Equals(value, this._VectorStretch)) { this._VectorStretch = value; this.RaisePropertyChanged(); } }
        }

        private double _ActualVectorWidth;
        public double ActualVectorWidth
        {
            get { return this._ActualVectorWidth; }
            set { if (!Object.Equals(value, this._ActualVectorWidth)) { this._ActualVectorWidth = value; this.RaisePropertyChanged(); } }
        }

        private double _ActualVectorHeight;
        public double ActualVectorHeight
        {
            get { return this._ActualVectorHeight; }
            set { if (!Object.Equals(value, this._ActualVectorHeight)) { this._ActualVectorHeight = value; this.RaisePropertyChanged(); } }
        }
        #endregion

        #region Constructor
        public SharpDisplay()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                updateVectorSize(this, this.VectorSize);
            };
            
            this.SizeChanged += (sender, args) => updateVectorSize(this, this.VectorSize);
        } 
        #endregion

        #region Methods
        private static void onVectorSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SharpDisplay this_ = (SharpDisplay)d;
            AdvancedSize new_size = (AdvancedSize)e.NewValue;

            if (this_.IsLoaded)
            {
                updateVectorSize(this_, new_size);
            }
        }

        private static void updateVectorSize(SharpDisplay this_, AdvancedSize new_size)
        {
            this_.VectorStretch = Stretch.Fill;
            if (new_size.Height.Unit == AdvancedLength.UnitType.Auto || new_size.Width.Unit == AdvancedLength.UnitType.Auto)
            {
                this_.VectorStretch = Stretch.Uniform;
            }

            this_.ActualVectorHeight = this_.computeActualVectorSize(new_size.Height, this_.ActualHeight);
            this_.ActualVectorWidth = this_.computeActualVectorSize(new_size.Width, this_.ActualWidth);
        }

        //This function should ONLY be invoked from updateVectorSize() (in  c# 7 this can be turned into a local function)
        private double computeActualVectorSize(AdvancedLength len, double container_len)
        {
            switch (len.Unit)
            {
                case AdvancedLength.UnitType.Auto:
                    return double.NaN; //In Auto mode, we set the Vector width (or length) to NaN and let the Vector.Strech = Uniform take care of the ActualWidth(or length)
                case AdvancedLength.UnitType.Percent:
                    return len.Value * container_len / 100;
                case AdvancedLength.UnitType.Pixel:
                    return len.Value;
                case AdvancedLength.UnitType.Star:
                    return container_len;
                default:
                    throw new InvalidEnumArgumentException("Unhandled value: " + len.Unit);
            }
        }
        #endregion

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
