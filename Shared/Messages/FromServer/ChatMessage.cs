using LiteNetLib.Utils;

namespace Shared.Messages.FromServer;

public class ChatMessage : INetMessage<ChatMessage>
{
	public string Sender { set; get; }
	public string Message { set; get; }


	public void Serialize(NetDataWriter writer)
	{
		writer.Put(this.Sender);
		writer.Put(this.Message);
	}


	public void Deserialize(NetDataReader reader)
	{
		this.Sender = reader.GetString();
		this.Message = reader.GetString();
	}
}