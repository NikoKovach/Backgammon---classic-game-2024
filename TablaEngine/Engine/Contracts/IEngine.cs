using TablaGameLogic.Core.Contracts;

namespace TablaEngine.Engine.Contracts
{
    public interface IEngine
     {
          IController Controller { get; }

          void RegistrationOfPlayers();

          void ChoiceOfColorByThePlayers();

          void PlayersArrangeTheirCheckers();

          void WhoWillMakeTheFirstMove();

          void Run();
     }
}
