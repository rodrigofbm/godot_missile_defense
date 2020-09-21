using Godot;
using System;

public class Scenes : Node {
  public PackedScene sceneBullet = (PackedScene)GD.Load("res://Scenes/Bullet.tscn");
  public PackedScene sceneExplosion = (PackedScene)GD.Load("res://Scenes/Explosion.tscn");
  public PackedScene sceneBulletStopper = (PackedScene)GD.Load("res://Scenes/BulletStopper.tscn");
  public PackedScene sceneCloud = (PackedScene)GD.Load("res://Scenes/Cloud.tscn");

}
