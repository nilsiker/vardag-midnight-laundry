namespace Vardag;

using Chickensoft.LogicBlocks;

public partial class PlayerCameraLogic {
  public partial record State : StateLogic<State>, IGet<Input.Focus>, IGet<Input.Unfocus>, IGet<Input.Tilt> {
    public State() {
      OnAttach(() => { });
      OnDetach(() => { });
    }

    public Transition On(in Input.Focus input) {
      var settings = Get<IPlayerCameraSettings>();
      Output(new Output.UpdateFov(settings.ZoomedFov, settings.ZoomSpeed));
      return ToSelf();
    }

    public Transition On(in Input.Unfocus input) {
      var settings = Get<IPlayerCameraSettings>();

      Output(new Output.UpdateFov(settings.Fov, settings.ZoomSpeed));
      return ToSelf();
    }

    public Transition On(in Input.Tilt input) {
      // TODO split tilt components into separate floats?
      var data = Get<Data>();
      var settings = Get<IPlayerCameraSettings>();

      if (!data.Tilt.IsEqualApprox(input.Direction)) {
        Output(new Output.Tilt(input.Direction, settings.TiltIntensity, settings.TiltSpeed));
        data.Tilt = input.Direction;

        // TODO handle in own input?
        Output(new Output.Bob(!input.Direction.IsZeroApprox(), settings.BobIntensity, settings.BobSpeed));
      }

      return ToSelf();
    }
  }
}
