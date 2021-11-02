using L4.Domain;

namespace L4.Controller
{
    public class MainController
    {
        private readonly CustomWorldService _customWorldService;
        private IMainView? _view;

        public MainController(CustomWorldService customWorldService)
        {
            _customWorldService = customWorldService;
        }

        public IMainView? View
        {
            set => Attach(value);
        }

        public void Compile()
        {
            var data = _customWorldService.RepopulateCustomWorldDatabase();
            _view.ShowCustomWorldData(data);
        }

        private void Attach(IMainView? view)
        {
            _view = view;
        }
    }
}