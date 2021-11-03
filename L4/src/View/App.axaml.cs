using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using L4.Controller;
using L4.Data;

namespace L4.View
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var diContainer = new DependencyInjectionContainer();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var database = diContainer.Get<Database>();
                database.OpenConnection();
                
                var mainWindow = diContainer.Get<MainWindow>();
                var mainController = diContainer.Get<MainController>();
                
                mainWindow.Controller = mainController;
                mainController.View = mainWindow;
                
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}