using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancheCurdScaffolding.Modles
{
    [Serializable]
    public class ColumnInfo
    {
        public string ShortTypeName { get; set; }
        public string strDateType { get; set; }
        public euColumnType DataType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Nullable { get; set; }
        public int MaxLength { get; set; }
        public int RangeMin { get; set; }
        public int RangeMax { get; set; }

        public bool IsVisible { get; set; }
        public bool IsPrimaryKey { get; set; }

        public bool IsNumeric
        {
            get { return ((this.DataType & euColumnType.intCT) == euColumnType.intCT); }
        }

        public bool HasMetaAttribute
        {
            get
            {
                return (MetaAttribute != string.Empty);
            }
        }

        public string MetaAttribute
        {
            get
            {
                switch (this.DataType)
                {
                    case euColumnType.stringCT:
                        if (this.MaxLength > 0)
                            return string.Format("[MaxLength({0})]", this.MaxLength);
                        else
                            break;
                    case euColumnType.longCT:
                    case euColumnType.intCT:
                    case euColumnType.decimalCT:
                    case euColumnType.floatCT:
                    case euColumnType.doubleCT:
                        if (this.RangeMin > 0 || this.RangeMax > 0)
                            return string.Format("[Range({0}, {1})]", this.RangeMin, this.RangeMax);
                        else
                            break;
                    default:
                            break;
                }
                return string.Empty;
            }
        }


        public ColumnInfo() { }

        public ColumnInfo(Microsoft.AspNet.Scaffolding.Core.Metadata.PropertyMetadata property)
            : this(property.PropertyName, property.ShortTypeName, (property.RelatedModel != null), property.IsPrimaryKey)
        {
        }

        public ColumnInfo(CodeParameter property)
            : this(property.Name, property.Type.AsString, false)
        {
        }

        public ColumnInfo(CodeProperty property)
            : this(property.Name, property.Type.AsString, false)
        {
        }
        private ColumnInfo(string strName, string strType, bool relatedModel)
        {
            this.Name = strName;
            this.ShortTypeName = strType;
            this.strDateType = strType.Replace("?", "").Replace("System.", "").ToLower();

            if (!relatedModel)
                this.DataType = GetColumnType(this.strDateType);
            else
            {
                this.DataType = euColumnType.RelatedModel;
            }

            DisplayName = this.Name;
            Nullable = true;
            IsVisible = true;
        }
        private ColumnInfo(string strName, string strType, bool relatedModel, bool isPrimaryKey)
        {
            this.Name = strName;
            this.ShortTypeName = strType;
            this.strDateType = strType.Replace("?", "").Replace("System.", "").ToLower();
            this.IsPrimaryKey = isPrimaryKey;
            if (!relatedModel)
                this.DataType = GetColumnType(this.strDateType);
            else
            {
                this.DataType = euColumnType.RelatedModel;
            }

            DisplayName = this.Name;
            Nullable = true;
            IsVisible = true;
        }

        public euColumnType GetColumnType(string shortTypeName)
        {
            return ParseEnum(shortTypeName);
        }

        //private static T ParseEnum<T>(string value)
        //{
        //    value = value+ "CT";
        //    return (T)Enum.Parse(typeof(T), value, true);
        //}

        //private static T ParseEnum2<T>(string value)
        //{
        //    value = value + "CT";
        //    T result;
        //    if (Enum.TryParse<T>(value, true, out result))
        //    {
        //        return result;
        //    }
        //    else
        //        return default(T);
        //}

        private static euColumnType ParseEnum(string value)
        {
            value = value + "CT";
            euColumnType result;
            if (Enum.TryParse<euColumnType>(value, true, out result))
            {
                return result;
            }
            else
                return euColumnType.stringCT;
        }
    }
}

