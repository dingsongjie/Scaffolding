using EnvDTE;
using LancheCurdScaffolding.Helpers;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Modles
{
    /// <summary>
    /// 当前项目信息
    /// </summary>
    public class ProjectInformation
    {
        public CodeGenerationContext Context { get; set; }
        public List<ModelType> DbContextTypeCollection { get; set; }
        public ICodeTypeService CodeTypeService { get; set; }

        public Project Project { get; set; }
        public ProjectInformation(CodeGenerationContext context)
        {
            this.Context = context;
            this.CodeTypeService = CommonHelper.GetService<ICodeTypeService>(context);
            this.Project = context.ActiveProject;
            /// dbcontexts
            var dbContextTypes = CodeTypeService.GetAllCodeTypes(Project)
                                             .Where(m => m.IsDerivedType( "Lanche.Entityframework.UnitOfWork.DbContextBase") ||  m.IsDerivedType( "Lanche.MongoDB.DbContext.MongoDbContext"))
                                             .Select(m => new ModelType(m));

            DbContextTypeCollection = new List<ModelType>();
            DbContextTypeCollection.AddRange(dbContextTypes);
            
           

          

        }
    }
}
