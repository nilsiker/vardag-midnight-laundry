namespace Vardag;
using Chickensoft.LogicBlocks;

public partial class GameLogic {
  public partial record State : StateLogic<State> {
    public State() {
      OnAttach(() => { });
      OnDetach(() => { });
    }
  }
}
