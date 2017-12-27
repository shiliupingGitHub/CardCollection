using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace Model
{
    [BsonKnownTypes(typeof(ARequest))]
    [BsonKnownTypes(typeof(AResponse))]
    [BsonKnownTypes(typeof(AActorMessage))]
#if SERVER
    [BsonKnownTypes(typeof(PlayerReconnect))]
#endif
    public abstract class AMessage
    {
    }

    [ProtoContract]
    [BsonKnownTypes(typeof(AActorRequest))]
    public abstract class ARequest : AMessage
    {
        [ProtoMember(1000)]
        [BsonIgnoreIfDefault]
        public uint RpcId;
    }

    /// <summary>
    /// 服务端回的RPC消息需要继承这个抽象类
    /// </summary>
    [ProtoContract]
    [BsonKnownTypes(typeof(AActorResponse))]
    public abstract class AResponse : AMessage
    {
        [ProtoMember(1000)]
        public uint RpcId;

        [ProtoMember(1001)]
        public int Error = 0;

        [ProtoMember(1002)]
        public string Message = "";
    }
}