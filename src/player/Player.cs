namespace Vardag;

using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;

public interface IPlayer : ICharacterBody3D { }

[InputMap]
[Meta(typeof(IAutoNode))]
public partial class Player : CharacterBody3D, IPlayer {
  #region Exports
  [Export] private PlayerSettings Settings { get; set; } = default!;
  #endregion

  #region Nodes
  [Node] private ICamera3D Camera { get; set; } = default!;
  #endregion

  #region Provisions
  #endregion

  #region Dependencies
  #endregion

  #region State
  private PlayerLogic Logic { get; set; } = default!;
  private PlayerLogic.IBinding Binding { get; set; } = default!;
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
    Logic.Set(Camera);

    Logic.Start();
  }

  #endregion

  #region Input Callbacks
  public void Look(Vector2 delta) => Logic.Input(new PlayerLogic.Input.RequestLook(delta));
  public void Move(Vector2 direction) => Logic.Input(new PlayerLogic.Input.RequestMove(direction));
  #endregion

  #region Output Callbacks
  private void OnOutputUpdateVelocity(Vector3 velocity) => Velocity = velocity;

  private void OnOutputMove() => MoveAndSlide();
  private void OnOutputLook(Vector3 rotation) {
    var rot = GlobalRotationDegrees;
    var camRot = Camera.RotationDegrees;

    rot.Y = rotation.Y;
    camRot.X = rotation.X;

    Camera.RotationDegrees = camRot;
    GlobalRotationDegrees = rot;
  }
  #endregion

  #region Godot Lifecycle
  public override void _Notification(int what) => this.Notify(what);

  public void OnReady() {
    SetProcess(true);
    SetPhysicsProcess(true);

    Input.MouseMode = Input.MouseModeEnum.Captured;
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
  }

  public void OnExitTree() {
    Logic.Stop();
    Binding.Dispose();
  }
  #endregion

}
