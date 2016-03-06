using LancheCurdScaffolding.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LancheCurdScaffolding.Ui
{
    class MvcScaffolderDialog:Form
    {
        private ProjectInformation _information;
        public MvcScaffolderDialog(ProjectInformation model)
        {
            this._information = model;
        }
    }
}
