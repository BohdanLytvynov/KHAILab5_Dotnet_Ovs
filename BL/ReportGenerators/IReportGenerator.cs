using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReportGenerators
{
    public interface IReportGenerator
    {
        DataTable GetReport(IEnumerable<Voter> voters);
    }
}
