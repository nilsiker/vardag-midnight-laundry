namespace Vardag;

using Godot;

public interface IPlayerCameraSettings {
  public float BobIntensity { get; }
  public float BobSpeed { get; }
  public float TiltIntensity { get; }
  public float TiltSpeed { get; }
  public float Fov { get; }
  public float ZoomedFov { get; }
  public float ZoomSpeed { get; }
}

[GlobalClass, Tool]
public partial class PlayerCameraSettings : Resource, IPlayerCameraSettings {
  [Export] public float BobIntensity { get; set; }
  [Export] public float BobSpeed { get; set; }
  [Export] public float TiltIntensity { get; set; }
  [Export] public float TiltSpeed { get; set; }
  [Export] public float Fov { get; set; }
  [Export] public float ZoomedFov { get; set; }
  [Export] public float ZoomSpeed { get; set; }
}
