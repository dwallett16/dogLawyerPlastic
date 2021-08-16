using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInputManager
{
    bool GetButtonDown(string input);
    float GetAxisRaw(string input);
}
