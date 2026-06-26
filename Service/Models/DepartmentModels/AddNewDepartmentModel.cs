using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.DepartmentModels
{
    public class AddNewDepartmentModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OperationalStatus { get; set; }
    }
}
