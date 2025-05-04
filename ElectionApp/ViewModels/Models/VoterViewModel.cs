using ElectionApp.ViewModels.Base.VM;
using BL.Validators;
using BL.Dto;

namespace ElectionApp.ViewModels.Models
{    
    internal class VoterViewModel : ViewModelBase
    {
        #region Fields

        private int m_id;
        private string m_surename;
        private string m_name;
        private string m_lastname;
        private int m_countyNumber;
        private string m_party;
        private int m_age;
        private string m_professionName;
        #endregion

        #region Properties

        public int Id { get => m_id; }

        public string Surename 
        {
            get => m_surename;
            set
            {
                Set(ref m_surename, value);
            }
        }

        public string Name 
        {
            get=> m_name;
            set
            { 
                Set(ref m_name, value);
            }
        }

        public string Lastname 
        {
            get=> m_lastname;
            set
            {
                Set(ref m_lastname, value);
            }
        }

        public int CountyNumber 
        {
            get=>m_countyNumber;
            set
            {
                Set(ref m_countyNumber, value);
            }
        }

        public string Party 
        {
            get=> m_party;
            set
            {
                Set(ref m_party, value);
            }
        }

        public int Age 
        {
            get=> m_age;
            set 
            {
                Set(ref m_age, value);
            }
        }

        public string ProfessionName 
        { 
            get=> m_professionName;
            set 
            {
                Set(ref m_professionName, value);
            }
        }
        #endregion

        #region IDataErrorInfo
        public override string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                
                switch (columnName)
                {
                    case nameof(Surename):
                        SetValidArrayValue(0, ValidationHelper.IsNSLValid(Surename, out error));
                        break;
                    case nameof(Name):
                        SetValidArrayValue(1, ValidationHelper.IsNSLValid(Name, out error));
                        break;
                    case nameof(Lastname):
                        SetValidArrayValue(2, ValidationHelper.IsNSLValid(Lastname, out error));
                        break;
                    case nameof(CountyNumber):

                        break;
                    case nameof(Party):
                        SetValidArrayValue(4, ValidationHelper.ValidateText(Party, out error));
                        break;
                    case nameof(Age):

                        break;
                    case nameof(ProfessionName):
                        SetValidArrayValue(6, ValidationHelper.ValidateText(ProfessionName, out error));
                        break;
                }

                return error;
            }
        }
        #endregion

        public VoterViewModel() : base(7)
        {
            
        }

        public VoterViewModel(int id,
            string surename,
            string name,
            string lastname,
            int countyNumber,
            string party,
            int age,
            string professionName) : base(7)
        {
            m_id = id;
            m_surename = surename;
            m_name = name;
            m_lastname = lastname;
            m_countyNumber = countyNumber;
            m_party = party;    
            m_age = age;
            m_professionName = professionName;
        }
    }
}
