
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;

public class PlayerActionStateTests
{
    [Test]
    public void ExecuteReturnsBattleState() {
        var state = new PlayerActionState();
        var controller = new BattleController();

        var result = state.Execute(controller);

        Assert.NotNull(result);
    }
}
