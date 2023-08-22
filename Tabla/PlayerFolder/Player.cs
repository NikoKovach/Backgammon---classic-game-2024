
namespace Tabla.PlayerFolder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tabla.Enums;
    using Tabla.Model.Interfaces;
    using Tabla.PlayerFolder.Contracts;
    using Tabla.ServicesFolder;

    public class Player : IPlayer
    {

        // Have a method RollDice
        private string name;
        private Color color;
        private  IList<IPool> myPools;
        private readonly IList<IPool> outPools;
        private readonly IList<IPool> bitenPools;

        public Player(string name,Color color)
        {
            this.Name = name;
            this.Color = color;
            //this.myPools = new List<IPool>();
            this.outPools = new List<IPool>();
            bitenPools = new List<IPool>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (value.Length < TableGlobalConstants.MinLenghtPlayerName)
                {
                    throw new ArgumentException("Name must contains at least 3 characters.");
                }
                this.name = value;
            }
        }

        public Color Color
        {
            get
            { 
                return this.color;
            }
            private set
            {
                if (value != Color.White && value != Color.Black)
                {
                    throw new ArgumentException("Pool color can be only black or white!");
                }
                this.color = value;
            }
        }

        public IList<IPool> MyPools
        {
            get { return new List<IPool>(this.myPools); }
        }

        public IList<IPool> OutPools
        {
            get { return new List<IPool>(this.outPools); }
        }

        public IList<IPool> BitenPools
        {
            get { return new List<IPool>(this.bitenPools); }
        }

        public void SetMyPools(IList<IPool> basicPools)
        {
            GlobalValidateClass.NullArgumentValidate(basicPools);
            this.myPools = basicPools;
        }

        public void AddToOutList(IPool pool)
        {
            if (this.outPools.Count <= TableGlobalConstants.MaxPoolsNumber 
                && pool.Color == this.Color)
            {
                this.outPools.Add(pool);
            }
        }

        public void AddToBitenList(IPool pool)
        {
            if (this.bitenPools.Count <= TableGlobalConstants.MaxPoolsNumber
                && pool.Color == this.Color)
            {
                this.bitenPools.Add(pool);
            }
        }

        public IPool RemoveFromBitenList()
        {
            IPool setPoolOnBoard = null;
            if (this.BitenPools.Count > 0)
            {
                setPoolOnBoard = this.bitenPools.Last();
                this.bitenPools.Remove(setPoolOnBoard);
            }

            return setPoolOnBoard;          
        }
    }
}
