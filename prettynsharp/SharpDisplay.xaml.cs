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

        //Width of the Vector displayed in the sharp button
        public AdvancedLength VectorWidth { get { return (AdvancedLength)GetValue(VectorWidthProperty); } set { SetValue(VectorWidthProperty, value); } }
        public static readonly DependencyProperty VectorWidthProperty =
            DependencyProperty.Register("VectorWidth", typeof(AdvancedLength), typeof(SharpDisplay), new FrameworkPropertyMetadata(new AdvancedLength(), new PropertyChangedCallback(onVectorWidthChanged)));

        // Height of the Vector displayed in the sharp button
        public AdvancedLength VectorHeight { get { return (AdvancedLength)GetValue(VectorHeightProperty); } set { SetValue(VectorHeightProperty, value); } }
        public static readonly DependencyProperty VectorHeightProperty =
            DependencyProperty.Register("VectorHeight", typeof(AdvancedLength), typeof(SharpDisplay), new FrameworkPropertyMetadata(new AdvancedLength(), new PropertyChangedCallback(onVectorHeightChanged)));

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
                updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
            };
            
            this.SizeChanged += (sender, args) => updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
        }
        #endregion

        #region Methods
        // Callback invoked when VectorWidth changes
        private static void onVectorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SharpDisplay self = (SharpDisplay)d;
            AdvancedLength new_width = (AdvancedLength)e.NewValue;

            if (self.IsLoaded)
            {
                updateActualVectorSize(self, new_width, self.VectorHeight);
            }
        }

        // Callback invoked when VectorHeight changes
        private static void onVectorHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SharpDisplay self = (SharpDisplay)d;
            AdvancedLength new_height = (AdvancedLength)e.NewValue;

            if (self.IsLoaded)
            {
                updateActualVectorSize(self, self.VectorWidth, new_height);
            }
        }

        // Recompute the Vector's ActualWidth and ActualSize base on current size conditions
        private static void updateActualVectorSize(SharpDisplay self, AdvancedLength new_width, AdvancedLength new_height)
        {
            self.VectorStretch = Stretch.Fill;
            if (new_width.Unit == AdvancedLength.UnitType.Auto || new_height.Unit == AdvancedLength.UnitType.Auto)
            {
                self.VectorStretch = Stretch.Uniform;
            }

            self.ActualVectorHeight = self.computeActualVectorSize(new_height, self.ActualHeight);
            self.ActualVectorWidth = self.computeActualVectorSize(new_width, self.ActualWidth);
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
