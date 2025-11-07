using LiteNetLib.Utils;

namespace Shared.Messages;

public interface INetMessage<T> : INetSerializable where T : INetMessage<T>, new();
