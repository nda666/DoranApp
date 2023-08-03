using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;

namespace DoranApp.Utils
{
    public class DataTableGenerator<T>
    {

        DataTable dt = new DataTable();
        PropertyInfo[] properties = null;
        public DataTableGenerator()
        {
            // Get the properties of the class using reflection
            properties = typeof(T).GetProperties();

            // Add columns to the DataTable based on the properties with ColumnAttribute
            foreach (var prop in properties)
            {
                var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

                if (columnAttr == null || !columnAttr.Hide)
                {
                    DataColumn column = CreateColumn(prop);
                    dt.Columns.Add(column);
                }
            }
        }
        public DataTable CreateDataTable(IEnumerable<T> data)
        {
            dt.BeginInit();
            dt.Rows.Clear();

            if (data != null)
            {
                // Add data rows to the DataTable
                foreach (var item in data)
                {
                    var row = dt.NewRow();

                    foreach (var prop in properties)
                    {
                        var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

                        if (columnAttr == null || !columnAttr.Hide)
                        {
                            string columnName = GetColumnName(prop);
                            object value = prop.GetValue(item);
                            row[columnName] = value ?? DBNull.Value;
                        }
                    }

                    dt.Rows.Add(row);
                }
            }
            dt.EndInit();
            return dt;
        }

        private DataColumn CreateColumn(System.Reflection.PropertyInfo property)
        {
            string columnName = GetColumnName(property);
            Type columnType = GetColumnType(property);
            if (IsNullableType(columnType))
            {
                columnType = Nullable.GetUnderlyingType(columnType) ?? columnType;
            }
            return new DataColumn(columnName, columnType);
        }

        private string GetColumnName(System.Reflection.PropertyInfo property)
        {
            var columnAttr = property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            return columnAttr?.Name ?? property.Name;
        }

        private Type GetColumnType(System.Reflection.PropertyInfo property)
        {
            var columnAttr = property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            return columnAttr?.DataType ?? property.PropertyType;
        }

        public List<DataColumn> GetDataColumns()
        {
            var properties = typeof(T).GetProperties();
            var columns = new List<DataColumn>();

            foreach (var prop in properties)
            {
                var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

                if (columnAttr == null || !columnAttr.Hide)
                {
                    DataColumn column = CreateColumn(prop);
                    columns.Add(column);
                }
            }

            return columns;
        }
        private bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

    public class ColumnAttribute : Attribute
    {
        public string Name { get; }
        public Type DataType { get; }
        public bool Hide { get; }

        public ColumnAttribute(string name = "", Type dataType = null, bool hide = false)
        {
            Name = name;
            DataType = dataType;
            Hide = hide;
        }
    }
}
