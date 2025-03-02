namespace Vardag;

using Godot;

public partial class PlayerCameraLogic {
  public static class Output {
    public record struct UpdateFov(float Fov, float Speed);
    public record struct Tilt(Vector2 Direction, float Intensity, float Speed);
    public record struct Bob(bool Bobbing, float Intensity, float Speed);
  }
}
