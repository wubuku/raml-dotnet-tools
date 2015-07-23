using System.ComponentModel;
using System.Runtime.CompilerServices;
using Raml.Common.Annotations;

namespace Raml.Common
{
    /// <summary>
    /// Interaction logic for RamlPropertiesEditor.xaml
    /// </summary>
    public partial class RamlPropertiesEditor : INotifyPropertyChanged
    {
        private string ramlPath;
        private string ns;
        private string source;

        public string Namespace
        {
            get { return ns; }
            set
            {
                ns = value;
                OnPropertyChanged();
            }
        }

        public string Source
        {
            get { return source; }
            set
            {
                source = value; 
                OnPropertyChanged();
            }
        }

        public RamlPropertiesEditor()
        {
            InitializeComponent();
        }

        public void Load(string ramlPath)
        {
            this.ramlPath = ramlPath;
            var ramlProperties = RamlPropertiesManager.Load(ramlPath);
            Namespace = ramlProperties.Namespace;
            Source = ramlProperties.Source;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ramlProperties = new RamlProperties
            {
                Namespace = Namespace,
                Source = Source
            };
            
            RamlPropertiesManager.Save(ramlProperties, ramlPath);
            Close();
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
