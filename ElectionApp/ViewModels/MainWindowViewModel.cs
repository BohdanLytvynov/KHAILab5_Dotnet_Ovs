using ElectionApp.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private string m_title;

        #endregion

        #region Properties

        public string Title 
        {
            get=> m_title;
            set=>Set(ref m_title, value);
        }

        #endregion

        #region Commands

        #endregion

        #region Ctor

        public MainWindowViewModel() : base(1)
        {
            #region Init Fields
            m_title = "Election App V1";


            #endregion

            #region Init Commands

            #endregion
        }

        #endregion

        #region Methods



        #endregion
    }
}
