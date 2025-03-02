namespace Vardag;

using Chickensoft.LogicBlocks;
using Godot;

public partial class PlayerLogic {
  public abstract partial record State : StateLogic<State>, IGet<Input.PhysicsTick>, IGet<Input.RequestLook> {

    public State() {
      OnAttach(() => { });
      OnDetach(() => { });
    }

    public Transition On(in Input.RequestLook input) {
      var data = Get<Data>();
      var settings = Get<IPlayerSettings>();

      // TODO have these be separate floats instead?
      data.LookRotation.X = Mathf.Clamp(data.LookRotation.X - (input.Rotation.Y * settings.LookSensitivity), settings.MinViewAngle, settings.MaxViewAngle);
      data.LookRotation.Y -= input.Rotation.X * settings.LookSensitivity;

      Output(new Output.Look(data.LookRotation));
      return ToSelf();
    }

    public Transition On(in Input.PhysicsTick input) {
      var data = Get<Data>();
      var settings = Get<IPlayerSettings>();

      data.Velocity = data.Velocity.MoveToward(data.DesiredVelocity, input.Delta * settings.Acceleration);

      Output(new Output.UpdateVelocity(data.Velocity));
      Output(new Output.Move());
      return ToSelf();
    }
  }
}
