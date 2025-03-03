namespace Vardag;

public partial class GameLogic {
  public partial record State {
    public partial record Playing : State, IGet<Input.OnPausePressed> {
      public Playing() { }

      public Transition On(in Input.OnPausePressed input) => To<Paused>();
    }
  }
}
