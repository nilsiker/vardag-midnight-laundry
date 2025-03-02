namespace Vardag;

using Godot;

public partial class PlayerCameraLogic {
  public static class Output {
    public record struct UpdateFov(float Fov);
    public record struct Tilt(Vector2 Direction);
  }
}
