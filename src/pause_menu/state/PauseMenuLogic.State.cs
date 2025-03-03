namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class PauseMenuLogic {
  public partial record State : StateLogic<State>, IGet<Input.OnResumePressed>, IGet<Input.OnQuitPressed> {
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

    public Transition On(in Input.OnResumePressed input) {
      Get<IGameRepo>().Resume();
      return ToSelf();
    }

    public Transition On(in Input.OnQuitPressed input) {
      Get<IGameRepo>().RequestQuit();
      return ToSelf();
    }

    private void OnGamePausedSync(bool paused) => Output(new Output.UpdateVisibility(paused));
  }
}
