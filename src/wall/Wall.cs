namespace Vardag;

using Godot;

[Tool, SceneTree]
public partial class Wall : Node3D {
  private CsgMesh3D WallMesh => _.WallMesh;

  #region Exports
  [Export]
  private Vector3 Measurements {
    get => ((BoxMesh)WallMesh.Mesh).Size;
    set {
      var mesh = (BoxMesh)WallMesh.Mesh;
      mesh.Size = value;
      WallMesh.GlobalPosition = WallMesh.GlobalPosition with { Y = value.Y / 2 };

      if (Material is not null) {
        Material.Uv1Scale = mesh.Size;
      }
    }
  }
  [Export]
  private bool Windowed { get; set; }
  [Export]
  private StandardMaterial3D Material {
    get => (StandardMaterial3D)WallMesh.Material;
    set {
      WallMesh.Material = value;
      var mesh = (BoxMesh)WallMesh.Mesh;
      if (mesh is not null) {
        value.Uv1Scale = mesh.Size;
      }
    }
  }
  #endregion

  public override void _Ready() {

  }
}
