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
    internal class ProjectInformation
    {
        private readonly CodeGenerationContext _context;
        private List<ModelType> _dbContextTypeCollection;
       // private List<ModelType> _modelTypeCollection;
        private ICodeTypeService _codeTypeService;
        private Project _project;
        public ProjectInformation(CodeGenerationContext context)
        {
            this._context = context;
            this._codeTypeService = CommonHelper.GetService<ICodeTypeService>(context);
            this._project = context.ActiveProject;
            /// dbcontexts
            var modelTypes = _codeTypeService.GetAllCodeTypes(_project)
                                             .Where(m => m.FullName == "Lanche.Entityframework.UnitOfWork.DbContextBase" || m.FullName == "Lanche.MongoDB.DbContext.MongoDbContext")
                                             .Select(m => new ModelType(m));

            _dbContextTypeCollection = new List<ModelType>();
            _dbContextTypeCollection.AddRange(modelTypes);        
        }
    }
}
