namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class GameLogic {
  public partial record State {
    public partial record Paused : State, IGet<Input.OnPausePressed> {
      public Paused() {
        OnAttach(() => Get<IGameRepo>().Paused.Changed += OnGamePausedChanged);
        OnDetach(() => Get<IGameRepo>().Paused.Changed -= OnGamePausedChanged);

        this.OnEnter(() => Get<IGameRepo>().Pause());
      }

      public Transition On(in Input.OnPausePressed input) => To<Playing>();

      private void OnGamePausedChanged(bool paused) {
        if (!paused) {
          Input(new Input.OnPausePressed());
        }
      }

    }
  }
}
