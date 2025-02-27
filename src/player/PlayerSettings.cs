namespace Vardag;

using Godot;

public interface IPlayerSettings {
  public float MinViewAngle { get; }
  public float MaxViewAngle { get; }
  public float MaxSpeed { get; }
  public float Acceleration { get; }
  public float LookSensitivity { get; }


}

[GlobalClass, Tool]
public partial class PlayerSettings : Resource, IPlayerSettings {
  [ExportCategory("Movement")]
  [Export] public required float MaxSpeed { get; set; }
  [Export] public required float Acceleration { get; set; }
  [ExportCategory("Camera")]
  [Export] public required float MinViewAngle { get; set; }
  [Export] public required float MaxViewAngle { get; set; }
  [Export] public required float LookSensitivity { get; set; }
}
