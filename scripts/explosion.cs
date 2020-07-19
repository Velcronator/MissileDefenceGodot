using Godot;
using System;

public class explosion : Area2D
{
    player player;
    bulletAI bulletAI;

    public override void _Ready()
    {
        bulletAI = (bulletAI)GetNode("/root/game/bullets/bulletAI");   
        player = (player)GetNode("/root/game/player");
    }

    public void _on_explosion_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        var explosionType = (AnimatedSprite)GetNode("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            // bulletAI.spawnExplosion(bullet.GlobalPosition, "enemy");
            bulletAI.CallDeferred("spawnExplosion", bullet.GlobalPosition ,"enemy");
            bullet.QueueFree();
            player.addScore(1);
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
