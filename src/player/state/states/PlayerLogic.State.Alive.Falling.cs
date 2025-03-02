namespace Vardag;

public partial class PlayerLogic {
  public abstract partial record State {
    public partial record Alive {
      public partial record Falling : Alive, IGet<Input.CheckGrounded>, IGet<Input.PhysicsTick> {
        public Falling() {

        }

        public new Transition On(in Input.PhysicsTick input) {

          var data = Get<Data>();
          data.Velocity.Y += 10 * input.Delta;
          Output(new Output.UpdateVelocity(data.Velocity));
          return ToSelf();
        }

        public Transition On(in Input.CheckGrounded input) => input.IsGrounded ? To<Grounded>() : ToSelf();
      }
    }
  }
}
