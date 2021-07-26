using Assets.Scripts.Battle.Actions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tests.EditTests
{
    public class EditTestBase
    {
        protected void SetActionUtilitiesMock(IActionUtilities mockObject)
        {
            var utilitiesField = typeof(ActionUtilities).GetField("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            utilitiesField.SetValue(null, mockObject);
        }

        [TearDown]
        protected void Teardown()
        {
            var utilitiesField = typeof(ActionUtilities).GetField("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            utilitiesField.SetValue("instance", null);
        }
    }
}
