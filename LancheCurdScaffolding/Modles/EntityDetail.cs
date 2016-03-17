using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Modles
{
    public  class EntityDetail
    {
        public List<MetadataFieldViewModel> Columns { get; set; }
        public MvcScaffolderCurdOutPutModel MvcModel{get;set;}
        public EntityDetail()
        {
            Columns = new List<MetadataFieldViewModel>();
        }
    }
}
