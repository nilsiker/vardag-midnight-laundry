namespace Vardag;

using System;
using System.Collections.Generic;
using System.Linq;
using Godot;


public interface IStateInfo {
  public string Name { get; }
  public string State { get; }
}

[SceneTree]
public partial class StateDebugDisplay : HBoxContainer {
  // Called when the node enters the scene tree for the first time.
  private int _index;

  private IEnumerable<IStateInfo> NodesInStateGroup => GetTree().GetNodesInGroup("state").OfType<IStateInfo>();

  public override void _Ready() {

  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) {
    if (!NodesInStateGroup.Any()) {
      _.EntityName.Text = "";
      _.EntityState.Text = "";
      return;
    }

    _.EntityName.Text = NodesInStateGroup.First().Name;
    _.EntityState.Text = NodesInStateGroup.First().State;
  }


  public override void _Input(InputEvent @event) {
    if (@event is InputEventKey key) {
      if (key.Keycode == Key.Pageup) {
        _index++;
        _index = Math.Min(0, NodesInStateGroup.Count());
      }
      else if (key.Keycode == Key.Pagedown) {
        _index = Math.Max(0, _index - 1);
      }
    }
  }
}
