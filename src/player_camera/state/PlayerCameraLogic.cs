namespace Vardag;

using System;
using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

public interface IPlayerCameraLogic : ILogicBlock<PlayerCameraLogic.State>;

[Meta]
[LogicBlock(typeof(State), Diagram = true)]
public partial class PlayerCameraLogic
  : LogicBlock<PlayerCameraLogic.State>,
    IPlayerCameraLogic {
  public override Transition GetInitialState() => To<State>();
  protected override void HandleError(Exception e) => throw e;
}
