namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class PlayerCameraLogic {
  public partial record State : StateLogic<State>, IGet<Input.Focus>, IGet<Input.Unfocus>, IGet<Input.Tilt> {
    public State() {
      OnAttach(() => { });
      OnDetach(() => { });
    }

    public Transition On(in Input.Focus input) {
      Output(new Output.UpdateFov(40));
      return ToSelf();
    }

    public Transition On(in Input.Unfocus input) {
      Output(new Output.UpdateFov(75));
      return ToSelf();
    }

    public Transition On(in Input.Tilt input) {
      // TODO split tilt components into separate floats?
      var data = Get<Data>();
      var newTilt = input.Direction * 5f;

      if (!newTilt.IsEqualApprox(data.Tilt)) {
        Output(new Output.Tilt(newTilt));
        data.Tilt = newTilt;

        // TODO handle in own input?
        Output(new Output.Bob(!newTilt.IsZeroApprox()));
      }

      return ToSelf();
    }
  }
}
