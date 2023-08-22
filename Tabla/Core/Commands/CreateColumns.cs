namespace Tabla.Core.Commands
{
    using System;

    using Tabla.Core.Commands.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.Repositories;
    using Tabla.ServicesFolder;
    using Tabla.Model.Interfaces;
    using Tabla.Factories.Contracts;
    using Tabla.Factories;

    public class CreateColumns : ICommand
    {       
        private IColumnFactory columnFactory;
        private IColumnRepository columnsRepository;

        public CreateColumns(ColumnFactory columnFactory, IColumnRepository columnsRepository)
        {
            this.ColumnFactory = columnFactory;
            this.ColumnsRepository = columnsRepository;
        }

        public IColumnFactory ColumnFactory 
        {
            get { return this.columnFactory; }
            set 
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.columnFactory = value; 
            }
        }

        public IColumnRepository ColumnsRepository 
        {
            get { return this.columnsRepository; }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.columnsRepository = value;
            }
        }

        public void Execute()
        {
            try
            {
                for (int i = 1; i <= TableGlobalConstants.ColumnNumber; i++)
                {
                    IColumn newcolumn = this.ColumnFactory.CreateColumn(i);
                    this.ColumnsRepository.AddColumn(newcolumn);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
