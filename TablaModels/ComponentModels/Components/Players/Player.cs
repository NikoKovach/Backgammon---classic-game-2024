namespace TablaModels.ComponentModels.Components.Players
{
    using System;
    using System.Collections.Generic;

    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public abstract class Player : IPlayer
    {        
        private string name;
        private IMoveChecker move;

        public Player(string name)
        {
            this.Name = name;

            this.State = PlayerState.NormalState;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value.Length < TableGlobalConstants.MinLenghtPlayerName)
                {
                    throw new ArgumentException(InvalidPlayerName);
                }

                if (value == null)
                {
                    throw new ArgumentNullException(NullPlayerName);
                }

                this.name = value;
            }
        }

        public PoolColor MyPoolsColor 
        { 
            get ; set ; 
        }

        public PlayerState State
        {
            get; set;
        }

        public IMoveChecker Move 
        {
            get
            { 
                return this.move; 
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(string.Format(NullMoveParameter,nameof(this.Move)));
                }

                this.move = value;
            }
        }

        public abstract void ArrangingTheCheckers( IBoard board);

        public int RollADice()
        {
            Random rnd = new Random();

            int number = rnd.Next
                (TableGlobalConstants.MinDiceValue, TableGlobalConstants.MaxDiceValue + 1);

            return number;
        }      
    }
}
