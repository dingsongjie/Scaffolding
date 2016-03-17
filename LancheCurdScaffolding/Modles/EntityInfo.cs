using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Modles
{
     [Serializable]
    class EntityInfo
    {
         
   
        public List<ColumnInfo> Columns { get; set; }

        public ColumnInfo this[string name]
        {
            get { return this.Columns.FirstOrDefault(x => x.Name == name); }
        }
        public ColumnInfo this[int index]
        {
            get { return this.Columns[index]; }
        }

        public EntityInfo()
        {
            this.Columns = new List<ColumnInfo>();
        }
    }
}
