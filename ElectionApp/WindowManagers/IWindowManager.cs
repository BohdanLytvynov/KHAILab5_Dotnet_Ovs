using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionApp.WindowManagers
{
    internal interface IWindowManager
    {
        void OpenWindow(Type window);
    }
}
