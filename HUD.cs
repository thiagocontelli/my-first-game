using Godot;
using System;

public class HUD : CanvasLayer
{

	[Signal]
	public delegate void StartGame();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, "timeout");

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the\nCreeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	private void _on_MessageTimer_timeout()
	{
		GetNode<Label>("Message").Hide();
	}


	private void _on_StartButton_pressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}



