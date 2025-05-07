using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DAL.Data
{
    public class JsonDataProvider : IDataProvider
    {
        #region Fields
        private string m_filePath;
        #endregion

        #region Properties
        public List<Voter> Voters { get; set; }

        public bool DataLoaded { get => Voters != null && Voters.Count > 0; }
        #endregion

        #region Ctor
        public JsonDataProvider()
        {
            m_filePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Database"
                + Path.DirectorySeparatorChar + "Voters.json";

            Voters = new List<Voter>();

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var str = IOHelper.ReadFromFile(m_filePath);

                if(string.IsNullOrEmpty(str))
                    return;

                Voters = JsonHelper.Deserialize(str, Voters);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Fail to Load Data! Error: " + ex.Message);
#endif
            }
        }

        public void SaveData()
        {
            try
            {
                var str = JsonHelper.Serialize(Voters);

                IOHelper.WriteToFile(str, m_filePath);
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Fail to save data! Error: " + e.Message);
#endif
            }
        }
        #endregion
    }
}
