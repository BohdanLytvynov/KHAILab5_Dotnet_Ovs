using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Voter : IModelBase, IEquatable<Voter>
    {       
        #region Properties

        public string Surename { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public int CountyNumber { get; set; }
        public string Party { get; set; }

        public int Age { get; set; }

        public string ProfessionName { get; set; }
        public int Id { get; set; }

        #endregion

        #region Ctor
        public Voter() : base()
        {
            
        }

        public Voter(int id, 
            string surename, 
            string name, 
            string lastname, 
            int countyNumber, 
            string party, 
            int age, 
            string professionName)
        {
            Id = id;
            Surename = surename;
            Name = name;
            Lastname = lastname;
            CountyNumber = countyNumber;
            Party = party;
            Age = age;
            ProfessionName = professionName;
        }

        public bool Equals(Voter other)
        {
            return this.Id.Equals(other.Id);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Id}) ")                
                .Append(Surename)
                .Append(" | ")
                .Append(Name)
                .Append(" | ")
                .Append(Lastname)
                .Append(" | ")
                .Append(CountyNumber)
                .Append(" | ")
                .Append(Party)
                .Append(" | ")
                .Append(Age)
                .Append(" | ")
                .Append(ProfessionName);

            return sb.ToString();
        }        
        #endregion

        #region Comparer
        public class CompareBySurename : IComparer<Voter>
        {
            public int Compare(Voter x, Voter y)
            {
                return x.Surename.CompareTo(y.Surename);
            }
        }

        #endregion
    }
}
