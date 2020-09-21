using Godot;
using System;

public class ExplosionFx : Area2D {

  BulletBrain _bulletBrain;
  Player _player;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    _bulletBrain = (BulletBrain)GetNode("/root/Game/Bullets/BulletBrain");
    _player = (Player)GetNode("/root/Game/Player");
  }
  public void _on_AnimatedSprite_animation_finished() {
    QueueFree();
  }

  public void _on_Explosion_area_entered(Area2D bullet) {
    var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
    var explosionType = (AnimatedSprite)GetNodeOrNull("AnimatedSprite");

    if (bulletType != null && bulletType.Animation == "enemy" && bullet is Bullet) {
      // _bulletBrain.SpawnExplosion(bullet.GlobalPosition, "enemy");
      _bulletBrain.CallDeferred("SpawnExplosion", bullet.GlobalPosition, "enemy");
      _player.AddScore();
      bullet.QueueFree();
    }
  }
}
