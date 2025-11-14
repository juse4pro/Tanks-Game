using LiteNetLib.Utils;
using Shared.Messages.FromClient;

namespace Tanks.Messages.FromClient;

public class ClientIntentionUpdated : ClientIntentionUpdatedMessage, IClientToServerMessage
{
	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.NewIntention);
	}
}