namespace Vardag;

using System.Diagnostics.CodeAnalysis;
using Godot;

public static class NodeExtensions {
  public static void ResetTween(this Node node, [NotNull] Tween? tween) {
    if (tween is not null && tween.IsRunning()) {
      tween.Kill();
    }

    tween = node.CreateTween();
  }
}
