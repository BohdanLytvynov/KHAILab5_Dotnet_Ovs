using DAL.Data;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private JsonDataProvider m_context;

        private IVoterRepository m_VoterRepository;

        public IVoterRepository VoterRepository
        {
            get
            { 
                if(m_VoterRepository == null)
                    m_VoterRepository = new VoterRepository(m_context);

                return m_VoterRepository;
            }
        }

        public RepositoryWrapper(IDataProvider dataContext)
        {
            m_context = dataContext as JsonDataProvider;
        }
        
        public void LoadData()
        {
            m_context.LoadData();
        }

        public void SaveData()
        {
            m_context.SaveData();
        }
    }
}
