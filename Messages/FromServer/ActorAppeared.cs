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
		Guid syncId = reader.GetGuid();

		string uidString = ResourceUid.IdToText(godotUid);
		PackedScene playerCharacterScene = ResourceLoader.Load<PackedScene>(uidString);
		this.Actor = playerCharacterScene.Instantiate<SharedActor>();

		if (this.Actor is not IClientActor clientActor)
			throw new Exception("Actor is not a client actor");

		this.Actor.Position = reader.GetVector();
		this.Actor.SyncId = syncId;
		clientActor.Deserialize(reader);
	}


	public void Spawn()
	{
		SyncService.RegisterActor(this.Actor);
		World.Instance.AddToWorld(this.Actor);
	}
}