using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class VoterDto
    {
        public string Surename { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public int CountyNumber { get; set; }
        public string Party { get; set; }

        public int Age { get; set; }

        public string ProfessionName { get; set; }
        public int Id { get; set; }
    }
}
