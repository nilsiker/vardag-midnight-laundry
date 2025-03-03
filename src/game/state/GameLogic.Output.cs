namespace Vardag;

public partial class GameLogic {
  public static class Output {
    public record struct SetPauseMode(bool Paused);
    public record struct CaptureMouse(bool Captured);
    public record struct QuitGame;
  }
}
