namespace Vardag;

using System;
using Chickensoft.Collections;

public interface IGameRepo {
  public IAutoProp<bool> Paused { get; }
  public void Pause();
  public void Resume();
}

public class GameRepo : IGameRepo, IDisposable {
  private readonly AutoProp<bool> _paused = new(false);
  public IAutoProp<bool> Paused => _paused;
  public void Pause() => _paused.OnNext(true);
  public void Resume() => _paused.OnNext(false);

  public void Dispose() {
    _paused.OnCompleted();
    _paused.Dispose();

    GC.SuppressFinalize(this);
  }
}
