using DoranApp.Exceptions;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    public abstract class AbstractData<T>
    {
        protected bool _isFetchComplete;
        protected List<T> _data;
        protected dynamic _query;
        protected DataTable _dataTable;
        protected virtual string RelativeUrl() { return ""; }
        protected AbstractData()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.AddRange(GetColumn());
            _isFetchComplete = false;
        }
        protected AbstractData(object query)
        {
            _query = query;
            _dataTable = new DataTable();
            _dataTable.Columns.AddRange(GetColumn());
            _isFetchComplete = false;
        }


        public virtual DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"), };
            return dataColumns;
        }

        public void SetQuery(dynamic query)
        {
            _query = query;
        }
        public bool IsFetchComplete()
        {
            return _isFetchComplete;
        }

        public List<T> GetData()
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

        public virtual async Task<object> CreateOrUpdate(string primaryKeyValue, object data)
        {
            return await CreateOrUpdate<TReturn>( primaryKeyValue,  data);
        }

        public virtual async Task<TReturn> CreateOrUpdate<TReturn>(string primaryKeyValue, object data)
        {

            var isEdit = string.IsNullOrEmpty(primaryKeyValue) == false;
            var uri = isEdit ? $"{RelativeUrl()}/{primaryKeyValue}" : $"{RelativeUrl()}";

            ConsoleDump.Extensions.Dump(uri, "ERRRR");
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
