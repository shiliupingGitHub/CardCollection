using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace Model
{
	[ProtoContract]
	[ProtoInclude(20000, typeof(AFrameMessage))]
    [BsonKnownTypes(typeof(PlayerReady))]
    [BsonKnownTypes(typeof(RoomKey))]
    [BsonKnownTypes(typeof(GamerEnter))]
    [BsonKnownTypes(typeof(GamerOut))]
    [BsonKnownTypes(typeof(GameStart))]
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
    public abstract class AActorMessage : AMessage
	{
	}

    [BsonKnownTypes(typeof(PlayerJoinRoom_RT))]
    [BsonKnownTypes(typeof(PlayCards_RT))]
    [BsonKnownTypes(typeof(Prompt_RT))]
    public abstract class AActorRequest : ARequest
	{
	}

    [BsonKnownTypes(typeof(PlayerJoinRoom_RE))]
    [BsonKnownTypes(typeof(PlayCards_RE))]
    [BsonKnownTypes(typeof(Prompt_RE))]
    public abstract class AActorResponse : AResponse
	{
	}

	[ProtoContract]
	public abstract class AFrameMessage : AActorMessage
	{
		[ProtoMember(1)]
		public long Id;
	}
}