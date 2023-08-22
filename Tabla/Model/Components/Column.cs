namespace Tabla.Model.Components
{
    using System;
    using System.Collections.Generic;
    using Tabla.Model.Interfaces;
    using Tabla.ServicesFolder;

    public class Column : IColumn
    {
        private int identityNumber;
        private string color;
        private Stack<IPool> poolStack;

        public Column(int idNumber)
        {
            this.IdentityNumber = idNumber;
            InitiliazeStack();
        }

        private void InitiliazeStack()
        {
            this.poolStack = new Stack<IPool>();
        }
        
        public int IdentityNumber
        {
            get 
            { 
                return this.identityNumber; 
            }
            private set 
            {
                if (value < 1 || value > TableGlobalConstants.ColumnNumber)
                {
                    throw new ArgumentException("Use number in the range 1 - 24");
                }
                this.identityNumber = value;
            }
        }

        public string Color
        {
            get 
            {
                return this.color;
            }
            set 
            {
                if (value == null || value == string.Empty)
                {
                    throw new ArgumentException("Pool color can be only black or white!");
                }
                this.color = value;
            }
        }

        public Stack<IPool> PoolStack
        {
            get 
            { 
                return new Stack<IPool>(this.poolStack);
            }
        }

        public bool AddPool(IPool pool)
        {
            if (this.PoolStack.Count >= 0 )
            {
                this.poolStack.Push(pool);
                return true;
            }

            int colorOfLastelement = (int)this.PoolStack.Peek().Color;
            if (colorOfLastelement == (int)(pool.Color) && this.PoolStack.Count >= 1 )
            {
                this.poolStack.Push(pool);
                return true;
            }

            if (colorOfLastelement != (int)(pool.Color) && this.PoolStack.Count == 1)
            {
                IPool beatenPool = this.RemovePool(); 
                this.poolStack.Push(pool);
                return true;
            }

            return false;
        }

        public IPool RemovePool()
        {
            IPool deletedPool = null;
            if (this.PoolStack.Count >  0)
            {
                deletedPool = this.poolStack.Pop();
                
            }
            return deletedPool;
        }

        public IPool GetPool()
        {
            return this.PoolStack.Peek();
        }
    }
}
