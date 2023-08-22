namespace Tabla.Core.Commands
{
    using System;

    using Tabla.Core.Commands.Contracts;
    using Tabla.Repositories.Contracts;
    using Tabla.Enums;
    using Tabla.Repositories;
    using Tabla.ServicesFolder;
    using Tabla.Model.Interfaces;
    using Tabla.Factories.Contracts;
    using Tabla.Factories;

    public class CreatePoolsCommand : ICommand
    {
        private IPoolFactory poolFactory;
        private IPoolRepository poolsRepository;

        public CreatePoolsCommand(PoolFactory poolFactory, IPoolRepository poolsRepository)
        {
            this.PoolFactory = poolFactory;
            this.PoolsRepository = poolsRepository;
        }

        public IPoolRepository PoolsRepository
        {
            get 
            { 
                return this.poolsRepository; 
            }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.poolsRepository = value; 
            }
        }

        public IPoolFactory PoolFactory 
        {
            get
            { 
                return this.poolFactory; 
            }
            set
            {
                GlobalValidateClass.NullArgumentValidate(value);
                this.poolFactory = value; 
            }
        }

        public void Execute()
        {
            try
            {
                for (int i = 1; i <= TableGlobalConstants.MaxPoolsNumber; i++)
                {
                    IPool whitePool = this.PoolFactory.CreatePool(Color.White, i);
                    this.PoolsRepository.AddPool(whitePool);
                    IPool blackPool = this.PoolFactory.CreatePool(Color.Black, i);
                    this.PoolsRepository.AddPool(blackPool);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
