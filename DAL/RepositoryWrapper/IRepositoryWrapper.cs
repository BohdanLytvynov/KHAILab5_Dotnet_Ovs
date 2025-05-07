using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IVoterRepository VoterRepository { get; }

        void LoadData();

        void SaveData();
    }
}
