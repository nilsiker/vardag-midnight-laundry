namespace Vardag;

using Chickensoft.LogicBlocks;
using Godot;


public partial class GameLogic {
  public abstract partial record State : StateLogic<State> {
    protected State() {
      OnAttach(() => {
        var game = Get<IGameRepo>();
        game.Paused.Sync += OnGamePausedSync;
        game.QuitRequested += OnGameQuitRequested;
      });
      OnDetach(() => {
        var game = Get<IGameRepo>();
        game.Paused.Sync -= OnGamePausedSync;
        game.QuitRequested -= OnGameQuitRequested;
      });
    }

    private void OnGameQuitRequested() => Output(new Output.QuitGame());

    protected virtual void OnGamePausedSync(bool paused) {
      Output(new Output.SetPauseMode(paused));
      Output(new Output.CaptureMouse(!paused));
    }
  }
}
