using EnvDTE;
using LancheCurdScaffolding.Helpers;
using LancheCurdScaffolding.Modles;
using LancheCurdScaffolding.Ui;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LancheCurdScaffolding.Scaffolders
{
    public class LancheMvcScaffolder : CodeGenerator
    {
        private EntityDetail _codeGeneratorModel;
        public LancheMvcScaffolder(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            var project = Context.ActiveProject;
            var selectionRelativePath = GetSelectionRelativePath();
            if (_codeGeneratorModel == null)
            {
                throw new InvalidOperationException("出现未知错误");
            }

            System.Windows.Input.Cursor currentCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                GenerateCode(project, selectionRelativePath, this._codeGeneratorModel);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }
        private void GenerateCode(Project project, string selectionRelativePath, EntityDetail codeGeneratorViewModel)
        {
            // Entity Type
            var modelTypeName = codeGeneratorViewModel.MvcModel.EntityName;

            //  dbContext
            string dbContextTypeName = codeGeneratorViewModel.MvcModel.DbContextName;
            ICodeTypeService codeTypeService = CommonHelper.GetService<ICodeTypeService>(ServiceProvider);
            CodeType dbContext = codeTypeService.GetCodeType(project, dbContextTypeName);

            // Get the Entity Framework Meta Data
            IEntityFrameworkService efService = CommonHelper.GetService<IEntityFrameworkService>(ServiceProvider);
            //ModelMetadata efMetadata = efService.AddRequiredEntity(Context, dbContextTypeName, modelTypeName);

            //  Controller
            string controllerName = codeGeneratorViewModel.MvcModel.ControllerName;
            string outputFolderPath = selectionRelativePath;
            string programTitle = codeGeneratorViewModel.MvcModel.ViewTitle;
            AddMvcController(project: project
                , controllerName: controllerName
                , outputPath: outputFolderPath
                );
         
            if (!codeGeneratorViewModel.MvcModel.IsGenerateView)
                return;


            string viewRootPath = GetViewsFolderPath(selectionRelativePath);
            AddView(project, codeGeneratorViewModel.Columns, codeGeneratorViewModel.MvcModel.ViewTitle, codeGeneratorViewModel.MvcModel.ServicePrefixName, codeGeneratorViewModel.MvcModel.EntityName, codeGeneratorViewModel.Columns.SingleOrDefault(m => m.IsPrimaryKey == true).Name, viewRootPath, controllerName);

        }

        public override bool ShowUIAndValidate()
        {
            try
            {
                var model = new ProjectInformation(Context);


                MvcScaffolderWindow window = new MvcScaffolderWindow(model);
                window.ShowDialog();

                IEntityFrameworkService efService = CommonHelper.GetService<IEntityFrameworkService>(Context); ;
                ModelMetadata efMetadata = efService.AddRequiredEntity(window.OutPutModel.ProjectInformation.Context, window.OutPutModel.DbContextName, window.OutPutModel.EntityName);
                EntityInfo dataModel = new EntityInfo();

                foreach (Microsoft.AspNet.Scaffolding.Core.Metadata.PropertyMetadata p1 in efMetadata.Properties)
                {
                    dataModel.Columns.Add(new ColumnInfo(p1));
                }
                EntityDetail entityDetail = new EntityDetail()
                {

                    MvcModel = window.OutPutModel
                };
                foreach (var column in dataModel.Columns)
                {
                    entityDetail.Columns.Add(new MetadataFieldViewModel(column));
                }
                EntityChooseWindow chooseWindow = new EntityChooseWindow(entityDetail);
                chooseWindow.ShowDialog();
                _codeGeneratorModel = chooseWindow.EntityDetail;
                return !chooseWindow.IsCancel;
            }
            catch
            {
                MessageBox.Show("创建dialog失败, 请自己修改源代码或者联系作者");
                return false;
            }


        }
        protected string GetSelectionRelativePath()
        {
            return Context.ActiveProjectItem == null ? String.Empty : CommonHelper.GetProjectRelativePath(Context.ActiveProjectItem);
        }
        //add MVC Controller
        private void AddMvcController(Project project
            , string controllerName
            , string outputPath

            )
        {

            if (String.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentException("controllerName");
            }
            outputPath += controllerName;
            string relativePath = outputPath.Replace(@"\", @"/");
            var templatePath = Path.Combine("Curd", "Controller");
            //var templatePath = "Controller";
            var defaultNamespace = GetDefaultNamespace();
            Dictionary<string, object> templateParams = new Dictionary<string, object>(){
                {"ControllerName", controllerName}
                , {"Namespace", defaultNamespace}
                , {"OverpostingWarningMessage", "联系 丁松杰"}              
            };

            AddFileFromTemplate(project: project
                , outputPath: outputPath
                , templateName: templatePath
                , templateParameters: templateParams
                 , skipIfExists: true);
        }
        protected string GetDefaultNamespace()
        {
            return Context.ActiveProjectItem == null
                ? Context.ActiveProject.GetDefaultNamespace()
                : Context.ActiveProjectItem.GetDefaultNamespace();
        }


        private string GetRelativeFolderPath(string selectionRelativePath, string folderName)
        {
            string keyControllers = "Controllers";
            string keyViews = folderName;

            return (
                (
                selectionRelativePath.IndexOf(keyControllers) >= 0)
                ? selectionRelativePath.Replace(keyControllers, keyViews)
                : Path.Combine(selectionRelativePath, keyViews)
                );
        }

        private void AddView(Project project, List<MetadataFieldViewModel> columns, string viewTitle, string servicePrefix, string entityName, string keyName, string viewsFolderPath,string controllerName)
        {
            List<string> ColumnNamesWithOutId = new List<string>();
            List<string> ColumnDisplayNamesWithOutId = new List<string>();
            foreach (var v in columns)
            {
                if(v.IsPrimaryKey==false)
                {
                    ColumnNamesWithOutId.Add(v.Name);
                }
                if (v.IsPrimaryKey == false)
                {
                    ColumnDisplayNamesWithOutId.Add(v.DisplayName);
                }
            }
            controllerName = controllerName.Replace("Controller", "");
            string outputPath = Path.Combine(viewsFolderPath, controllerName, "Index");
            var templatePath = Path.Combine("Curd", "View");
            Dictionary<string, object> templateParams = new Dictionary<string, object>(){
               // {"Columns", columns}
                 {"ViewTittle", viewTitle}
                , {"ServicePrefix", servicePrefix}  
                , {"EntityName", controllerName}
                ,{"KeyName", keyName}
                ,{"ColumnNamesWithOutId",ColumnNamesWithOutId}
                ,{"ColumnDisplayNamesWithOutId",ColumnDisplayNamesWithOutId}
            };
            AddFileFromTemplate(project: project
                , outputPath: outputPath
                , templateName: templatePath
                , templateParameters: templateParams
                 , skipIfExists: true);
        
        }
        /// <summary>
        /// Get Views folder
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private string GetViewsFolderPath(string selectionRelativePath)
        {
            //string keyControllers = "Controllers";
            //string keyViews = "Views";

            //return (
            //    (
            //    controllerPath.IndexOf(keyControllers) >= 0)
            //    ? controllerPath.Replace(keyControllers, keyViews)
            //    : Path.Combine(controllerPath, keyViews)
            //    );
            return GetRelativeFolderPath(selectionRelativePath, "Views");
        }


    }
}
