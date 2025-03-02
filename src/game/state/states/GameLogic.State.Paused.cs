namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class GameLogic {
  public partial record State {
    public partial record Paused : State, IGet<Input.OnPausePressed> {
      public Paused() {
        OnAttach(() => { });
        OnDetach(() => { });

        this.OnEnter(() => {
          Get<IGameRepo>().Pause();
          Output(new Output.Pause());
        });

        this.OnExit(() => {
          Get<IGameRepo>().Resume();
          Output(new Output.Resume());
        });
      }

      public Transition On(in Input.OnPausePressed input) => To<Playing>();
    }
  }
}
