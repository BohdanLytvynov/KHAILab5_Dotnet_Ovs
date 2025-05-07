using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VoterRepository : IRepositoryBase<Voter>, IVoterRepository
    {
        JsonDataProvider DataContext { get; set; }

        public VoterRepository(IDataProvider dataContext)
        {
            DataContext = dataContext as JsonDataProvider;
        }

        public bool Edit(int id, Voter entity)
        {
            var voters = DataContext.Voters;
            int len = voters.Count;
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (voters[i].Id.Equals(id))
                {
                    index = i;
                    break;
                }
            }

            if (index <= 0)
            {
                return false;
            }
            else
            {
                voters[index].UpdateValues(entity);
                return true;
            }
        }

        public Voter Get(int id)
        {
            return DataContext.Voters.Where(x => x.Id.Equals(id)).Select(x => x).FirstOrDefault();
        }

        public IEnumerable<Voter> GetAll()
        {
            return DataContext.Voters;
        }

        public bool Remove(int id)
        {
            var voters = DataContext.Voters;
            int len = voters.Count;
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (voters[i].Id.Equals(id))
                {
                    index = i;
                    break;
                }
            }

            if (index <= 0)
            {
                return false;
            }
            else
            {
                voters.RemoveAt(index);
                return true;
            }
        }

        public void SaveData()
        {
            DataContext.SaveData();
        }

        public void Add(Voter entity)
        {
            Voter last = DataContext.Voters.LastOrDefault();

            int index = 0;

            if (last != null)
            {
                index = last.Id + 1;
            }

            entity.Id = index;

            DataContext.Voters.Add(entity);
        }
    }
}
