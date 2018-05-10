using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PrettyNSharp
{
    public enum ContentDisplayType
    {
        IconOnly,
        ContentOnly,
        Both
    }

    /// <summary>
    /// Interaction logic for SharpButton.xaml
    /// </summary>
    [TemplatePart(Name = "PART_Vector", Type = typeof(Path))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    public partial class SharpButton : Button, INotifyPropertyChanged
    {
        #region Members
        private Path _PART_Vector;
        private ContentPresenter _PART_Content;
        #endregion

        #region Dependency Properties
        // SVG icon displayed in the SharpButton
        public Path Vector { get { return (Path)GetValue(VectorProperty); } set { SetValue(VectorProperty, value); } }
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Path), typeof(SharpButton), new PropertyMetadata(null));

        //Width of the Vector displayed in the sharp button
        public AdvancedLength VectorWidth { get { return (AdvancedLength)GetValue(VectorWidthProperty); } set { SetValue(VectorWidthProperty, value); } }
        public static readonly DependencyProperty VectorWidthProperty =
            DependencyProperty.Register("VectorWidth", typeof(AdvancedLength), typeof(SharpButton), new FrameworkPropertyMetadata(new AdvancedLength(), new PropertyChangedCallback(onVectorWidthChanged)));

        // Height of the Vector displayed in the sharp button
        public AdvancedLength VectorHeight { get { return (AdvancedLength)GetValue(VectorHeightProperty); } set { SetValue(VectorHeightProperty, value); } }
        public static readonly DependencyProperty VectorHeightProperty =
            DependencyProperty.Register("VectorHeight", typeof(AdvancedLength), typeof(SharpButton), new FrameworkPropertyMetadata(new AdvancedLength(), new PropertyChangedCallback(onVectorHeightChanged)));

        // Fill color of the Vector
        public Brush VectorBrush { get { return (Brush)GetValue(VectorBrushProperty); } set { SetValue(VectorBrushProperty, value); } }
        public static readonly DependencyProperty VectorBrushProperty =
            DependencyProperty.Register("VectorBrush", typeof(Brush), typeof(SharpButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        // Fill color of the Vector when mouse is over the SharpButton
        public Brush HighlightBrush { get { return (Brush)GetValue(HighlightBrushProperty); } set { SetValue(HighlightBrushProperty, value); } }
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(SharpButton), new PropertyMetadata(null));

        // SharpButton background when mouse is over
        public Brush BackgroundOnHover { get { return (Brush)GetValue(BackgroundOnHoverProperty); } set { SetValue(BackgroundOnHoverProperty, value); } }
        public static readonly DependencyProperty BackgroundOnHoverProperty =
            DependencyProperty.Register("BackgroundOnHover", typeof(Brush), typeof(SharpButton), new PropertyMetadata(null));

        // SharpButton background when clicked
        public Brush BackgroundOnClick { get { return (Brush)GetValue(BackgroundOnClickProperty); } set { SetValue(BackgroundOnClickProperty, value); } }
        public static readonly DependencyProperty BackgroundOnClickProperty =
            DependencyProperty.Register("BackgroundOnClick", typeof(Brush), typeof(SharpButton), new PropertyMetadata(null));

        // What is displayed by the SharpButton: The vector only, The Content only, or both
        public ContentDisplayType ContentDisplay { get { return (ContentDisplayType)GetValue(ContentDisplayProperty); } set { SetValue(ContentDisplayProperty, value); } }
        public static readonly DependencyProperty ContentDisplayProperty =
            DependencyProperty.Register("ContentDisplay", typeof(ContentDisplayType), typeof(SharpButton), new FrameworkPropertyMetadata(ContentDisplayType.IconOnly));

        // Select the location of the content with respect to the icon: left, right, top, or bottom
        public Dock ContentLocation { get { return (Dock)GetValue(ContentLocationProperty); } set { SetValue(ContentLocationProperty, value); } }
        public static readonly DependencyProperty ContentLocationProperty =
            DependencyProperty.Register("ContentLocation", typeof(Dock), typeof(SharpButton), new FrameworkPropertyMetadata(Dock.Right));

        // Corner radius of the SharpButton
        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SharpButton), new FrameworkPropertyMetadata(default(CornerRadius)));
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
        public SharpButton() : base()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.HighlightBrush == null)
                {
                    this.HighlightBrush = this.VectorBrush;
                }
                if (this.BackgroundOnHover == null)
                {
                    this.BackgroundOnHover = this.Background ?? new SolidColorBrush(Colors.Transparent);
                }
                updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
            };

            // If the SharpButton size has changed, the vector size need to be updated as well
            this.SizeChanged += (sender, args) => updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
        } 
        #endregion

        #region Methods
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._PART_Vector = this.GetTemplateChild("PART_Vector") as Path;
            this._PART_Content = this.GetTemplateChild("PART_Content") as ContentPresenter;
            if (this._PART_Vector == null)
            {
                throw new Exception("Could not get template child 'PART_Vector' from " + this.GetType() + "!");
            }
            if (this._PART_Content == null)
            {
                throw new Exception("Could not get template child 'PART_Content' from " + this.GetType() + "!");
            }
            updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
            this._PART_Content.SizeChanged += (sender, args) => updateActualVectorSize(this, this.VectorWidth, this.VectorHeight);
        }

        // Callback invoked when VectorWidth changes
        private static void onVectorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SharpButton self = (SharpButton)d;
            AdvancedLength new_width = (AdvancedLength)e.NewValue;

            if (self.IsLoaded)
            {
                updateActualVectorSize(self, new_width, self.VectorHeight);
            }
        }

        // Callback invoked when VectorHeight changes
        private static void onVectorHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SharpButton self = (SharpButton)d;
            AdvancedLength new_height = (AdvancedLength)e.NewValue;

            if (self.IsLoaded)
            {
                updateActualVectorSize(self, self.VectorWidth, new_height);
            }
        }

        // Recompute the Vector's ActualWidth and ActualSize base on current size conditions
        private static void updateActualVectorSize(SharpButton self, AdvancedLength new_width, AdvancedLength new_height)
        {
            self.VectorStretch = Stretch.Fill;
            if (new_width.Unit == AdvancedLength.UnitType.Auto || new_height.Unit == AdvancedLength.UnitType.Auto)
            {
                self.VectorStretch = Stretch.Uniform;
            }

            self.ActualVectorHeight = self.computeActualVectorSize(new_height, self.ActualHeight);
            self.ActualVectorWidth = self.computeActualVectorSize(new_width, self.ActualWidth - self._PART_Content.ActualWidth);
        }

        // Compute an AdvancedLength base on current size conditions
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
