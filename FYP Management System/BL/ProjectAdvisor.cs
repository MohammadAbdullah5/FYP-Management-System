using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_Management_System.BL
{
    class ProjectAdvisor
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public int AdvisorRole { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
