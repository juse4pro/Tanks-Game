using Godot;
using LiteNetLib;
using LiteNetLib.Utils;
using Server.Messages.ServerToClient;
using Shared;
using Shared.Messages.FromServer;

namespace Server;

public class GameServer
{
	private const int MaxPlayers = 250;
	private NetManager _manager;
	private readonly Dictionary<NetPeer, NetPlayer> _players = [];
	private readonly ActorCollection _actorCollection = new();


	public void Start()
	{
		EventBasedNetListener listener = new();
		listener.ConnectionRequestEvent += this.OnConnectionRequestEvent;
		listener.PeerConnectedEvent += this.OnPeerConnectedEvent;
		listener.PeerDisconnectedEvent += this.OnPeerDisconnectedEvent;
		listener.NetworkReceiveEvent += this.OnNetworkReceiveEvent;
		this._manager = new NetManager(listener);
		this._manager.Start(Settings.GamePort);
		GD.Print($"Server running on port {this._manager.LocalPort}");
	}


	public void Stop()
	{
		foreach (NetPlayer player in this._players.Values) player.Peer.Disconnect();

		this._players.Clear();
	}


	public void Update(float deltaTime)
	{
		this._manager.PollEvents();

		this._actorCollection.Update(deltaTime);
	}


	public void Spawn(SharedActor actor)
	{
		this._actorCollection.Add(actor);

		actor.SyncId = SyncService.GetNewId();
		SyncService.RegisterActor(actor);
		actor.TreeExiting += () => this.OnActorDespawned(actor);

		NetDataWriter writer = new();
		writer.Put((byte)MessageId.ActorAppeared);
		ActorAppeared actorAppearedMessage = new()
		{
			Actor = actor
		};
		actorAppearedMessage.Serialize(writer);
		this.BroadcastMessage(writer);
	}


	private void OnActorDespawned(SharedActor actor)
	{
		SyncService.UnregisterActor(actor);
		NetDataWriter writer = new();
		writer.Put((byte)MessageId.ActorDisappeared);
		ActorDisappeared actorDisappearedMessage = new()
		{
			DisappearedSyncId = actor.SyncId
		};
		actorDisappearedMessage.Serialize(writer);
		this.BroadcastMessage(writer);
	}


	private void OnConnectionRequestEvent(ConnectionRequest request)
	{
		GD.Print($"Connection request from {request.RemoteEndPoint}");
		if (this._players.Count >= MaxPlayers)
			request.Reject();

		request.AcceptIfKey(Settings.ConnectionKey);
	}


	private void OnPeerConnectedEvent(NetPeer connectedPeer)
	{
		NetPlayer newPlayer = new()
		{
			Peer = connectedPeer
		};

		// Sync all existing actors to the connecting player.
		foreach (SharedActor actor in this._actorCollection.GetAllActors())
		{
			NetDataWriter writer = new();
			writer.Put((byte)MessageId.ActorAppeared);
			ActorAppeared actorAppearedMessage = new()
			{
				Actor = actor
			};
			actorAppearedMessage.Serialize(writer);
			connectedPeer.Send(writer, DeliveryMethod.ReliableOrdered);
		}

		this._players.Add(connectedPeer, newPlayer);

		this.BroadcastSystemChatMessage($"Player connected {newPlayer}");

		// Creating a new character for the new player.
		ServerCharacter character = new()
		{
			Position = new Vector2(GD.RandRange(100, 600), GD.RandRange(100, 600))
		};
		newPlayer.PossessCharacter(character);
		this.Spawn(character);
	}


	private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
	{
		NetPlayer player = this._players[peer];
		this._players.Remove(peer);

		player.PossessedCharacter?.QueueFree();

		this.BroadcastSystemChatMessage($"Player disconnected {player} ({disconnectInfo.Reason})");
	}


	private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel,
		DeliveryMethod deliveryMethod)
	{
	}


	public void BroadcastSystemChatMessage(string message)
	{
		GD.Print(message);
		NetDataWriter writer = new();
		writer.Put((byte)MessageId.Chat);
		writer.Put(new ChatMessage
		{
			Sender = "SYSTEM",
			Message = message
		});

		this.BroadcastMessage(writer);
	}


	private void BroadcastMessage(NetDataWriter writer)
	{
		foreach (NetPlayer player in this._players.Values)
			player.Peer.Send(writer, DeliveryMethod.ReliableOrdered);
	}
}