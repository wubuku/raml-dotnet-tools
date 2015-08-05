namespace Raml.Common
{
    /// <summary>
    /// Interaction logic for RamlPropertiesEditor.xaml
    /// </summary>
    public partial class RamlPropertiesEditor
    {
        private string ramlPath;
        public RamlPropertiesEditor()
        {
            InitializeComponent();
        }

        public void Load(string ramlPath)
        {
            this.ramlPath = ramlPath;
            PropertyGrid.SelectedObject = RamlPropertiesManager.Load(ramlPath);
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RamlPropertiesManager.Save((RamlProperties)PropertyGrid.SelectedObject, ramlPath);
            Close();
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
