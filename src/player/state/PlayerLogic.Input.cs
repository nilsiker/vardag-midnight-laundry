namespace Vardag;

using Godot;

public partial class PlayerLogic {
  public static class Input {
    public record struct PhysicsTick(float Delta);
    public record struct RequestLook(Vector2 Rotation);
    public record struct RequestMove(Vector2 Direction);
  }
}
