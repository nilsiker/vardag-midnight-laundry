namespace Vardag;

using System;
using Chickensoft.Collections;
using Godot;


public interface IGameRepo {
  public event Action? QuitRequested;
  public IAutoProp<bool> Paused { get; }

  public void RequestQuit();
  public void Pause();
  public void Resume();
}

public class GameRepo : IGameRepo, IDisposable {
  public event Action? QuitRequested;
  private readonly AutoProp<bool> _paused = new(false);
  public IAutoProp<bool> Paused => _paused;

  public void RequestQuit() => QuitRequested?.Invoke();
  public void Pause() => _paused.OnNext(true);
  public void Resume() => _paused.OnNext(false);

  public void Dispose() {
    QuitRequested = null;

    _paused.OnCompleted();
    _paused.Dispose();

    GC.SuppressFinalize(this);
  }
}
