namespace Model
{
	public static class ErrorCode
	{
		public const int ERR_Success = 0;
		public const int ERR_RpcFail = 1;
		public const int ERR_AccountOrPasswordError = 2;
		public const int ERR_ConnectGateKeyError = 3;
		public const int ERR_ReloadFail = 4;
		public const int ERR_NotFoundUnit = 5;
        public const int ERR_NotFoundActor = 1;
        public const int ERR_ActorLocationNotFound = 106;
        public const int ERR_SessionActorError = 107;
        public const int ERR_ActorError = 108;
    }

    public static class CommandType
    {
        public const int CT_CreateRoom = 0;
    }


    public static class GameType
    {
        public const int GT_Cow = 0;
    }
}