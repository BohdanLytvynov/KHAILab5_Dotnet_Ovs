using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesWrapper
    {
        private static IServiceProvider m_serviceProvider;

        public IServiceProvider Services 
        {
            get 
            {
                if(m_serviceProvider == null)
                    m_serviceProvider = InitializeServices();

                return m_serviceProvider;
            }
        }

        private static IServiceProvider InitializeServices()
        {
            var services = new ServiceCollection();

            #region Do initial Setup here

            #endregion

            return services.BuildServiceProvider();
        }
    }
}
