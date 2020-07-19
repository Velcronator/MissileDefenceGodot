using Godot;
using System;

public class player : Node
{
    bulletAI bulletAI;
    public bool canShoot = true;
    public int health = 3;
    public int score = 0;
    
    public void _on_playerHitZone_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            bulletAI.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
            hitplayer();
        }    
    }

    public override void _Ready()
    {
        bulletAI = (bulletAI)GetNode("/root/game/bullets/bulletAI");
        updateUI();
    }

    public void hitplayer(int damageAmount = 1)
    {
        health = Math.Max(health-damageAmount,0);
        updateUI();
    }

    public void addScore(int scoreAmount = 1)
    {
        score += scoreAmount;
        updateUI();
    }


    public void updateUI()
    {
        var healthAndScore = (Label)GetNode("/root/game/hud/healthAndScore");
        var newHUDText = "Health: " + health + "     Score: " + score;
        healthAndScore.Text = newHUDText;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
