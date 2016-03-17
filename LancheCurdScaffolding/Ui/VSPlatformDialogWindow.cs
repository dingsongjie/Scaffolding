using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Ui.Base
{
    //Create the class to avoid referencing the assembly of "Microsoft.VisualStudio.shell.<version>.dll" in xaml,
    //becasue the same xaml source will be used by both dev11 and dev12 OOB 
    [ExcludeFromCodeCoverage]
    public class VSPlatformDialogWindow : Microsoft.VisualStudio.PlatformUI.DialogWindow
    {
    }
}
