namespace Vardag;

using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

public interface IPlayerLogic : ILogicBlock<PlayerLogic.State>;

[Meta]
[LogicBlock(typeof(State), Diagram = true)]
public partial class PlayerLogic
  : LogicBlock<PlayerLogic.State>,
    IPlayerLogic {
  public override Transition GetInitialState() => To<State.Alive.Grounded.Idle>();
}
