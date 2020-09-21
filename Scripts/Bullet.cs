using Godot;
using System;

public class Bullet : Area2D {
  public int speed = 400;
  Vector2 _velocity = new Vector2(0, 0);
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {

  }

  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float _delta) {
    _velocity = Vector2.Right.Rotated(Rotation) * speed * _delta;
    Translate(_velocity);
  }
}
