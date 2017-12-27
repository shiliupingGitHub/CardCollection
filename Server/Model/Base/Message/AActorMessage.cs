using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace Model
{
    [BsonKnownTypes(typeof(AFrameMessage))]
    [BsonKnownTypes(typeof(FrameMessage))]
    [BsonKnownTypes(typeof(PlayerQuit))]
    [BsonKnownTypes(typeof(PlayerReady))]
    [BsonKnownTypes(typeof(RoomKey))]
    [BsonKnownTypes(typeof(GamerEnter))]
    [BsonKnownTypes(typeof(GamerOut))]
    [BsonKnownTypes(typeof(GameStart))]
    [BsonKnownTypes(typeof(PlayerQuit))]
    [BsonKnownTypes(typeof(GamerMoneyLess))]
    [BsonKnownTypes(typeof(GrabLordSelect))]
    [BsonKnownTypes(typeof(SelectAuthority))]
    [BsonKnownTypes(typeof(SelectLord))]
    [BsonKnownTypes(typeof(AuthorityPlayCard))]
    [BsonKnownTypes(typeof(GamerPlayCards))]
    [BsonKnownTypes(typeof(Discard))]
    [BsonKnownTypes(typeof(GameMultiples))]
    [BsonKnownTypes(typeof(Gameover))]
    [BsonKnownTypes(typeof(ChangeGameMode))]
    [BsonKnownTypes(typeof(GamerReconnect))]
    [BsonKnownTypes(typeof(GamerReenter))]
    [BsonKnownTypes(typeof(GamerReconnect))]
    public abstract class AActorMessage : AMessage
    {
    }

    [BsonKnownTypes(typeof(GetJoinRoomKey_RT))]
    [BsonKnownTypes(typeof(PlayerJoinRoom_RT))]
    [BsonKnownTypes(typeof(PlayCards_RT))]
    [BsonKnownTypes(typeof(Prompt_RT))]
    public abstract class AActorRequest : ARequest
    {
    }

    [BsonKnownTypes(typeof(GetJoinRoomKey_RE))]
    [BsonKnownTypes(typeof(PlayerJoinRoom_RE))]
    [BsonKnownTypes(typeof(PlayCards_RE))]
    [BsonKnownTypes(typeof(Prompt_RE))]
    public abstract class AActorResponse : AResponse
    {
    }

    /// <summary>
    /// 帧消息，继承这个类的消息会经过服务端转发
    /// </summary>
    [ProtoContract]
    public abstract class AFrameMessage : AActorMessage
    {
        [ProtoMember(1)]
        public long Id;
    }
}