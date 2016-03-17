using LancheCurdScaffolding.Modles;
using LancheCurdScaffolding.Ui.Base;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
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
    /// EntityChooseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EntityChooseWindow : VSPlatformDialogWindow
    {
        public bool IsCancel { get; set; }
        private EntityDetail _model;
        public EntityDetail EntityDetail { get { return _model; }  }
        public EntityChooseWindow(EntityDetail model)
        {
            this._model = model;
            InitializeComponent();
           
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.list.ItemsSource = _model.Columns;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsCancel = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            this.Close();
        }
    }
}
