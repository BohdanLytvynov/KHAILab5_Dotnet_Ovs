using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataContext
    {
        #region Fields
        private string m_filePath;
        #endregion

        #region Properties
        public List<Voter> Voters { get; set; }        
        #endregion

        #region Ctor
        public DataContext()
        {
            m_filePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Database"
                + Path.DirectorySeparatorChar + "Voters.json";
            try
            {
                var str = IOHelper.ReadFromFile(m_filePath);

                Voters = JsonHelper.Deserialize(str, Voters);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("DAL ctor. Error: " + ex.Message);
#endif
            }            
        }

        public void SaveData()
        { 
            
        }
        #endregion      
    }
}
