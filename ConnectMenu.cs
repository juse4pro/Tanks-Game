using Godot;
using System;

public partial class ConnectMenu : Control
{
	private void OnConnectButtonPressed()
	{
		LineEdit ipInput = this.GetNode<LineEdit>("%IpInput");
		string serverAddress = ipInput.Text;
		Net.Instance.Join(serverAddress, Shared.Settings.GamePort);
	}
}