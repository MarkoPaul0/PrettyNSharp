using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PrettyNSharp
{
    /// <summary>
    /// Interaction logic for SharpCheckbox.xaml
    /// </summary>
    public partial class SharpCheckbox : CheckBox, INotifyPropertyChanged
    {
        #region Dependency Properties
        public Path Vector { get { return (Path)GetValue(VectorProperty); } set { SetValue(VectorProperty, value); } }
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Path), typeof(SharpCheckbox), new PropertyMetadata(null));

        public Brush VectorBrush { get { return (Brush)GetValue(VectorBrushProperty); } set { SetValue(VectorBrushProperty, value); } }
        public static readonly DependencyProperty VectorBrushProperty =
            DependencyProperty.Register("VectorBrush", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush HighlightBrush { get { return (Brush)GetValue(HighlightBrushProperty); } set { SetValue(HighlightBrushProperty, value); } }
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(null));

        public Brush BackgroundOnHover { get { return (Brush)GetValue(BackgroundOnHoverProperty); } set { SetValue(BackgroundOnHoverProperty, value); } }
        public static readonly DependencyProperty BackgroundOnHoverProperty =
            DependencyProperty.Register("BackgroundOnHover", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(null));

        public Brush BackgroundOnClick { get { return (Brush)GetValue(BackgroundOnClickProperty); } set { SetValue(BackgroundOnClickProperty, value); } }
        public static readonly DependencyProperty BackgroundOnClickProperty =
            DependencyProperty.Register("BackgroundOnClick", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(null));
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

        public SharpCheckbox()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.Vector == null)
                {
                    //TODO: set a default vector
                }
                if (this.HighlightBrush == null)
                {
                    this.HighlightBrush = this.VectorBrush;
                }
                if (this.BackgroundOnHover == null)
                {
                    this.BackgroundOnHover = this.Background ?? new SolidColorBrush(Colors.Transparent);
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
