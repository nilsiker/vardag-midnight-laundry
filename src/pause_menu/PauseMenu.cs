namespace Vardag;

using System;
using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IPauseMenu : IControl { }

[Meta(typeof(IAutoNode))]
public partial class PauseMenu : Control, IPauseMenu {
  #region Exports
  #endregion

  #region Nodes
  [Node] private Button ResumeButton { get; set; } = default!;
  [Node] private Button OptionsButton { get; set; } = default!;
  [Node] private Button QuitButton { get; set; } = default!;
  #endregion

  #region Provisions
  #endregion

  #region Dependencies
  [Dependency] private IGameRepo GameRepo => this.DependOn<IGameRepo>();
  #endregion

  #region State
  private PauseMenuLogic Logic { get; set; } = default!;
  private PauseMenuLogic.IBinding Binding { get; set; } = default!;
  #endregion

  #region Dependency Lifecycle
  public void Setup() => Logic = new();

  public void OnResolved() {
    Binding = Logic.Bind();

    // Bind functions to state outputs here
    Binding
      .Handle((in PauseMenuLogic.Output.UpdateVisibility output) => OnOutputUpdateVisibility(output.Visible));

    Logic.Set(GameRepo);
    Logic.Start();
  }

  private void OnOutputUpdateVisibility(bool visible) => Visible = visible;
  #endregion

  #region Godot Lifecycle
  public override void _Notification(int what) => this.Notify(what);

  public void OnReady() {
    SetProcess(true);
    SetPhysicsProcess(true);

    ResumeButton.Pressed += OnResumeButtonPressed;
    QuitButton.Pressed += OnQuitButtonPressed;
  }


  public void OnProcess(double delta) { }

  public void OnPhysicsProcess(double delta) { }

  public void OnExitTree() {
    Logic.Stop();
    Binding.Dispose();
  }
  #endregion

  #region Input Callbacks
  private void OnResumeButtonPressed() => Logic.Input(new PauseMenuLogic.Input.OnResumePressed());
  private void OnQuitButtonPressed() => Logic.Input(new PauseMenuLogic.Input.OnQuitPressed());
  #endregion


  #region Output Callbacks
  #endregion
}
