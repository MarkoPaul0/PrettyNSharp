using PrettyNSharp;
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

namespace ExampleWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private ContentDisplayType _DisplayType;
        public ContentDisplayType DisplayType
        {
            get { return this._DisplayType; }
            set { if (!Object.Equals(value, this._DisplayType)) { this._DisplayType = value; this.RaisePropertyChanged(); } }
        }


        private void SharpButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DisplayType == ContentDisplayType.Both)
            {
                this.DisplayType = ContentDisplayType.ContentOnly;
            }
            else if (this.DisplayType == ContentDisplayType.ContentOnly)
            {
                this.DisplayType = ContentDisplayType.IconOnly;
            }
            else
            {
                this.DisplayType = ContentDisplayType.Both;
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
