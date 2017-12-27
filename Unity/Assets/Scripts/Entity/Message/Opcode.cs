namespace Model
{
    // 1000开始
    public static partial class Opcode
    {
        #region ETOuterMessage
        public const ushort FrameMessage = 1000;
        public const ushort R2C_ServerLog = 1003;
        public const ushort C2M_Reload = 1008;
        #endregion

        #region ClientOuterMessage
        public const ushort Login_RT = 1010;
        public const ushort Login_RE = 1011;
        public const ushort Register_RT = 1012;
        public const ushort Register_RE = 1013;
        public const ushort StartMatch_RT = 1014;
        public const ushort StartMatch_RE = 1015;
        public const ushort GetUserInfo_RT = 1016;
        public const ushort GetUserInfo_RE = 1017;
        public const ushort LoginGate_RT = 1018;
        public const ushort LoginGate_RE = 1019;
        public const ushort Quit = 1020;
        public const ushort PlayerJoinRoom_RT = 1021;
        public const ushort PlayerJoinRoom_RE = 1022;
        public const ushort PlayerReady = 1023;
        public const ushort GrabLordSelect = 1024;
        public const ushort PlayCards_RT = 1026;
        public const ushort PlayCards_RE = 1027;
        public const ushort Discard = 1028;
        public const ushort Prompt_RT = 1029;
        public const ushort Prompt_RE = 1030;
        public const ushort ChangeGameMode = 1031;
        #endregion

        #region GateServerOuterMessage
        public const ushort RoomKey = 1100;
        #endregion

        #region MapServerOuterMessage
        public const ushort GamerEnter = 1110;
        public const ushort GamerOut = 1111;
        public const ushort GameStart = 1113;
        public const ushort GamerMoneyLess = 1114;
        public const ushort SelectAuthority = 1115;
        public const ushort SelectLord = 1116;
        public const ushort AuthorityPlayCard = 1118;
        public const ushort GamerPlayCards = 1119;
        public const ushort GameMultiples = 1120;
        public const ushort Gameover = 1121;
        public const ushort GamerReconnect = 1122;
        public const ushort GamerReenter = 1223;
        #endregion
    }
}
