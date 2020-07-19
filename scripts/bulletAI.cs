using Godot;
using System;

public class bulletAI : Node
{

    Timer enemySpawner;
    scenes scenes = new scenes();
    [Export] public float maxSpawnInterval = 4f;
    [Export] public float minSpawnInterval = 0.5f;
    [Export] public float spawnIntervalDecrease = 0.2f;
    [Export] public float spawnInterval = 0f;
    [Export] public int playerBulletsSpeed = 300;
    [Export] public int enemyBulletsSpeed = 250;

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

    public void _on_cloudSpawner_timeout()
    {
        spawnCloud();
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

        if(animationName == "player")
        {
            bullet.speed = playerBulletsSpeed;
        }else if(animationName == "enemy")
        {
            bullet.speed = enemyBulletsSpeed;
        }

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

    
    public void spawnCloud()
    {
        // spawn explosion at position
        var cloud = (AnimatedSprite)scenes._sceneCloud.Instance();
        GetNode("/root/game/foreground").AddChild(cloud);

        cloud.Frame = Convert.ToInt32(Math.Floor(GD.RandRange(0,3)));
        Vector2 spawnPosition = new Vector2(-100, Convert.ToSingle(GD.RandRange(0,400)));
        cloud.GlobalPosition = spawnPosition;
        var randomScale = Convert.ToSingle(GD.RandRange(0,1));
        cloud.Scale = new Vector2(randomScale,randomScale);
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
