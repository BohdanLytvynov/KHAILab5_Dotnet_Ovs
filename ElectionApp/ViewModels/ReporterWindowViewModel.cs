using BL.ReportGenerators;
using DAL.RepositoryWrapper;
using ElectionApp.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionApp.ViewModels
{
    internal class ReporterWindowViewModel : ViewModelBase
    {
        #region Fields
        IReportGenerator m_reportGenerator;
        IRepositoryWrapper m_repositoryWrapper;
        private DataTable m_report;

        private string m_title;
        #endregion

        #region Properties
        public DataTable Report 
        { 
            get=> m_report;
            set=> Set(ref m_report, value);
        }

        public string Title 
        {
            get=> m_title;
            set=>Set(ref m_title, value);
        }
        #endregion

        #region Commands

        #endregion

        #region Ctor
        public ReporterWindowViewModel(IReportGenerator reportGenerator, 
            IRepositoryWrapper repositoryWrapper) : this()
        {
            m_reportGenerator = reportGenerator;
            m_repositoryWrapper = repositoryWrapper;
            m_report = new DataTable();

            var voters = m_repositoryWrapper.VoterRepository.GetAll();

            Report = m_reportGenerator.GetReport(voters);
        }

        public ReporterWindowViewModel() : base(0)
        {
            m_title = "Report Window";
        }
        #endregion
    }
}
