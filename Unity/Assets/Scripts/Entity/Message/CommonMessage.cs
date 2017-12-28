using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace Model
{
    [BsonIgnoreExtraElements]
    [ProtoContract]
    public class PlayerBaseInfo
    {
        [ProtoMember(2)]
        public long roleId;
    }


    [BsonIgnoreExtraElements]
    [ProtoContract]
    public class GameCommand
    {
        [ProtoMember(1)]
        public int roomId;
        [ProtoMember(2)]
        public int ComandType;
        [ProtoMember(3)]
        public int GameType;
        [ProtoMember(4)]
        public string mComandContent;
    }
}