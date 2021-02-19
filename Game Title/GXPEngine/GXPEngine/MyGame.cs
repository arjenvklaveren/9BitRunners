using System;									
using System.Drawing;                           
using GXPEngine;
using System.Threading;

public class MyGame : Game
{
	public bool isPlaying = false;
	public bool gameOver = false;

	Timing timer;
	public Player player;
	public Level level;
	public HUD hud;
	public SceneManager sceneManager;

	public float score;

	public float scrollSpeed;

	public Sound backgroundSound;

	public MyGame() : base(1520, 790, false, false, 0, 0)
	{
		timer = new Timing();
		player = new Player();
		level = new Level();
		hud = new HUD();
		sceneManager = new SceneManager();

		targetFps = 60;

		backgroundSound = new Sound("backGroundMusic.mp3", true, true);
		backgroundSound.Play();

		AddChild(level);
		AddChild(player);
		AddChild(sceneManager);
		AddChild(hud);
	}

	void Update()
	{
		if(player.lives != 0)
        {
			game.x -= scrollSpeed;
			score += scrollSpeed / 10;
		}
        else
        {
			gameOver = true;
        }	

		if(Input.GetKeyDown(Key.ENTER) && isPlaying == false)
        {
			Reset();
			player.lives = 3;
			isPlaying = true;
			level.startPlatformAmount = 12;
		}
	}

	public void Reset()
    {
		level.DeleteEnemies();
		level.DeletePlatforms();
		level.currentLevel = 1;
		level.hasBuildBeginningPlatform = false;
		player.x = 400;
		player.y = 500;
		x = 0;
		score = 0;
		hud.x = 0;
		player.previousX = 0;
		player.playerSpeed = 12;
		player.animationSpeed = 5;
		level.previousPlatformAmount = 0;
		level.distanceBetweenPlatforms = 200;
		level.platformSpriteOffset = 0;
		level.backgroundPosX = 0;
		level.currentPlatformPosX = 0;
		level.currentPlatformPosY = 700;
		level.background.previousBackgroundLeftResetPos = 1920;
		level.background.previousBackgroundRightResetPos = 3840;
		level.background.x = 0;
		level.background.backGroundLeftPosX = 0;
		level.background.backGroundRightPosX = 1920;
		level.background.backgroundLevel1SpriteLeft.alpha = 1;
		level.background.backgroundLevel1SpriteRight.alpha = 1;
		level.background.backgroundLevel2SpriteLeft.alpha = 0;
		level.background.backgroundLevel2SpriteRight.alpha = 0;
		level.background.backgroundLevel3SpriteLeft.alpha = 0;
		level.background.backgroundLevel3SpriteRight.alpha = 0;
		sceneManager.scene1.alpha = 0;
		sceneManager.scene2.alpha = 0;
		sceneManager.scene3.alpha = 0;
	}

	public void GameReset()
    {
		level.maxReachedLevel = 1;
		player.lives = 1;
		level.startPlatformAmount = 999;
		isPlaying = false;
		gameOver = false;
		Reset();
	}

	static void Main()							
	{
		new MyGame().Start();		
	}
}