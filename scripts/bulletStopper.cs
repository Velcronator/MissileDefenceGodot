using Godot;
using System;

public class bulletStopper : Area2D
{
    bulletAI bulletAI;
    public void _on_bulletStopper_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "player") && (bullet is bullet))
        {
            bulletAI.spawnExplosion(GlobalPosition ,"player");
            bullet.QueueFree();
            QueueFree();//Kills the instance as well
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bulletAI = (bulletAI)GetNode("/root/game/bullets/bulletAI");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
