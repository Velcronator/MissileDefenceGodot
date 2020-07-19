using Godot;
using System;

public class player : Node
{
    bulletAI bulletAI;
    public bool canShoot = true;
    public bool gameOver = false;
    public int health = 3;
    public int score = 0;
    
    public void _on_playerHitZone_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            // bulletAI.spawnExplosion(bullet.GlobalPosition, "enemy");
            bulletAI.CallDeferred("spawnExplosion", bullet.GlobalPosition ,"enemy");
            bullet.QueueFree();
            hitplayer();
        }    
    }

    public override void _Input(InputEvent _inputEvent)
    {
        if((_inputEvent.IsActionPressed("click")) && (gameOver == true))
        {
            GetTree().ReloadCurrentScene();
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
        if((health <= 0) && gameOver == false)
        {
            gameOver = true;
            canShoot = false;

            var gameOverScreen = (Node2D)GetNode("/root/game/hud/gameOverScreen");
            gameOverScreen.Visible = true;

            var cannon = (Node2D)GetNode("/root/game/foreground/cannon");
            // bulletAI.spawnExplosion(cannon.GlobalPosition,"enemy");
            bulletAI.CallDeferred("spawnExplosion", cannon.GlobalPosition ,"enemy");
            cannon.QueueFree();
        }

    }

    public void addScore(int scoreAmount = 1)
    {
        score += scoreAmount;
        updateUI();
        bulletAI.increaseDifficullty();
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
