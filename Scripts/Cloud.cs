using Godot;
using System;

public class Cloud : AnimatedSprite {
  [Export]
  public float Speed { get; set; } = 200f;
  public Vector2 Velocity { get; private set; } = Vector2.Right;

  public override void _Ready() {
    Speed = Convert.ToInt32(GD.RandRange(200, 300));
  }

  public override void _Process(float delta) {
    var newSpeed = Velocity * Speed * delta;
    Translate(newSpeed);
  }
}
