namespace Vardag;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IPlayerCamera : ICamera3D {
  public void Focus();
  public void Unfocus();
  public void Tilt(Vector2 direction);
}

[Meta(typeof(IAutoNode))]
public partial class PlayerCamera : Camera3D, IPlayerCamera {
  private Tween? _focusTween;
  private Tween? _tiltTween;

  #region Exports
  #endregion

  #region Nodes
  #endregion

  #region Provisions
  #endregion

  #region Dependencies
  #endregion

  #region State
  private PlayerCameraLogic Logic { get; set; } = default!;
  private PlayerCameraLogic.IBinding Binding { get; set; } = default!;
  #endregion

  #region Dependency Lifecycle
  public void Setup() => Logic = new();

  public void OnResolved() {
    Binding = Logic.Bind();

    // Bind functions to state outputs here
    Binding
      .Handle((in PlayerCameraLogic.Output.UpdateFov output) => OnOutputUpdateFov(output.Fov))
      .Handle((in PlayerCameraLogic.Output.Tilt output) => OnOutputTilt(output.Direction));

    Logic.Start();
  }

  #endregion


  #region Godot Lifecycle
  public override void _Notification(int what) => this.Notify(what);

  public void OnReady() {
    SetProcess(true);
    SetPhysicsProcess(true);
  }

  public void OnProcess(double delta) { }

  public void OnPhysicsProcess(double delta) { }

  public void OnExitTree() {
    Logic.Stop();
    Binding.Dispose();
  }
  #endregion

  #region Input Callbacks
  public void Focus() => Logic.Input(new PlayerCameraLogic.Input.Focus());
  public void Unfocus() => Logic.Input(new PlayerCameraLogic.Input.Unfocus());
  public void Tilt(Vector2 direction) => Logic.Input(new PlayerCameraLogic.Input.Tilt(direction));
  #endregion

  #region Output Callbacks
  public void OnOutputUpdateFov(float fov) {
    this.ResetTween(ref _focusTween);

    _focusTween.TweenProperty(this, "fov", fov, 0.4f)
      .SetEase(Tween.EaseType.InOut)
      .SetTrans(Tween.TransitionType.Quad);
  }

  private void OnOutputTilt(Vector2 direction) {
    this.ResetTween(ref _tiltTween);

    _tiltTween.TweenProperty(this, "fov", direction, 0.4f)
      .SetEase(Tween.EaseType.InOut)
      .SetTrans(Tween.TransitionType.Quad);
  }
  #endregion
}
