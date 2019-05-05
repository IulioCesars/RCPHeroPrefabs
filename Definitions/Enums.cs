using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Prefab.Definitions
{
    public class Enums
    {
        public enum MarkerState { Normal, Success, Fail }

        public enum GameState { WaitForStart, Contractions, RCP, RestartCycle }
    }
}
