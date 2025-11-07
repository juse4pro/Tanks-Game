using LiteNetLib.Utils;

namespace Shared.Messages;

public static class WriterExtensions
{
    public static void PutMessage<T>(this NetDataWriter writer, T message) where T : INetMessage<T>, new()
    {
        message.Serialize(writer);
    }
}
