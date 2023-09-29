using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    public abstract class AbstractData<T>
    {
        protected List<T> _data;
        protected DataTable _dataTable;
        protected DataTableGenerator _dataTableGen;
        protected bool _isFetchComplete;
        protected dynamic _query;

        protected AbstractData()
        {
            _dataTableGen = new DataTableGenerator(ColumnSettings());
            _dataTable = _dataTableGen.CreateDataTable<T>(null);
            _isFetchComplete = false;
        }

        protected AbstractData(object query)
        {
            _query = query;
            _dataTableGen = new DataTableGenerator(ColumnSettings());
            _dataTable = _dataTableGen.CreateDataTable<T>(null);
            _isFetchComplete = false;
        }

        protected virtual string RelativeUrl()
        {
            return "";
        }

        protected virtual List<ColumnSettings> ColumnSettings()
        {
            return new List<ColumnSettings>();
        }

        public virtual DataColumn[] GetColumn()
        {
            return _dataTableGen.GetDataColumns().ToArray();
        }

        public void SetQuery(dynamic query)
        {
            _query = query;
        }

        public bool IsFetchComplete()
        {
            return _isFetchComplete;
        }

        public virtual List<T> GetData()
        {
            return _data;
        }

        public async Task Refresh()
        {
            _isFetchComplete = false;
            await RunRefresh();
            _isFetchComplete = true;
        }

        protected virtual async Task RunRefresh()
        {
        }

        public virtual DataTable GetDataTable()
        {
            return _dataTable;
        }

        public virtual void UpdateData(int i, T Data)
        {
            if (i < 0)
            {
                return;
            }

            _data[i] = Data;
            _dataTable = _dataTableGen.CreateDataTable<T>(_data);
        }

        public virtual void UpdateDatatableAndData(Func<DataRow, bool> dataTableCondition, int dataIndex, T Data)
        {
            if (dataIndex >= 0)
            {
                _data[dataIndex] = Data;
            }

            _dataTableGen.UpdateRowWhere(dataTableCondition, Data);
        }

        internal virtual async Task<TReturn> Delete(string primaryKeyValue)
        {
            var rest = new Rest($"{RelativeUrl()}/{primaryKeyValue}");
            return await rest.Delete();
        }

        internal virtual async Task<DoranApp.Utils.TReturn> Restore(string primaryKeyValue)
        {
            var rest = new Rest($"{RelativeUrl()}/{primaryKeyValue}/restore");
            var result = await rest.Delete();
            return result;
        }

        public virtual async Task<object> CreateOrUpdate(string primaryKeyValue, object data)
        {
            return await CreateOrUpdate<TReturn>(primaryKeyValue, data);
        }

        public virtual async Task<TReturn> CreateOrUpdate<TReturn>(string primaryKeyValue, object data)
        {
            var isEdit = string.IsNullOrEmpty(primaryKeyValue) == false;
            var uri = isEdit ? $"{RelativeUrl()}/{primaryKeyValue}" : $"{RelativeUrl()}";

            var rest = new Rest(uri);
            try
            {
                var method = isEdit ? "Put" : "Post";
                var _rest = typeof(Rest).GetMethod(method);
                var result = await (Task<TReturn>)_rest.Invoke(rest, new object[] { data });
                return result;
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex, "ERRRR");
                throw;
            }
        }
    }
}