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
        // SVG for checkmark
        public Path Checkmark { get { return (Path)GetValue(VectorProperty); } set { SetValue(VectorProperty, value); } }
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Checkmark", typeof(Path), typeof(SharpCheckbox), new PropertyMetadata(null));

        // SVG when checkbox is in 3 state mode, and value isChecked value is null
        public Path Nullmark { get { return (Path)GetValue(NullmarkProperty); } set { SetValue(NullmarkProperty, value); } }
        public static readonly DependencyProperty NullmarkProperty =
            DependencyProperty.Register("Nullmark", typeof(Path), typeof(SharpCheckbox), new FrameworkPropertyMetadata(null));

        // Checkmark And Nullmark fill color
        public Brush MarkBrush { get { return (Brush)GetValue(VectorBrushProperty); } set { SetValue(VectorBrushProperty, value); } }
        public static readonly DependencyProperty VectorBrushProperty =
            DependencyProperty.Register("MarkBrush", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        // Checkmark and Nullmark fill color when mouse is over
        public Brush MarkHighlight { get { return (Brush)GetValue(HighlightBrushProperty); } set { SetValue(HighlightBrushProperty, value); } }
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("MarkHighlight", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(null));

        // Margin between the Checkmark (or Nullmark) and the checkbox frame
        public Thickness MarkMargin { get { return (Thickness)GetValue(MarkMarginProperty); } set { SetValue(MarkMarginProperty, value); } }
        public static readonly DependencyProperty MarkMarginProperty =
            DependencyProperty.Register("MarkMargin", typeof(Thickness), typeof(SharpCheckbox), new FrameworkPropertyMetadata(default(Thickness)));

        // Border color when mouse is over (The border acts as the frame of the checkbox)
        public Brush BorderOnHover { get { return (Brush)GetValue(BorderOnHoverProperty); } set { SetValue(BorderOnHoverProperty, value); } }
        public static readonly DependencyProperty BorderOnHoverProperty =
            DependencyProperty.Register("BorderOnHover", typeof(Brush), typeof(SharpCheckbox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        // Corner radius of the checkbox border
        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SharpCheckbox), new FrameworkPropertyMetadata(default(CornerRadius)));



        public bool ToggleMode { get { return (bool)GetValue(ToggleModeProperty); } set { SetValue(ToggleModeProperty, value); } }
        public static readonly DependencyProperty ToggleModeProperty =
            DependencyProperty.Register("ToggleMode", typeof(bool), typeof(SharpCheckbox), new FrameworkPropertyMetadata(false, new PropertyChangedCallback((d,e) => { ((SharpCheckbox)d).updateBrushes(); })));

        public Brush UntoggledBrush { get { return (Brush)GetValue(UntoggledBrushProperty); } set { SetValue(UntoggledBrushProperty, value); } }
        public static readonly DependencyProperty UntoggledBrushProperty =
            DependencyProperty.Register("UntoggledBrush", typeof(Brush), typeof(SharpCheckbox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public Brush UntoggledHighlight { get { return (Brush)GetValue(UntoggledHighlightProperty); } set { SetValue(UntoggledHighlightProperty, value); } }
        public static readonly DependencyProperty UntoggledHighlightProperty =
            DependencyProperty.Register("UntoggledHighlight", typeof(Brush), typeof(SharpCheckbox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightGray)));
        #endregion

        #region GUI Properties
        private Brush _ActualMarkBrush;
        public Brush ActualMarkBrush
        {
            get { return this._ActualMarkBrush; }
            set { if (!Object.Equals(value, this._ActualMarkBrush)) { this._ActualMarkBrush = value; this.RaisePropertyChanged(); } }
        }

        private Brush _ActualMarkHighLight;
        public Brush ActualMarkHighlight
        {
            get { return this._ActualMarkHighLight; }
            set { if (!Object.Equals(value, this._ActualMarkHighLight)) { this._ActualMarkHighLight = value; this.RaisePropertyChanged(); } }
        }

        private Visibility _CheckMarkVisibility;
        public Visibility CheckMarkVisibility
        {
            get { return this._CheckMarkVisibility; }
            set { if (!Object.Equals(value, this._CheckMarkVisibility)) { this._CheckMarkVisibility = value; this.RaisePropertyChanged(); } }
        }

        private Visibility _NullMarkVisibility;
        public Visibility NullMarkVisibility
        {
            get { return this._NullMarkVisibility; }
            set { if (!Object.Equals(value, this._NullMarkVisibility)) { this._NullMarkVisibility = value; this.RaisePropertyChanged(); } }
        }
        #endregion

        public SharpCheckbox()
        {
            InitializeComponent();
            this.Click += (sender, args) => onClick();
            this.Loaded += (sender, args) =>
            {
                if (this.MarkHighlight == null)
                {
                    this.MarkHighlight = this.MarkBrush;
                }
                this.updateBrushes();
                this.onClick();
            };
        }

        private void onClick()
        {
            if (ToggleMode)
            {
                CheckMarkVisibility = Visibility.Visible;
                NullMarkVisibility = Visibility.Collapsed;
            }
            else
            {
                if (IsChecked == null)
                {
                    CheckMarkVisibility = Visibility.Collapsed;
                    NullMarkVisibility = Visibility.Visible;
                }
                else if (IsChecked == true)
                {
                    CheckMarkVisibility = Visibility.Visible;
                    NullMarkVisibility = Visibility.Collapsed;
                }
                else
                {
                    CheckMarkVisibility = Visibility.Collapsed;
                    NullMarkVisibility = Visibility.Collapsed;
                }
            }
            this.updateBrushes();
        }

        private void updateBrushes()
        {
            if (ToggleMode)
            {
                if ((bool)IsChecked)
                {
                    ActualMarkBrush = MarkBrush;
                    ActualMarkHighlight = MarkHighlight;
                }
                else
                {
                    ActualMarkBrush = UntoggledBrush;
                    ActualMarkHighlight = UntoggledHighlight;
                }
            }
            else
            {
                ActualMarkBrush = MarkBrush;
                ActualMarkHighlight = MarkHighlight;
            }
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
