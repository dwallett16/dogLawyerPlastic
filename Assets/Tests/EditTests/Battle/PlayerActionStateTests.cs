
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

        var result = state.Execute();

        Assert.NotNull(result);
    }
}
