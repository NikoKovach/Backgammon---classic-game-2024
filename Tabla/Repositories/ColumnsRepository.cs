namespace Tabla.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Tabla.Model.Interfaces;
    using Tabla.Repositories.Contracts;

    public class ColumnsRepository : IColumnRepository
    {
        private readonly IList<IColumn> columns;

        public ColumnsRepository()
        {
            this.columns = new List<IColumn>();
        }

        public IList<IColumn> Columns
        {
            get
            {
                return new List<IColumn>(this.columns);
            }
        }

        public void AddColumn(IColumn newColumn)
        {
            int idNumber = newColumn.IdentityNumber;

            if (!this.Columns.Any(x => x.IdentityNumber == idNumber))
            {
                this.columns.Add(newColumn);
            };
        }
    }
}
