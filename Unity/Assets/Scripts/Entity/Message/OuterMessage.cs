// 服务器与客户端之间的消息 Opcode从1-9999

using System.Collections.Generic;
using ProtoBuf;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Model
{
    #region ETOuterMessage
    public struct FrameMessageInfo
    {
        public long Id;
        public AMessage Message;
    }

    // 服务端发给客户端,每帧一条
    [Message(Opcode.FrameMessage)]
    public class FrameMessage : AActorMessage
    {
        public int Frame;
        public List<AFrameMessage> Messages = new List<AFrameMessage>();
    }

    [Message(Opcode.C2M_Reload)]
    public class C2M_Reload : ARequest
    {
        public AppType AppType;
    }

    [Message(11)]
    public class M2C_Reload : AResponse
    {
    }

    [Message(14)]
    public class C2R_Ping : ARequest
    {
    }

    [Message(15)]
    public class R2C_Ping : AResponse
    {
    }
    #endregion

    #region ClientOuterMessage
    [Message(Opcode.Login_RT)]
    public class Login_RT : ARequest
    {
        public string Account;
        public string Password;
    }

    [Message(Opcode.Login_RE)]
    public class Login_RE : AResponse
    {
        public long Key;
        public string Address;
    }

    [Message(Opcode.Register_RT)]
    public class Register_RT : ARequest
    {
        public string Account;
        public string Password;
    }

    [Message(Opcode.Register_RE)]
    public class Register_RE : AResponse
    {

    }

    [Message(Opcode.StartMatch_RT)]
    public class StartMatch_RT : ARequest
    {
        public long PlayerID;
        public RoomLevel Level;
    }

    [Message(Opcode.StartMatch_RE)]
    public class StartMatch_RE : AResponse
    {

    }

    [Message(Opcode.GetUserInfo_RT)]
    public class GetUserInfo_RT : ARequest
    {
        public long UserID;
    }

    [Message(Opcode.GetUserInfo_RE)]
    public class GetUserInfo_RE : AResponse
    {
        public string NickName;
        public int Wins;
        public int Loses;
        public long Money;
    }

    [Message(Opcode.LoginGate_RT)]
    public class LoginGate_RT : ARequest
    {
        public long Key;
    }

    [Message(Opcode.LoginGate_RE)]
    public class LoginGate_RE : AResponse
    {
        public long PlayerID;
        public long UserID;
    }

    [Message(Opcode.Quit)]
    public class Quit : AMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.PlayerJoinRoom_RT)]
    public class PlayerJoinRoom_RT : AActorRequest
    {
        public long Key;
    }

    [Message(Opcode.PlayerJoinRoom_RE)]
    public class PlayerJoinRoom_RE : AActorResponse
    {

    }

    [Message(Opcode.PlayerReady)]
    public class PlayerReady : AActorMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.GrabLordSelect)]
    public class GrabLordSelect : AActorMessage
    {
        public long PlayerID;
        public bool IsGrab;
    }

    [Message(Opcode.PlayCards_RT)]
    public class PlayCards_RT : AActorRequest
    {
        public long PlayerID;
        public Card[] Cards;
    }

    [Message(Opcode.PlayCards_RE)]
    public class PlayCards_RE : AActorResponse
    {

    }

    [Message(Opcode.Discard)]
    public class Discard : AActorMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.Prompt_RT)]
    public class Prompt_RT : AActorRequest
    {
        public long PlayerID;
    }

    [Message(Opcode.Prompt_RE)]
    public class Prompt_RE : AActorResponse
    {
        public Card[] Cards;
    }

    [Message(Opcode.ChangeGameMode)]
    public class ChangeGameMode : AActorMessage
    {
        public long PlayerID;
    }
    #endregion

    #region GateServerOuterMessage
    [Message(Opcode.RoomKey)]
    public class RoomKey : AActorMessage
    {
        public long Key;
    }
    #endregion

    #region MapServerOuterMessage
    public class GamerInfo
    {
        public long PlayerID;
        public long UserID;
        public bool IsReady;
    }

    [Message(Opcode.GamerEnter)]
    public class GamerEnter : AActorMessage
    {
        public long RoomID;
        public GamerInfo[] GamersInfo;
    }

    [Message(Opcode.GamerOut)]
    public class GamerOut : AActorMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.GameStart)]
    public class GameStart : AActorMessage
    {
        public Card[] GamerCards;
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, int> GamerCardsNum;
    }

    [Message(Opcode.GamerMoneyLess)]
    public class GamerMoneyLess : AActorMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.SelectAuthority)]
    public class SelectAuthority : AActorMessage
    {
        public long PlayerID;
    }

    [Message(Opcode.SelectLord)]
    public class SelectLord : AActorMessage
    {
        public long PlayerID;
        public Card[] LordCards;
    }

    [Message(Opcode.AuthorityPlayCard)]
    public class AuthorityPlayCard : AActorMessage
    {
        public long PlayerID;
        public bool IsFirst;
    }

    [Message(Opcode.GamerPlayCards)]
    public class GamerPlayCards : AActorMessage
    {
        public long PlayerID;
        public Card[] Cards;
    }

    [Message(Opcode.GameMultiples)]
    public class GameMultiples : AActorMessage
    {
        public int Multiples;
    }

    [Message(Opcode.Gameover)]
    public class Gameover : AActorMessage
    {
        public Identity Winner;
        public long BasePointPerMatch;
        public int Multiples;
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, long> GamersScore;
    }

    [Message(Opcode.GamerReenter)]
    public class GamerReenter : AActorMessage
    {
        public long PastID;
        public long NewID;
    }

    [Message(Opcode.GamerReconnect)]
    public class GamerReconnect : AActorMessage
    {
        public long PlayerID;
        public int Multiples;
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, Identity> GamersIdentity;
        public Card[] LordCards;
        public KeyValuePair<long, Card[]> DeskCards;
    }
    #endregion
}