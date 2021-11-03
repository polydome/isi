using System.Collections.Generic;
using L4.Domain;

namespace L4.Controller
{
    public interface IMainView
    {
        void ShowCustomWorldData(IEnumerable<DataCustomWorld> records);
        int ReadSurfaceAreaThreshold();
        int ReadPopulationThreshold();
        float ReadFreedomThreshold();
        float ReadLadderScoreThreshold();
        float ReadGdpPerCapitaThreshold();
    }
}