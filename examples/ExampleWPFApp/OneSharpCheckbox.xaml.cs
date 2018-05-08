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
using System.Windows.Shapes;

namespace ExampleWPFApp
{
    /// <summary>
    /// Interaction logic for OneSharpCheckbox.xaml
    /// </summary>
    public partial class OneSharpCheckbox : Window, INotifyPropertyChanged
    {
        public OneSharpCheckbox()
        {
            InitializeComponent();
        }

        private bool? _CheckboxSet;
        public bool? CheckboxSet
        {
            get { return this._CheckboxSet; }
            set { if (!Object.Equals(value, this._CheckboxSet)) { this._CheckboxSet = value; this.RaisePropertyChanged(); } }
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

        private void SharpCheckbox_Click(object sender, RoutedEventArgs e)
        {
            SharpCheckbox cb = (SharpCheckbox)sender;
            if (cb.IsChecked == null)
            {
                cb.IsChecked = false;
            }
            else if (cb.IsChecked == false)
            {
                cb.IsChecked = true;
            }
            else
            {
                cb.IsChecked = null;
            }
        }
    }
}
