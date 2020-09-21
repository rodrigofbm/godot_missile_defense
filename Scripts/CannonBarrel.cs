using Godot;
using System;

public class CannonBarrel : Sprite {
  BulletBrain _bulletBrain;
  Player _player;
  Scenes _scenes = new Scenes();
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    _bulletBrain = (BulletBrain)GetNode("/root/Game/Bullets/BulletBrain");
    _player = (Player)GetNode("/root/Game/Player");
  }

  public override void _Input(InputEvent _event) {
    if (_player.CanShoot && _event.IsActionPressed("LeftClick")) ShootAtMouse();
  }

  public void ShootAtMouse() {
    _bulletBrain.SpawnBullet(GlobalPosition, GetGlobalMousePosition(), "player");
    var bulletStopper = (Area2D)_scenes.sceneBulletStopper.Instance();
    GetNode("/root/Game/Bullets").AddChild(bulletStopper);
    bulletStopper.GlobalPosition = GetGlobalMousePosition();
    _player.CanShoot = false;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta) {
    LookAt(GetGlobalMousePosition());
  }
}
