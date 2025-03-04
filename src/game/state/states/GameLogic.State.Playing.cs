namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class GameLogic {
  public partial record State {
    public partial record Playing : State, IGet<Input.OnPausePressed> {
      public Playing() {
        this.OnEnter(() => Get<IGameRepo>().Play());
      }

      public Transition On(in Input.OnPausePressed input) => To<Paused>();
    }
  }
}
