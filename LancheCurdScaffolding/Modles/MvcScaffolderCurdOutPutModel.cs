using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Modles
{
   public  class MvcScaffolderCurdOutPutModel
    {
       public ProjectInformation ProjectInformation { get; set; }
       public string EntityName { get; set; }
       public bool IsPaging { get; set; }
       public bool IsGenerateView { get; set; }
       public bool AddJs { get; set; }
       public bool AddMasterPage { get; set; }
       public string MasterPageFolderName { get; set; }
       public string ControllerName { get; set; }
       public string ViewTitle { get; set; }
       public string RootFolder { get; set; }
       public string DbContextName { get; set; }
       public string ServicePrefixName { get; set; }
    }
}
