using Godot;
using System;

public class bulletAI : Node
{

    Timer enemySpawner;
    scenes scenes = new scenes();
    public float maxSpawnInterval = 4f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecrease = 0.2f;
    public float spawnInterval = 0f;

    public void increaseDifficullty()
    {
        var newSpawnInterval = spawnInterval - spawnIntervalDecrease;
        newSpawnInterval = Math.Max(newSpawnInterval,minSpawnInterval);
        enemySpawner.WaitTime = newSpawnInterval;
        enemySpawner.Start();
        GD.Print(newSpawnInterval);
    }


    public void _on_enemySpawner_timeout()
    {
        spawnEnemy();
    }

    public void spawnEnemy()
    {

        Vector2 spawnPosition = new Vector2(Convert.ToSingle(GD.RandRange(0,1000)),-30);
        Vector2 targetPosition = new Vector2(Convert.ToSingle(GD.RandRange(0,1000)),550);
        spawnBullet(spawnPosition,targetPosition,"enemy");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemySpawner = (Timer)GetNode("enemySpawner");
        spawnInterval = maxSpawnInterval;
    }

    public void spawnBullet(Vector2 spawnPosition, Vector2 targetPosition, string animationName)
    {
        // spawn bullet at position and look at mouse target position
        var bullet = (bullet)scenes._sceneBullet.Instance();
        GetNode("/root/game/bullets").AddChild(bullet);
        bullet.GlobalPosition = spawnPosition;
        bullet.LookAt(targetPosition);

        // set the bullet animation
        var bulletSprite = (AnimatedSprite)bullet.GetNode("AnimatedSprite");
        bulletSprite.Play(animationName);
    }

    public void spawnExplosion(Vector2 spawnPosition, string animationName)
    {
        // spawn explosion at position
        var explosion = (Area2D)scenes._sceneExplosion.Instance();
        GetNode("/root/game/bullets").AddChild(explosion);
        explosion.GlobalPosition = spawnPosition;

        var explosionSprite = (AnimatedSprite)explosion.GetNode("AnimatedSprite");
        explosionSprite.Play(animationName);

    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
