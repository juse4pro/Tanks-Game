using System;
using Godot;
using LiteNetLib.Utils;
using Shared;
using Shared.Messages.FromServer;

namespace Tanks.Messages.FromServer;

public class ActorAppeared : ActorAppearedMessage, IServerToClientMessage
{
	public void Deserialize(NetDataReader reader)
	{
		long godotUid = reader.GetLong();

		PackedScene playerCharacterScene = ResourceLoader.Load<PackedScene>("uid://dlltarjrl4x3j");
		this.Actor = playerCharacterScene.Instantiate<SharedActor>();

		if (this.Actor is not IClientActor clientActor)
			throw new Exception("Actor is not a client actor");

		this.Actor.Position = reader.GetVector();
		clientActor.Deserialize(reader);
	}
}