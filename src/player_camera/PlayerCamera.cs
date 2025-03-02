namespace Vardag;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IPlayerCamera : INode3D {
  public void Focus();
  public void Unfocus();
  public void SetPitch(float pitch);
  public void Tilt(Vector2 direction);
}

[Tool, SceneTree]
[Meta(typeof(IAutoNode))]
public partial class PlayerCamera : Node3D, IPlayerCamera {
  private Tween? _focusTween;
  private Tween? _tiltTween;
  private Tween? _bobTween;

  #region Exports
  [Export] private PlayerCameraSettings Settings { get; set; } = default!;
  [Export]
  private float Height {
    get => Camera?.Position.Y ?? 0;
    set {
      if (Camera is not null) {
        Camera.Position = Camera.Position with { Y = value };
      }
    }
  }
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
      .Handle((in PlayerCameraLogic.Output.UpdateFov output) => OnOutputUpdateFov(output.Fov, output.Speed))
      .Handle((in PlayerCameraLogic.Output.Tilt output) => OnOutputTilt(output.Direction, output.Intensity, output.Speed))
      .Handle((in PlayerCameraLogic.Output.Bob output) => OnOutputBob(output.Bobbing, output.Intensity, output.Speed));


    Logic.Set(new PlayerCameraLogic.Data());
    Logic.Set(Settings as IPlayerCameraSettings);
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
    Logic?.Stop();
    Binding?.Dispose();
  }
  #endregion

  #region Input Callbacks
  public void Focus() => Logic.Input(new PlayerCameraLogic.Input.Focus());
  public void Unfocus() => Logic.Input(new PlayerCameraLogic.Input.Unfocus());
  public void Tilt(Vector2 direction) => Logic.Input(new PlayerCameraLogic.Input.Tilt(direction));
  public void SetPitch(float pitch) {
    var camRot = Camera.RotationDegrees;
    camRot.X = pitch;
    Camera.RotationDegrees = camRot;
  }

  #endregion


  #region Output Callbacks
  public void OnOutputUpdateFov(float fov, float speed) {
    this.ResetTween(ref _focusTween);

    _focusTween.TweenProperty(Camera, "fov", fov, speed)
      .SetEase(Tween.EaseType.InOut)
      .SetTrans(Tween.TransitionType.Quad);
  }

  public void OnOutputBob(bool bobbing, float intensity, float speed) {
    // TODO replace with anim further down the line?
    this.ResetTween(ref _bobTween);
    if (bobbing) {
      _bobTween
        .TweenProperty(TiltNode, "position:y", intensity, speed)
        .SetEase(Tween.EaseType.InOut);
      _bobTween
        .TweenProperty(TiltNode, "position:y", -intensity, speed)
        .SetEase(Tween.EaseType.InOut);
      _bobTween.SetLoops();
    }
    else {
      _bobTween.TweenProperty(TiltNode, "position:y", 0, speed);
    }
  }

  private void OnOutputTilt(Vector2 direction, float intensity, float speed) {
    this.ResetTween(ref _tiltTween);

    _tiltTween
      .TweenProperty(TiltNode, "rotation_degrees:z", -direction.X * intensity, speed)
      .SetEase(Tween.EaseType.InOut);

    _tiltTween
      .Parallel()
      .TweenProperty(TiltNode, "rotation_degrees:x", direction.Y * intensity, speed)
      .SetTrans(Tween.TransitionType.Quad)
      .SetEase(Tween.EaseType.InOut);
  }
  #endregion
}
