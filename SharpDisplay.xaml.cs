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
    public partial class SharpDisplay : UserControl, INotifyPropertyChanged
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
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(SharpDisplay), new PropertyMetadata(null));

        public Brush BackgroundOnHover { get { return (Brush)GetValue(BackgroundOnHoverProperty); } set { SetValue(BackgroundOnHoverProperty, value); } }
        public static readonly DependencyProperty BackgroundOnHoverProperty =
            DependencyProperty.Register("BackgroundOnHover", typeof(Brush), typeof(SharpDisplay), new PropertyMetadata(null));

        public AdvancedSize VectorSize { get { return (AdvancedSize)GetValue(VectorSizeProperty); } set { SetValue(VectorSizeProperty, value); } }
        public static readonly DependencyProperty VectorSizeProperty =
            DependencyProperty.Register("VectorSize", typeof(AdvancedSize), typeof(SharpDisplay), new PropertyMetadata(new AdvancedSize(), new PropertyChangedCallback(onVectorSizeChanged)));

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
            this_.Stretch = Stretch.Fill;
            if (new_size.Height.Unit == AdvancedLength.UnitType.Auto || new_size.Width.Unit == AdvancedLength.UnitType.Auto)
            {
                this_.Stretch = Stretch.Uniform;
            }

            this_.ActualVectorHeight = this_.computeActualVectorSize(new_size.Height, this_.ActualHeight);
            this_.ActualVectorWidth = this_.computeActualVectorSize(new_size.Width, this_.ActualWidth);
        }

        private double computeActualVectorSize(AdvancedLength len, double container_len)
        {
            if (len.Unit == AdvancedLength.UnitType.Auto)
            {
                return container_len; //value does not matter because stretch is set to uniform
            }
            else if (len.Unit == AdvancedLength.UnitType.Percent)
            {
                return len.Value * container_len / 100;
            }
            else if (len.Unit == AdvancedLength.UnitType.Pixel)
            {
                return len.Value;
            }
            else if (len.Unit == AdvancedLength.UnitType.Star)
            {
                return container_len;
            }
            throw new InvalidEnumArgumentException("Unknwon enum value: " + len.Unit);
        }
        #endregion

        #region GUI Properties
        private Stretch _Stretch;
        public Stretch Stretch
        {
            get { return this._Stretch; }
            set { if (!Object.Equals(value, this._Stretch)) { this._Stretch = value; this.RaisePropertyChanged(); } }
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


        public SharpDisplay()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.Vector == null)
                {
                    this.Vector = Constants.DEFAULT_PATH;
                }
                if (this.HighlightBrush == null)
                {
                    this.HighlightBrush = this.VectorBrush;
                }
                if (this.BackgroundOnHover == null)
                {
                    this.BackgroundOnHover = this.Background ?? new SolidColorBrush(Colors.Transparent);
                }
                updateVectorSize(this, this.VectorSize);
            };

            this.SizeChanged += (sender, args) => updateVectorSize(this, this.VectorSize);
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
