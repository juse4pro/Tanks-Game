using Godot;
using System;

public partial class MainMenu : Control
{
    private void OnHostButtonPressed()
    {
        this.GetTree().ChangeSceneToFile("uid://gv2iag7mntlg");
    }
    

    private void OnJoinButtonPressed()
    {
        this.GetTree().ChangeSceneToFile("uid://w65t5rvodews");
    }
    

    private void OnExitButtonPressed()
    {
        this.GetTree().Quit();
    }
}
