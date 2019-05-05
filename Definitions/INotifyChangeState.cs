using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Prefab.Definitions.Enums;

namespace Assets.Prefab.Definitions
{
    interface INotifyChangeState
    {
        void ChangeState(GameState gameState);
    }
}
