using Godot;
using System;

public partial class MainMenu : Control
{
	private void OnJoinButtonPressed()
	{
		this.GetTree().ChangeSceneToFile("uid://w65t5rvodews");
	}


	private void OnExitButtonPressed()
	{
		this.GetTree().Quit();
	}
}