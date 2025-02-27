namespace Vardag;

using Chickensoft.GodotNodeInterfaces;
using Chickensoft.LogicBlocks;
using Godot;

public partial class PlayerLogic {
  public partial record State : StateLogic<State>, IGet<Input.PhysicsTick>, IGet<Input.RequestLook>, IGet<Input.RequestMove> {
    public State() {
      OnAttach(() => { });
      OnDetach(() => { });
    }

    public Transition On(in Input.RequestLook input) {
      var data = Get<Data>();

      // TODO have these be separate floats instead?
      data.LookRotation.X -= input.Rotation.Y * 0.0001f;
      data.LookRotation.Y -= input.Rotation.X * 0.0001f;

      Output(new Output.Look(data.LookRotation));
      return ToSelf();
    }
    public Transition On(in Input.RequestMove input) {
      var data = Get<Data>();
      var camera = Get<ICamera3D>();

      var right = camera.GlobalBasis.X * input.Direction.X;
      var forward = camera.GlobalBasis.X.Cross(Vector3.Up) * input.Direction.Y;

      data.DesiredVelocity = (right + forward) * 5f;

      return ToSelf();
    }

    public Transition On(in Input.PhysicsTick input) {
      var data = Get<Data>();

      data.Velocity = data.Velocity.MoveToward(data.DesiredVelocity, input.Delta * 10f);

      Output(new Output.UpdateVelocity(data.Velocity));
      Output(new Output.Move());
      return ToSelf();
    }
  }
}
