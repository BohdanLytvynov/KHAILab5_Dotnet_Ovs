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
        private DataContext m_context;

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

        public RepositoryWrapper(DataContext dataContext)
        {
            m_context = dataContext;
        }
    }
}
