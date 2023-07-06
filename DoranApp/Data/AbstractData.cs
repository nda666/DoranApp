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
    }
}
