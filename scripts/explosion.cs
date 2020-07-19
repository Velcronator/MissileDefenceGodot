using Godot;
using System;

public class explosion : Area2D
{
    bulletAI bulletAI;

    public override void _Ready()
    {
        bulletAI = (bulletAI)GetNode("/root/game/bullets/bulletAI");    
    }

    public void _on_explosion_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        var explosionType = (AnimatedSprite)GetNode("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            bulletAI.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
            // QueueFree();//Kills the instance as well
        }    
    }

    public void _on_AnimatedSprite_animation_finished()
    {
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
