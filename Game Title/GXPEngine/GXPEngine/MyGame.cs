using System;									
using System.Drawing;                           
using GXPEngine;

public class MyGame : Game
{
	Timing timer;
	public Player player;
	Level level;

	public float scrollSpeed;

	public MyGame() : base(1520, 790, false, true, 0, 0)	
	{
		timer = new Timing();
		player = new Player();
		level = new Level();

		AddChild(level);
		AddChild(player);
	}

	void Update()
	{
		game.x -= scrollSpeed;
	}

	static void Main()							
	{
		new MyGame().Start();					
	}
}