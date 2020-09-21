using Godot;
using System;

public class BulletStopper : Area2D {
  BulletBrain _bulletBrain;
  Player _player;

  public override void _Ready() {
    _bulletBrain = (BulletBrain)GetNode("/root/Game/Bullets/BulletBrain");
    _player = (Player)GetNode("/root/Game/Player");
  }
  public void _on_BulletStopper_area_entered(Area2D bullet) {
    var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");

    if (bulletType != null && bulletType.Animation == "player" && bullet is Bullet) {
      //_bulletBrain.SpawnExplosion(GlobalPosition, "player");
      _bulletBrain.CallDeferred("SpawnExplosion", GlobalPosition, "player");
      _player.CanShoot = true;
      bullet.QueueFree();
      QueueFree();
    }
  }
}
