using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using L4.Controller;
using L4.Domain;

namespace L4.View
{
    public class MainWindow : Window, IMainView
    {
        private readonly DataGrid _worldDataGrid;
        private MainController? _controller;

        public MainController Controller
        {
            set => _controller = value;
        }

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

        public int ReadSurfaceAreaThreshold()
        {
            return (int) this.FindControl<NumericUpDown>("InputSurfaceArea").Value;
        }

        public float ReadFreedomThreshold()
        {
            return (float) this.FindControl<NumericUpDown>("InputFreedomOfChoice").Value;
        }

        public float ReadLadderScoreThreshold()
        {
            return (float) this.FindControl<NumericUpDown>("InputLadderScore").Value;

        }

        public int ReadPopulationThreshold()
        {
            return (int) this.FindControl<NumericUpDown>("InputPopulation").Value;

        }

        public float ReadGdpPerCapitaThreshold()
        {
            return (float) this.FindControl<NumericUpDown>("InputGdpPerCapita").Value;
        }

        private void Compile_OnClick(object? sender, RoutedEventArgs e)
        {
            _controller.Compile();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}