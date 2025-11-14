using LiteNetLib.Utils;
using Shared.Messages.FromClient;

namespace Server.Messages.ClientToServer;

public class ClientIntentionUpdated : ClientIntentionUpdatedMessage, IClientToServerMessage
{
	public void Deserialize(NetDataReader reader)
	{
		this.NewIntention.Deserialize(reader);
	}
}