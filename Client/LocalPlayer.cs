using Godot;
using System;
using LiteNetLib;
using LiteNetLib.Utils;
using Shared;
using Tanks;
using Tanks.Messages.FromClient;

public partial class LocalPlayer : Node
{
	public static LocalPlayer Instance;

	private Intention _localIntention;


	public override void _EnterTree()
	{
		Instance = this;
	}


	public override void _PhysicsProcess(double delta)
	{
		Intention newIntention = new();

		if (Input.IsActionPressed("move_forward"))
			newIntention.Move = 1;
		if (Input.IsActionPressed("move_backward"))
			newIntention.Move = -1;

		if (Input.IsActionPressed("turn_cw"))
			newIntention.Turn = 1;
		if (Input.IsActionPressed("turn_ccw"))
			newIntention.Turn = -1;

		newIntention.Shoot = Input.IsActionPressed("shoot");

		if (newIntention == this._localIntention)
			return;

		this._localIntention = newIntention;
		this.SendLocalIntention();
	}


	private void SendLocalIntention()
	{
		NetDataWriter writer = new();
		writer.Put((byte)MessageId.IntentionUpdated);
		ClientIntentionUpdated message = new()
		{
			NewIntention = this._localIntention
		};
		message.Serialize(writer);
		Net.Instance.GetServerPeer().Send(writer, DeliveryMethod.ReliableSequenced);
	}
}