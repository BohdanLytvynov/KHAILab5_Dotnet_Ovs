
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectionApp.WindowManagers
{
    internal class WindowManager : IWindowManager
    {
        private IServiceProvider m_serviceProvider;

        private Dictionary<string, Window> m_windows;

        public WindowManager(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            m_windows = new Dictionary<string, Window>();
        }

        public void OpenWindow(Type window)
        {
            Window w = null;
            string typeName = window.Name;
            if (m_windows.TryGetValue(typeName, out w) && w != null)
            {
                w.Topmost = true;
                w.ShowDialog();
                return;
            }

            w = (Window)m_serviceProvider.GetRequiredService(window);
            w.Closed += (object s, EventArgs e) => 
            {
                m_windows.Remove(typeName);
            };
            m_windows[typeName] = w;
            w.Topmost = true;
            w.ShowDialog();
        }

        
    }
}
