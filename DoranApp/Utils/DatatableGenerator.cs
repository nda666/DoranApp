using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DoranApp.Utils
{
    public class DataTableGenerator
    {
        private DataTable dt = new DataTable();
        private List<ColumnSettings> columnSettingsList = new List<ColumnSettings>();

        public DataTableGenerator(List<ColumnSettings> columnSettingsList)
        {
            this.columnSettingsList = columnSettingsList;

            foreach (var columnSettings in columnSettingsList)
            {
                DataColumn column = CreateColumn(columnSettings);
                dt.Columns.Add(column);
            }
        }

        public DataTable CreateDataTable<T>(IEnumerable<T> data)
        {
            dt.BeginInit();
            dt.Rows.Clear();

            if (data != null)
            {
                foreach (var item in data)
                {
                    var row = dt.NewRow();

                    foreach (var columnSettings in columnSettingsList)
                    {
                        string columnName = columnSettings.Name;
                        object value = columnSettings.PropertySelector(item);
                        row[columnName] = value ?? DBNull.Value;
                    }

                    dt.Rows.Add(row);
                }
            }
            dt.EndInit();
            return dt;
        }

        private DataColumn CreateColumn(ColumnSettings columnSettings)
        {
            string columnName = columnSettings.Name;
            Type columnType = columnSettings.DataType ?? typeof(string);

            if (IsNullableType(columnType))
            {
                columnType = Nullable.GetUnderlyingType(columnType) ?? columnType;
            }

            return new DataColumn(columnName, columnType);
        }

        public List<DataColumn> GetDataColumns()
        {
            var columns = new List<DataColumn>();

            foreach (var columnSettings in columnSettingsList)
            {
                DataColumn column = CreateColumn(columnSettings);
                columns.Add(column);
            }

            return columns;
        }

        public List<string> GetColumnNames()
        {
            return columnSettingsList.Select(columnSettings => columnSettings.Name).ToList();
        }

        private bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

    public class ColumnSettings
    {
        public string Name { get; }
        public Type DataType { get; }
        public Func<object, object> PropertySelector { get; }

        public ColumnSettings(string name, Func<object, object> propertySelector, Type dataType = null)
        {
            Name = name;
            DataType = dataType ?? GetPropertyType(propertySelector);
            PropertySelector = propertySelector;
        }

        private Type GetPropertyType(Func<object, object> propertySelector)
        {
            if (propertySelector.Target is MemberExpression memberExpression)
            {
                return memberExpression.Member is PropertyInfo propertyInfo ? propertyInfo.PropertyType : typeof(object);
            }

            return typeof(object);
        }
    }

    public class ColumnSettings<T> : List<ColumnSettings>
    {
        public void Add<TProperty>(string name, Func<T, TProperty> propertySelector, Type dataType = null)
        {
            var converter = new Func<object, object>(item => propertySelector((T)item));
            Add(new ColumnSettings(name, converter, dataType));
        }
    }
}