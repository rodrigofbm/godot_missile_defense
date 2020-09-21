using Godot;
using System;

public class Player : Node {
  BulletBrain _bulletBrain;
  [Export]
  public int Health { get; private set; } = 3;
  public int Score { get; private set; } = 0;
  public bool CanShoot { get; set; } = true;
  public bool isGameOver { get; private set; } = false;

  public override void _Ready() {
    _bulletBrain = (BulletBrain)GetNode("/root/Game/Bullets/BulletBrain");
    UpdateUi();
  }

  public override void _Input(InputEvent @event) {
    if (isGameOver && @event.IsActionPressed("LeftClick")) GetTree().ReloadCurrentScene();
  }

  public void _on_PlayerHitZone_area_entered(Area2D bullet) {
    var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");

    if (bulletType != null && bulletType.Animation == "enemy" && bullet is Bullet) {
      //_bulletBrain.SpawnExplosion(bullet.GlobalPosition, "enemy");
      _bulletBrain.CallDeferred("SpawnExplosion", bullet.GlobalPosition, "enemy");
      HitPlayer();
      bullet.QueueFree();
    }
  }

  public void HitPlayer(int damage = 1) {
    Health = Math.Max(Health - damage, 0);
    UpdateUi();

    if (Health <= 0 && !isGameOver) {
      var gameOver = (Node2D)GetNode("/root/Game/HUD/GameOver");
      var cannon = (Node2D)GetNode("/root/Game/Foreground/cannon");

      isGameOver = true;
      CanShoot = false;

      gameOver.Visible = true;
      //_bulletBrain.SpawnExplosion(cannon.GlobalPosition, "player");
      _bulletBrain.CallDeferred("SpawnExplosion", cannon.GlobalPosition, "player");
      cannon.QueueFree();
    }
  }

  public void AddScore(int score = 5) {
    Score += score;
    UpdateUi();
    _bulletBrain.IncreaseDificulty();
  }

  public void UpdateUi() {
    var healthScore = (Label)GetNode("/root/Game/HUD/Health_Score");
    var newHudText = "HEALTH: " + Health + "     " + "SCORE: " + Score;

    healthScore.Text = newHudText;
  }
}
