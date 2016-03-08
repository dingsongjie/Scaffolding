using LancheCurdScaffolding.Helpers;
using LancheCurdScaffolding.Resources;
using LancheCurdScaffolding.Scaffolders;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Factory
{
    /// <summary>
    /// 入口
    /// </summary>
    [Export(typeof(CodeGeneratorFactory))]
    public class LancheMvcScaffolderFactory: CodeGeneratorFactory
    {
        public LancheMvcScaffolderFactory()
            : base(CreateCodeGeneratorInformation())
        {

        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new LancheMvcScaffolder(context, Information);
        }
        // 支持 c#  .net framework 4.5.1 以上
        public override bool IsSupported(CodeGenerationContext codeGenerationContext)
        {
            if (ProjectLanguage.CSharp.Equals(codeGenerationContext.ActiveProject.GetCodeLanguage()))
            {
                FrameworkName targetFramework = codeGenerationContext.ActiveProject.GetTargetFramework();
                return (targetFramework != null) &&
                        String.Equals(".NetFramework", targetFramework.Identifier, StringComparison.OrdinalIgnoreCase) &&
                        targetFramework.Version >= new Version(4, 5,1);
            }

            return false;
        }
        private static CodeGeneratorInformation CreateCodeGeneratorInformation()
        {
            return new CodeGeneratorInformation(
                displayName: "LancheProject mvc 增删改查代码生成",
                description: "LancheProject mvc CURD 快速生成",
                author: "dsj",
                version: new Version(0, 1, 0, 0),
                id: typeof(LancheMvcScaffolder).Name,
                icon: CommonHelper.ToImageSource(Resource.main),
                gestures: new[] { "Controller" },
                categories: new[] { Categories.Common, Categories.MvcController, Categories.Other }
            );
        }
    }
}
