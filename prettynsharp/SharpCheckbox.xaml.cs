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
        public Vector Nullmark { get { return (Vector)GetValue(NullmarkProperty); } set { SetValue(NullmarkProperty, value); } }
        public static readonly DependencyProperty NullmarkProperty =
            DependencyProperty.Register("Nullmark", typeof(Vector), typeof(SharpCheckbox), new FrameworkPropertyMetadata(default(Vector)));

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
        #endregion

        public SharpCheckbox()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.MarkHighlight == null)
                {
                    this.MarkHighlight = this.MarkBrush;
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
