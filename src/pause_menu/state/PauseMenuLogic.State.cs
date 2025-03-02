namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class PauseMenuLogic {
  public partial record State : StateLogic<State> {
    public State() {
      OnAttach(() => {
        var game = Get<IGameRepo>();
        game.Paused.Sync += OnGamePausedSync;
      });
      OnDetach(() => {
        var game = Get<IGameRepo>();
        game.Paused.Sync -= OnGamePausedSync;
      });
    }

    private void OnGamePausedSync(bool paused) => Output(new Output.UpdateVisibility(paused));
  }
}
