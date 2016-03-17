using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LancheCurdScaffolding.Modles
{
    public class MetadataFieldViewModel
    {
        private ColumnInfo _columnInfo;
       
        public MetadataFieldViewModel(ColumnInfo columnInfo)
        {
            this._columnInfo = columnInfo;
        }
        public bool IsPrimaryKey { get { return _columnInfo.IsPrimaryKey; } set { _columnInfo.IsPrimaryKey = value; } }
        public string Name 
        { 
            get { return _columnInfo.Name; } 
            set { _columnInfo.Name = value; } 
        }
        public string DisplayName 
        {
            get { return _columnInfo.DisplayName; }
            set { _columnInfo.DisplayName = value; }
        }
        public bool Nullable
        {
            get { return _columnInfo.Nullable; }
            set { _columnInfo.Nullable = value; }
        }
        public bool IsVisible 
        { 
            get { return _columnInfo.IsVisible; }
            set { _columnInfo.IsVisible = value; }
        }
        public string strDateType 
        { 
            get { return _columnInfo.strDateType; }
            set { _columnInfo.strDateType = value; }
        }
        public int MaxLength
        { 
            get { return _columnInfo.MaxLength; }
            set { _columnInfo.MaxLength = value; }
        }
        public int RangeMin 
        { 
            get { return _columnInfo.RangeMin; }
            set { _columnInfo.RangeMin = value; }
        }
        public int RangeMax 
        {
            get { return _columnInfo.RangeMax; }
            set { _columnInfo.RangeMax = value; }
        }
        public Visibility ShowEditorMaxLength
        {
            get
            {
                if (_columnInfo.DataType == euColumnType.stringCT)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            
        }
        public Visibility ShowEditorRange
        {
            get
            {
                //if (_columnInfo.DataType == euColumnType.intCT
                //    || _columnInfo.DataType == euColumnType.decimalCT
                //    || _columnInfo.DataType == euColumnType.longCT
                //    )
                //    return Visibility.Visible;
                //else
                    return Visibility.Collapsed;
            }
       }
    }
}
