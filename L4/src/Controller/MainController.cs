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
            var thresholdSettings = new ThresholdSettings
            {
                Population = _view.ReadPopulationThreshold(),
                LadderScore = _view.ReadLadderScoreThreshold(),
                SurfaceArea = _view.ReadSurfaceAreaThreshold(),
                FreedomOfChoice = _view.ReadFreedomThreshold(),
                GdpPerCapita = _view.ReadGdpPerCapitaThreshold()
            };

            var data =
                _customWorldService.RepopulateCustomWorldDatabase(thresholdSettings);

            _view.ShowCustomWorldData(data);
        }

        private void Attach(IMainView? view)
        {
            _view = view;
        }
    }
}