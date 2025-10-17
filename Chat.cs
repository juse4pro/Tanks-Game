using Godot;
using System;

public partial class Chat : Control
{
	public override void _Ready()
	{
		Net.Instance.Connect(Net.SignalName.ChatMessage, new Callable(this, nameof(this.OnChatMessage)));
	}


	private void OnChatMessage(string sender, string message)
	{
		Control chatList = this.GetNode<Control>("%ChatList");
		Label newChatLabel = new Label()
		{
			Text = $"{sender}: {message}"
		};
		chatList.AddChild(newChatLabel);
	}
}