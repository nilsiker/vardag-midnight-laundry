namespace Vardag;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IGame : ICanvasLayer, IStateInfo, IProvide<IGameRepo> { }

[InputMap]
[Meta(typeof(IAutoNode))]
public partial class Game : CanvasLayer, IGame {
  #region Exports
  #endregion

  #region Nodes
  #endregion

  #region Provisions
  IGameRepo IProvide<IGameRepo>.Value() => GameRepo;
  #endregion

  #region Dependencies
  #endregion

  #region State
  private IGameRepo GameRepo { get; set; } = default!;
  private GameLogic Logic { get; set; } = default!;
  private GameLogic.IBinding Binding { get; set; } = default!;

  string IStateInfo.Name => Name;

  public string State => Logic.Value.ToString();
  #endregion

  #region Dependency Lifecycle
  public void Setup() {
    Logic = new();
    GameRepo = new GameRepo();
  }

  public void OnResolved() {
    Binding = Logic.Bind();

    // Bind functions to state outputs here
    Binding
      .Handle((in GameLogic.Output.Pause _) => OnOutputPause())
      .Handle((in GameLogic.Output.Resume _) => OnOutputResume());

    Logic.Set(GameRepo);
    Logic.Start();

    this.Provide();
  }
  #endregion

  #region Godot Lifecycle
  public override void _Notification(int what) => this.Notify(what);

  public void OnReady() {
    SetProcess(true);
    SetPhysicsProcess(true);

    AddToGroup("state");
  }

  public void OnProcess(double delta) { }

  public void OnPhysicsProcess(double delta) { }

  public override void _UnhandledInput(InputEvent @event) {
    if (@event.IsActionPressed(Pause)) {
      Logic.Input(new GameLogic.Input.OnPausePressed());
    }
  }

  public void OnExitTree() {
    Logic.Stop();
    Binding.Dispose();
  }
  #endregion

  #region Input Callbacks
  #endregion

  #region Output Callbacks
  private static void OnOutputPause() {
    Engine.TimeScale = 0f;
    Input.MouseMode = Input.MouseModeEnum.Visible;
  }

  private static void OnOutputResume() {
    Engine.TimeScale = 1f;
    Input.MouseMode = Input.MouseModeEnum.Captured;
  }
  #endregion
}
