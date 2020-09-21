using Godot;
using System;

public class BulletBrain : Scenes {
  Timer enemySpawner;
  [Export]
  public float maxSpawnInterval { get; private set; } = 3f;
  [Export]
  public float minSpawnInterval { get; private set; } = 0.5f;
  public float spawnIntervalDecrease { get; private set; } = 0.15f;

  public float SpawnInterval { get; private set; }
  [Export]
  public int PlayerBulletSpeed { get; set; } = 300;
  [Export]
  public int EnemyBulletSpeed { get; set; } = 250;

  public override void _Ready() {
    SpawnInterval = maxSpawnInterval;
    enemySpawner = (Timer)GetNode("EnemySpawn");
  }

  public void IncreaseDificulty() {
    var newSpawnInterval = SpawnInterval - spawnIntervalDecrease;
    SpawnInterval = Math.Max(newSpawnInterval, minSpawnInterval);
    enemySpawner.WaitTime = SpawnInterval;
    enemySpawner.Start();
  }

  public void SpawnBullet(Vector2 spawnPosition, Vector2 targetPosition, string animationName) {
    var bullet = (Bullet)sceneBullet.Instance();
    GetNode("/root/Game/Bullets").AddChild(bullet);
    bullet.GlobalPosition = spawnPosition;
    bullet.LookAt(targetPosition);

    // set the bullet animation
    var bulletSprite = (AnimatedSprite)bullet.GetNode("AnimatedSprite");
    bulletSprite.Play(animationName);

    if (animationName == "player") {
      bullet.speed = PlayerBulletSpeed;
    } else {
      bullet.speed = EnemyBulletSpeed;
    }
  }

  public void SpawnExplosion(Vector2 spawnPosition, string animationName) {
    var explosion = (Area2D)sceneExplosion.Instance();
    GetNode("/root/Game/Bullets").AddChild(explosion);
    explosion.GlobalPosition = spawnPosition;

    // set the explosion animation
    var explosionSprite = (AnimatedSprite)explosion.GetNode("AnimatedSprite");
    explosionSprite.Play(animationName);
  }

  public void _on_EnemySpawn_timeout() {
    SpawnEnemy();
  }

  public void _on_CloudSpawner_timeout() {
    SpawnCloud();
  }

  void SpawnEnemy() {
    Vector2 spawnPosition = new Vector2(Convert.ToSingle(GD.RandRange(0f, 1000f)), -30f);
    Vector2 targetPosition = new Vector2(Convert.ToSingle(GD.RandRange(0f, 1000f)), 440f);

    SpawnBullet(spawnPosition, targetPosition, "enemy");
  }

  void SpawnCloud() {
    Vector2 spawnPosition = new Vector2(-5f, Convert.ToSingle(GD.RandRange(0f, 360f)));
    var cloud = (AnimatedSprite)sceneCloud.Instance();
    var randCloud = GD.RandRange(0, cloud.Frames.GetFrameCount("default"));
    var randScale = Convert.ToSingle(GD.RandRange(0.5f, 1));

    cloud.Scale = new Vector2(randScale, randScale);
    GetNode("/root/Game/Foreground").AddChild(cloud);
    cloud.GlobalPosition = spawnPosition;

    cloud.Frame = Convert.ToInt32(Math.Floor(randCloud));
    cloud.Play("default");
  }
}

