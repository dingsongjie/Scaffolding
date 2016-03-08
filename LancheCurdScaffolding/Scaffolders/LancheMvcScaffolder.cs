using LancheCurdScaffolding.Modles;
using LancheCurdScaffolding.Ui;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LancheCurdScaffolding.Scaffolders
{
    public class LancheMvcScaffolder : CodeGenerator
    {
        public LancheMvcScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            MessageBox.Show("ok");
        }

        public override bool ShowUIAndValidate()
        {
            try
            {
                var model = new ProjectInformation(Context);


                MvcScaffolderWindow window = new MvcScaffolderWindow(model);
                window.ShowDialog();
                //MvcScaffolderForm window = new MvcScaffolderForm(model);
                //window.Show();
                EntityChooseWindow chooseWindow = new EntityChooseWindow(window.OutPutModel);
                window.ShowDialog();
                return true;
            }
            catch
            {
                MessageBox.Show("创建dialog失败, 请自己修改源代码或者联系作者");
                return false;
            }

           
        }

    }
}
