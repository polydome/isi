using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using L4.Controller;
using L4.Domain;

namespace L4.View
{
    public class MainWindow : Window, IMainView
    {
        private readonly DataGrid _worldDataGrid;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _worldDataGrid = this.FindControl<DataGrid>("WorldDataGrid");
            _worldDataGrid.IsReadOnly = true;
        }

        public void ShowCustomWorldData(IEnumerable<DataCustomWorld> records)
        {
            _worldDataGrid.Items = records;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}