using LiteNetLib;

namespace Shared.Messages;

public static class ReaderExtensions
{
    public static T GetMessage<T>(this NetPacketReader reader) where T : INetMessage<T>, new()
    {
        T message = new();
        message.Deserialize(reader);
        return message;
    }
}
