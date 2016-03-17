using LancheCurdScaffolding.Modles;
using LancheCurdScaffolding.Ui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LancheCurdScaffolding.Ui
{
    /// <summary>
    /// MvcScaffolder.xaml 的交互逻辑
    /// </summary>
    public partial class MvcScaffolderWindow : VSPlatformDialogWindow
    {
        private ProjectInformation _model;
        public MvcScaffolderCurdOutPutModel OutPutModel { get; set; }
        public MvcScaffolderWindow(ProjectInformation model)
        {
            this._model = model;
           InitializeComponent();
        }
       

        private void VSPlatformDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
              this.dbContextTypeComboBox.ItemsSource = _model.DbContextTypeCollection;
              var servicePrefixNmae = System.Configuration.ConfigurationManager.AppSettings["servicePrefixName"];
              servicePrefix.Text = servicePrefixNmae;

              this.masterPage.IsEnabled = false;
        }

        private void dbContextTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             var v =  this.dbContextTypeComboBox.SelectedValue.ToString();
             var dbContext = _model.DbContextTypeCollection.SingleOrDefault(m => m.DisplayName == v);
             var nameSpace = dbContext.DisplayName.Substring(dbContext.DisplayName.IndexOf('(') + 1, dbContext.DisplayName.Length - (dbContext.DisplayName.IndexOf('(') + 1)-1);
             var entities = _model.CodeTypeService.GetAllCodeTypes(_model.Project).Where(m => m.Namespace.FullName.Contains(nameSpace)).Select(m => new ModelType(m));;
             this.entityTypeComboBox.ItemsSource = entities;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             System.Configuration.ConfigurationManager.AppSettings["servicePrefixName"]= servicePrefix.Text;
            
            MvcScaffolderCurdOutPutModel outModel = new MvcScaffolderCurdOutPutModel()
            {
                AddJs = (bool)this.IsAddJs.IsChecked,
                AddMasterPage = (bool)this.AddMasterPage.IsChecked,
                ControllerName = this.controllerName.Text,
                EntityName = _model.CodeTypeService.GetAllCodeTypes(_model.Project).SingleOrDefault(m =>(m.Namespace != null && !String.IsNullOrWhiteSpace(m.Namespace.FullName)
                              ? String.Format("{0} ({1})", m.Name, m.Namespace.FullName)
                              : m.Name )== this.entityTypeComboBox.SelectedValue.ToString()).FullName,
                MasterPageFolderName = this.masterPage.Text,
                IsGenerateView = (bool)this.IsGenerateView.IsChecked,
                IsPaging = (bool)this.IsPaging.IsChecked,
                RootFolder = "",
                ViewTitle = this.viewTittle.Text,
                ProjectInformation = _model,
                 DbContextName=_model.CodeTypeService.GetAllCodeTypes(_model.Project).SingleOrDefault(m =>(m.Namespace != null && !String.IsNullOrWhiteSpace(m.Namespace.FullName)
                              ? String.Format("{0} ({1})", m.Name, m.Namespace.FullName)
                              : m.Name )== this.dbContextTypeComboBox.SelectedValue.ToString()).FullName,
                ServicePrefixName = servicePrefix.Text

            };
            OutPutModel = outModel;
            this.Close();
        }

        

        private void AddMasterPage_Click(object sender, RoutedEventArgs e)
        {
            if(this.AddMasterPage.IsChecked==true)
            {
                this.masterPage.IsEnabled = true;
            }
            else
            {
                this.masterPage.IsEnabled = false;
            }
        }

        private void entityTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.controllerName.Text = entityTypeComboBox.SelectedValue.ToString().Split(' ')[0] + "Controller";
            this.viewTittle.Text = entityTypeComboBox.SelectedValue.ToString().Split(' ')[0];
        } 
    }
}
