namespace Vardag;

public partial class PauseMenuLogic {
  public static class Input {
    public record struct OnResumePressed;
    public record struct OnOptionsPressed;
    public record struct OnQuitPressed;
  }
}
