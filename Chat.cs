using Godot;
using System;

public partial class Chat : Control
{
	public override void _Ready()
	{
		Tanks.Net.Instance.Connect(Tanks.Net.SignalName.ChatMessage, new Callable(this, nameof(this.OnChatMessage)));
	}


	private void OnChatMessage(string sender, string message)
	{
		Control chatList = this.GetNode<Control>("%ChatList");
		Label newChatLabel = new()
		{
			Text = $"{sender}: {message}"
		};
		chatList.AddChild(newChatLabel);
	}
}