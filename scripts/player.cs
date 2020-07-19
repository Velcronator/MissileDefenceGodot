using Godot;
using System;

public class player : Node
{
    bulletAI bulletAI;
    
    public void _on_playerHitZone_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            bulletAI.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
        }    
    }

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
