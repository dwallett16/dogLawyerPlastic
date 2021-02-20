using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleState
{
    IBattleState Execute(BattleController controller);
}
