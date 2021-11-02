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
                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;

                var mainController = diContainer.Get<MainController>();
                mainController.View = mainWindow;

                var database = diContainer.Get<Database>();
                database.OpenConnection();

                mainController.Compile();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}