namespace Model
{
    // 1-999
    public static partial class Opcode
    {
        #region ETInnerMessage
        public const ushort ActorRequest = 1;
        public const ushort ActorResponse = 2;
        public const ushort ActorRpcRequest = 3;
        public const ushort ActorRpcResponse = 4;
        public const ushort G2G_LockRequest = 10;
        public const ushort G2G_LockResponse = 11;
        public const ushort G2G_LockReleaseRequest = 12;
        public const ushort G2G_LockReleaseResponse = 13;

        public const ushort M2A_Reload = 20;
        public const ushort A2M_Reload = 21;

        public const ushort DBSaveRequest = 26;
        public const ushort DBSaveResponse = 27;
        public const ushort DBQueryRequest = 28;
        public const ushort DBQueryResponse = 29;
        public const ushort DBSaveBatchResponse = 37;
        public const ushort DBSaveBatchRequest = 38;
        public const ushort DBQueryBatchRequest = 61;
        public const ushort DBQueryBatchResponse = 62;
        public const ushort DBQueryJsonRequest = 65;
        public const ushort DBQueryJsonResponse = 66;

        public const ushort ObjectAddRequest = 70;
        public const ushort ObjectAddResponse = 71;
        public const ushort ObjectRemoveRequest = 72;
        public const ushort ObjectRemoveResponse = 73;
        public const ushort ObjectLockRequest = 74;
        public const ushort ObjectLockResponse = 75;
        public const ushort ObjectUnLockRequest = 76;
        public const ushort ObjectUnLockResponse = 77;
        public const ushort ObjectGetRequest = 78;
        public const ushort ObjectGetResponse = 79;
        #endregion

        #region RealmServerInnerMessage
        public const ushort GetLoginKey_RT = 100;
        public const ushort GetLoginKey_RE = 101;
        #endregion

        #region GateServerInnerMessage
        public const ushort JoinMatch_RT = 150;
        public const ushort JoinMatch_RE = 151;
        public const ushort PlayerDisconnect = 152;
        public const ushort PlayerQuit = 153;
        #endregion

        #region MatchServerInnerMessage
        public const ushort CreateRoom_RT = 200;
        public const ushort CreateRoom_RE = 201;
        public const ushort GetJoinRoomKey_RT = 202;
        public const ushort GetJoinRoomKey_RE = 203;
        public const ushort MatchSuccess = 204;
        public const ushort PlayerReconnect = 206;
        #endregion

        #region MapServerInnerMessage
        public const ushort GamerQuitRoom = 250;
        public const ushort RoomState = 251;
        #endregion
    }
}
