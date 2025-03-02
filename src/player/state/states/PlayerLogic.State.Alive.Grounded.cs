namespace Vardag;

using Chickensoft.LogicBlocks;
using Godot;

public partial class PlayerLogic {
  public partial record State {
    public partial record Alive {
      public partial record Grounded : Alive, IGet<Input.RequestMove>, IGet<Input.CheckGrounded> {

        public Grounded() {
          this.OnEnter(() => {
            var data = Get<Data>();
            Output(new Output.UpdateVelocity(data.Velocity with { Y = 0 }));
          });
        }

        public Transition On(in Input.RequestMove input) {
          var data = Get<Data>();
          var settings = Get<IPlayerSettings>();
          var camera = Get<IPlayerCamera>();

          var right = camera.GlobalBasis.X * input.Direction.X;
          var forward = camera.GlobalBasis.X.Cross(Vector3.Up) * input.Direction.Y;

          data.DesiredVelocity = (right + forward) * settings.MaxSpeed;

          return ToSelf();
        }

        public Transition On(in Input.CheckGrounded input) => input.IsGrounded ? ToSelf() : To<Falling>();
      }
    }

  }
}
