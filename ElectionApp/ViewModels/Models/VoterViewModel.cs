using ElectionApp.ViewModels.Base.VM;
using BL.Validators;
using ElectionApp.Enums;
using System.Windows.Input;
using ElectionApp.ViewModels.Base.Commands;

namespace ElectionApp.ViewModels.Models
{    
    internal class VoterViewModel : ViewModelBase
    {
        #region Fields

        private int m_id;
        private string m_surename;
        private string m_name;
        private string m_lastname;
        private string m_countyNumber;
        private string m_party;
        private string m_age;
        private string m_professionName;

        private int m_showNumber;

        private Action m_state;
        private bool m_IsValid;
        
        #endregion

        #region Properties

        public int Id { get => m_id; set => m_id = value; }

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

        public string CountyNumber 
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

        public string Age 
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

        public Action Action 
        {
            get=> m_state;
            set=>Set(ref m_state, value);
        }

        public int ShowNumber 
        {
            get=> m_showNumber;
            set => Set(ref m_showNumber, value);
        }

        public bool IsValid 
        {
            get=> m_IsValid;
            set=>Set(ref m_IsValid, value);
        }
        #endregion

        #region Commands

        public ICommand OnAbortDeletePressed { get; set; }

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
                        SetValidArrayValue(3, ValidationHelper.ValidateNumber(CountyNumber, out error));
                        break;
                    case nameof(Party):
                        SetValidArrayValue(4, ValidationHelper.ValidateText(Party, out error));
                        break;
                    case nameof(Age):
                        SetValidArrayValue(5, ValidationHelper.ValidateNumber(Age, out error));
                        break;
                    case nameof(ProfessionName):
                        SetValidArrayValue(6, ValidationHelper.ValidateText(ProfessionName, out error));
                        break;
                }

                IsValid = ValidateFields(0, GetValidArrayCount()-1);

                return error;
            }
        }
        #endregion

        public VoterViewModel() : 
            this(-1, "enter smth", "enter smth", "enter smth", "enter smth", 
                "enter smth", "enter smth", "enter smth")
        {
            
        }

        public VoterViewModel(
            int id,
            string surename,
            string name,
            string lastname,
            string countyNumber,
            string party,
            string age,
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

            m_state = Action.None;
            m_IsValid = false;

            OnAbortDeletePressed = new Command(
                OnAbortDeleteButtonPressedExecute,
                CanOnAbortDeleteButtonPressedExecute
                );
        }

        #region Methods

        private bool CanOnAbortDeleteButtonPressedExecute(object p) => true;

        private void OnAbortDeleteButtonPressedExecute(object p)
        { 
            Action = Action.None;
        }


        #endregion
    }
}
