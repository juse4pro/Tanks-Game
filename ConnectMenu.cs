using Godot;
using System;

public partial class ConnectMenu : Control
{
	public override void _Ready()
	{
		Net.Instance.Connect(
			Net.SignalName.ConnectionStatusChanged,
			new Callable(this, nameof(this.OnConnectionStatusChanged))
		);
	}


	private void OnConnectButtonPressed()
	{
		LineEdit ipInput = this.GetNode<LineEdit>("%IpInput");
		ipInput.Editable = false;
		this.GetNode<Button>("%ConnectButton").Disabled = true;

		string serverAddress = ipInput.Text;
		Net.Instance.Join(serverAddress, Shared.Settings.GamePort);
	}


	private void OnConnectionStatusChanged(bool wasSuccessful)
	{
		if (wasSuccessful)
		{
			this.GetTree().ChangeSceneToFile("uid://gv2iag7mntlg");
		}
		else
		{
			this.GetNode<LineEdit>("%IpInput").Editable = true;
			this.GetNode<Button>("%ConnectButton").Disabled = false;
		}
	}
}