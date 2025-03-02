namespace Vardag;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IPlayer : ICharacterBody3D, IStateInfo { }

[InputMap]
[Meta(typeof(IAutoNode))]
public partial class Player : CharacterBody3D, IPlayer {
  #region Exports
  [Export] private PlayerSettings Settings { get; set; } = default!;
  #endregion

  #region Nodes
  [Node] private IPlayerCamera PlayerCamera { get; set; } = default!;
  #endregion

  #region Provisions
  #endregion

  #region Dependencies
  #endregion

  #region State
  private PlayerLogic Logic { get; set; } = default!;
  private PlayerLogic.IBinding Binding { get; set; } = default!;

  string IStateInfo.Name => Name;
  public string State => Logic.Value.ToString();
  #endregion


  #region Dependency Lifecycle
  public void Setup() => Logic = new();

  public void OnResolved() {
    Binding = Logic.Bind();

    // Bind functions to state outputs here
    Binding
      .Handle((in PlayerLogic.Output.Look output) => OnOutputLook(output.Rotation))
      .Handle((in PlayerLogic.Output.Move output) => OnOutputMove())
      .Handle((in PlayerLogic.Output.UpdateVelocity output) => OnOutputUpdateVelocity(output.Velocity));

    Logic.Set(new PlayerLogic.Data());
    Logic.Set(Settings as IPlayerSettings);
    Logic.Set(PlayerCamera);

    Logic.Start();
  }

  #endregion

  #region Input Callbacks
  public void Look(Vector2 delta) => Logic.Input(new PlayerLogic.Input.RequestLook(delta));
  public void Move(Vector2 direction) => Logic.Input(new PlayerLogic.Input.RequestMove(direction));
  #endregion

  #region Output Callbacks
  private void OnOutputUpdateVelocity(Vector3 velocity) => Velocity = velocity;


  private Tween? _tween;
  private void OnOutputMove() {
    // // TODO move this to camera logic
    // var camPivot = GetNode<Node3D>("CamPivot");
    // if (_tween is not null && _tween.IsRunning()) {
    //   _tween.Kill();
    // }
    // _tween = CreateTween();
    // _tween.TweenProperty(camPivot, "rotation_degrees:z", -Input.GetAxis(Left, Right) * 2.5, .25);
    PlayerCamera.Tilt(Input.GetVector(Left, Right, Forward, Backward));

    MoveAndSlide();
  }


  private void OnOutputLook(Vector3 rotation) {
    var rot = GlobalRotationDegrees;
    rot.Y = rotation.Y;
    GlobalRotationDegrees = rot;

    PlayerCamera.SetPitch(rotation.X);
  }
  #endregion

  #region Godot Lifecycle
  public override void _Notification(int what) => this.Notify(what);

  public void OnReady() {
    SetProcess(true);
    SetPhysicsProcess(true);

    Input.MouseMode = Input.MouseModeEnum.Captured;
    AddToGroup("state");
  }

  public void OnProcess(double delta) { }

  public void OnPhysicsProcess(double delta) {
    Logic.Input(new PlayerLogic.Input.RequestMove(Input.GetVector(Left, Right, Forward, Backward)));
    Logic.Input(new PlayerLogic.Input.PhysicsTick((float)delta));
  }

  public override void _UnhandledInput(InputEvent @event) {
    if (@event is InputEventMouseMotion motion) {
      Look(motion.ScreenRelative);
    }
    else if (@event.IsAction(Focus)) {
      if (@event.IsPressed()) {
        PlayerCamera.Focus();
      }
      else {
        PlayerCamera.Unfocus();
      }
    }
  }

  public void OnExitTree() {
    Logic.Stop();
    Binding.Dispose();
  }
  #endregion

}
