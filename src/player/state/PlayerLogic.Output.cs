namespace Vardag;

using Godot;

public partial class PlayerLogic {
  public static class Output {
    public record struct Move;
    public record struct Look(Vector3 Rotation);
    public record struct UpdateVelocity(Vector3 Velocity);
  }
}
