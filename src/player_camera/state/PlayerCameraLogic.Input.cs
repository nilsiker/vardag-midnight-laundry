namespace Vardag;

using Godot;

public partial class PlayerCameraLogic {
  public static class Input {
    public record struct Focus;
    public record struct Unfocus;
    public record struct Tilt(Vector2 Direction);
  }
}
