using System.Collections.Generic;using MongoDB.Bson.Serialization.Attributes;


// 服务器内部消息 Opcode从10000开始
namespace Model{

    #region ETInnerMessage    [Message(Opcode.ActorRequest)]
    [BsonIgnoreExtraElements]
    public class ActorRequest : ARequest
    {
        public long Id { get; set; }

        public AMessage AMessage { get; set; }
    }

    [Message(Opcode.ActorResponse)]
    [BsonIgnoreExtraElements]
    public class ActorResponse : AResponse
    {
    }

    [Message(Opcode.ActorRpcRequest)]
    [BsonIgnoreExtraElements]
    public class ActorRpcRequest : ActorRequest
    {
    }    [Message(Opcode.ActorRpcResponse)]
    [BsonIgnoreExtraElements]
    public class ActorRpcResponse : ActorResponse
    {
        public AMessage AMessage { get; set; }
    }


    [Message(Opcode.M2A_Reload)]
    [BsonIgnoreExtraElements]
    public class M2A_Reload : ARequest
    {
    }


    [Message(Opcode.A2M_Reload)]
    [BsonIgnoreExtraElements]
    public class A2M_Reload : AResponse
    {
    }


    [Message(Opcode.G2G_LockRequest)]
    [BsonIgnoreExtraElements]
    public class G2G_LockRequest : ARequest
    {
        public long Id;
        public string Address;
    }


    [Message(Opcode.G2G_LockResponse)]
    [BsonIgnoreExtraElements]
    public class G2G_LockResponse : AResponse
    {
    }

    [Message(Opcode.G2G_LockReleaseRequest)]
    [BsonIgnoreExtraElements]
    public class G2G_LockReleaseRequest : ARequest
    {
        public long Id;
        public string Address;
    }


    [Message(Opcode.G2G_LockReleaseResponse)]
    [BsonIgnoreExtraElements]
    public class G2G_LockReleaseResponse : AResponse
    {
    }

    [Message(Opcode.DBSaveRequest)]
    [BsonIgnoreExtraElements]
    public class DBSaveRequest : ARequest
    {
        public bool NeedCache = true;

        public string CollectionName = "";

        public Entity Entity;
    }



    [Message(Opcode.DBSaveBatchResponse)]
    [BsonIgnoreExtraElements]
    public class DBSaveBatchResponse : AResponse
    {
    }


    [Message(Opcode.DBSaveBatchRequest)]
    [BsonIgnoreExtraElements]
    public class DBSaveBatchRequest : ARequest
    {
        public bool NeedCache = true;
        public string CollectionName = "";
        public List<Entity> Entitys = new List<Entity>();
    }

    [Message(Opcode.DBSaveResponse)]
    [BsonIgnoreExtraElements]
    public class DBSaveResponse : AResponse
    {
    }


    [Message(Opcode.DBQueryRequest)]
    [BsonIgnoreExtraElements]
    public class DBQueryRequest : ARequest
    {
        public long Id;
        public string CollectionName { get; set; }
        public bool NeedCache = true;
    }


    [Message(Opcode.DBQueryResponse)]
    [BsonIgnoreExtraElements]
    public class DBQueryResponse : AResponse
    {
        public Entity Entity;
    }


    [Message(Opcode.DBQueryBatchRequest)]
    [BsonIgnoreExtraElements]
    public class DBQueryBatchRequest : ARequest
    {
        public string CollectionName { get; set; }
        public List<long> IdList { get; set; }
        public bool NeedCache = true;
    }

    [Message(Opcode.DBQueryBatchResponse)]
    [BsonIgnoreExtraElements]
    public class DBQueryBatchResponse : AResponse
    {
        public List<Entity> Entitys;
    }


    [Message(Opcode.DBQueryJsonRequest)]
    [BsonIgnoreExtraElements]
    public class DBQueryJsonRequest : ARequest
    {
        public string CollectionName { get; set; }
        public string Json { get; set; }
        public bool NeedCache = true;
    }

    [Message(Opcode.DBQueryJsonResponse)]
    [BsonIgnoreExtraElements]
    public class DBQueryJsonResponse : AResponse
    {
        public List<Entity> Entitys;
    }

    [Message(Opcode.ObjectAddRequest)]
    [BsonIgnoreExtraElements]
    public class ObjectAddRequest : ARequest
    {
        public long Key { get; set; }
        public int AppId { get; set; }
    }

    [Message(Opcode.ObjectAddResponse)]
    [BsonIgnoreExtraElements]
    public class ObjectAddResponse : AResponse
    {
    }

    [Message(Opcode.ObjectRemoveRequest)]
    [BsonIgnoreExtraElements]
    public class ObjectRemoveRequest : ARequest
    {
        public long Key { get; set; }
    }

    [Message(Opcode.ObjectRemoveResponse)]
    [BsonIgnoreExtraElements]
    public class ObjectRemoveResponse : AResponse
    {
    }

    [Message(Opcode.ObjectLockRequest)]
    [BsonIgnoreExtraElements]
    public class ObjectLockRequest : ARequest
    {
        public long Key { get; set; }
        public int LockAppId { get; set; }
        public int Time { get; set; }
    }

    [Message(Opcode.ObjectLockResponse)]
    [BsonIgnoreExtraElements]
    public class ObjectLockResponse : AResponse
    {
    }

    [Message(Opcode.ObjectUnLockRequest)]
    [BsonIgnoreExtraElements]
    public class ObjectUnLockRequest : ARequest
    {
        public long Key { get; set; }
        public int UnLockAppId { get; set; }
        public int AppId { get; set; }
    }

    [Message(Opcode.ObjectUnLockResponse)]
    [BsonIgnoreExtraElements]
    public class ObjectUnLockResponse : AResponse
    {
    }

    [Message(Opcode.ObjectGetRequest)]
    [BsonIgnoreExtraElements]
    public class ObjectGetRequest : ARequest
    {
        public long Key { get; set; }
    }

    [Message(Opcode.ObjectGetResponse)]
    [BsonIgnoreExtraElements]
    public class ObjectGetResponse : AResponse
    {
        public int AppId { get; set; }
    }




    #endregion
    #region RealmServerInnerMessage    [Message(Opcode.GetLoginKey_RT)]
    [BsonIgnoreExtraElements]
    public class GetLoginKey_RT : ARequest
    {
        public long UserID;
    }

    [Message(Opcode.GetLoginKey_RE)]
    [BsonIgnoreExtraElements]
    public class GetLoginKey_RE : AResponse
    {
        public long Key;
    }
    #endregion
    #region GateServerInnerMessage
    [Message(Opcode.JoinMatch_RT)]
    [BsonIgnoreExtraElements]
    public class JoinMatch_RT : ARequest
    {
        public long PlayerID;
        public long UserID;
        public long GateSessionID;
        public int GateAppID;
    }

    [Message(Opcode.JoinMatch_RE)]
    [BsonIgnoreExtraElements]
    public class JoinMatch_RE : AResponse
    {
        public long ActorID;
    }

    [Message(Opcode.PlayerDisconnect)]
    [BsonIgnoreExtraElements]
    public class PlayerDisconnect : AMessage
    {
        public long UserID;
    }

    [Message(Opcode.PlayerQuit)]
    [BsonIgnoreExtraElements]
    public class PlayerQuit : AActorMessage
    {
        public long PlayerID;
    }
    #endregion

    #region MatchServerInnerMessage
    [Message(Opcode.CreateRoom_RT)]
    [BsonIgnoreExtraElements]
    public class CreateRoom_RT : ARequest
    {
        public RoomLevel Level;
    }

    [Message(Opcode.CreateRoom_RE)]
    [BsonIgnoreExtraElements]
    public class CreateRoom_RE : AResponse
    {
        public long RoomID;
    }

    [Message(Opcode.GetJoinRoomKey_RT)]
    [BsonIgnoreExtraElements]
    public class GetJoinRoomKey_RT : AActorRequest
    {
        public long PlayerID;
        public long UserID;
        public long GateSeesionID;
    }

    [Message(Opcode.GetJoinRoomKey_RE)]
    [BsonIgnoreExtraElements]
    public class GetJoinRoomKey_RE : AActorResponse
    {
        public long Key;
    }

    [Message(Opcode.MatchSuccess)]
    [BsonIgnoreExtraElements]
    public class MatchSuccess : AMessage
    {
        public long PlayerID;
        public long RoomID;
        public long Key;
    }

    [Message(Opcode.PlayerReconnect)]
    [BsonIgnoreExtraElements]
    public class PlayerReconnect : AMessage
    {
        public long PlayerID;
        public long UserID;
        public long GateSessionID;
    }
    #endregion

    #region MapServerInnerMessage
    [Message(Opcode.GamerQuitRoom)]
    [BsonIgnoreExtraElements]
    public class GamerQuitRoom : AMessage
    {
        public long RoomID;
        public long PlayerID;
    }

    [Message(Opcode.RoomState)]
    [BsonIgnoreExtraElements]
    public class SyncRoomState : AMessage
    {
        public long RoomID;
        public RoomState State;
    }
    #endregion
}