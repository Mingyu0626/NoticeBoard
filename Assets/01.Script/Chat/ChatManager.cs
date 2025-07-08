using ExitGames.Client.Photon;
using Photon.Chat;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviourSingleton<ChatManager>, IChatClientListener
{
    // 채팅 이벤트 -> 총 11개(IChatClientListener에 선언된 11개의 메서드)의 이벤트 처리를 통해 채팅 시스템을 완성한다.
    // 1. (1) 서버 로그
    // 1. (3) 서버 접속/해제/상태변화 (카카오톡 접속/해제)
    // 2. (2) 채널 접속/해제 (카카오톡 채팅방(1:1, 오픈채팅) 접속/해제)
    // 3. (2) 메시지 수신 (1:1, 오픈채팅)
    // 4. (2) 다른 사람의 방 입장/퇴장 (카카오톡 단톡방 입장/퇴장)
    // 5. (1) 친구 이벤트(친구 상태변화)

    private ChatClient _client;

    // 기본 채널 이름 (실제 서비스에서는 사용자가 방 목록에서 선택하던지.. 아니면 포톤 방 이름과 같게 자동 접속)
    private const string DEFAULT_CHANNEL = "global";
    private const string MY_NAME = "Mingyu";

    private List<Chat> _chatList;
    public List<Chat> ChatList => _chatList;

    public event Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // IChatClientListener 구현 객체를 this로 넘겨 초기화한다.
        _client = new ChatClient(this);

        // 디버그 로그 레벨
        _client.DebugOut = DebugLevel.ALL;

        // 서버 지역 설정(US, EU, ASIA)
        _client.ChatRegion = "ASIA";

        // 유저 ID
        var auth = new AuthenticationValues(MY_NAME);

        // 채팅 연결
        _client.Connect("3ef16cc8-f034-402c-b0fa-30a27a25e0a2", "1.0.0", auth);

        _chatList = new List<Chat>();
    }

    private void Update()
    {
        // ChatClient는 MonoBehaviour가 아니므로, 매 프레임마다 서비스를 호출해줘야
        // 네트워크 메시지가 처리되고, 콜백 메서드들이 실행된다.
        _client.Service();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SendChatMessage("안녕하세요.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SendChatMessage("반갑습니다.");
        }
    }

    // 포톤챗 내부 로그 발생시 호출되는 메서드(필터링 레벨 이상)
    public void DebugReturn(DebugLevel level, string message)
    {
        switch (level)
        {
            case DebugLevel.ERROR:
                Debug.LogError("[PhotonChat] " + message);
                break;
            case DebugLevel.WARNING:
                Debug.LogWarning("[PhotonChat] " + message);
                break;
            default:
                Debug.Log("[PhotonChat] " + message);
                break;
        }
    }

    // 서버 접속 상태가 변한다.
    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"포톤챗 상태 : {state}");
    }

    public void OnConnected()
    {
        Debug.Log("포톤챗 접속 완료");
        var channelOption = new ChannelCreationOptions { PublishSubscribers = true };
        // 배열인 이유: 여러 채널을 구독한다. (자유 채널, 공지 채널 등)
        // _client.Subscribe(DEFAULT_CHANNEL); // 채널 1개 구독
        // _client.Subscribe(new[] { DEFAULT_CHANNEL }); // 채널 여러개 구독

        Debug.Log("[PhotonChat] Connected");
        int messagesFromHistory = 20;
        // 배열인 이유: 여러 채널을 구독한다. (자유 채널, 공지 채널 등)
        _client.Subscribe(DEFAULT_CHANNEL, 0, messagesFromHistory, creationOptions: channelOption);
    }

    public void OnDisconnected()
    {
        Debug.Log("포톤챗 접속 종료");
        // 반드시 재접속 시도를 해줘야 한다.
        // 와이파이가 끊기거나, IP 벤이 되거나, 
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        /*
        sender, messages 가 배열인 이유:
        Photon 은 네트워크 최적화를 위해 같은 프레임(또는 같은 네트워크 패킷) 안에 들어온
        여러 개의 메시지를 한 번에 묶어 전달할 수 있다. 따라서 한 콜백 호출에 n개의
        발신자(senders) 와 메시지(messages)가 함께 온다.
        messages 는 object[] 이며 주로 string 을 사용하지만 byte[]/JSON 도 가능.
        */
        for (int i = 0; i < messages.Length; i++)
        {
            Debug.Log($"[{channelName}] {senders[i]}: {messages[i]}");

            if (senders[i] == MY_NAME)
            {
                _chatList.Add(new Chat(EChatType.Mine, senders[i], messages[i].ToString()));
            }
            else
            {
                _chatList.Add(new Chat(EChatType.Other, senders[i], messages[i].ToString()));
            }
        }
        OnDataChanged?.Invoke();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"[Whisper] {sender} ▶ {message}");
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        // channels : 이번에 구독 요청한 채널들
        // results  : 구독 성공 여부

        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log($"[PhotonChat] Subscribed ▶ {channels[i]} (success={results[i]})");
        }

        // Todo: 채널 리스트 갱신
        OnDataChanged?.Invoke();
        // -> 내가 구독 중인 채널 모든 목록을 알고 싶다면:
        foreach (var channel in _client.PublicChannels)
        {
            Debug.Log($"현재 구독 중인 채널: {channel.Key}");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach (var ch in channels)
        {
            Debug.Log($"[PhotonChat] Unsubscribed ▶ {ch}");
        }
        // Todo: 채널 리스트 갱신
        OnDataChanged?.Invoke();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log($"[PhotonChat] {user} joined {channel}");
        Chat chat = new Chat(EChatType.System, user, $"{user}님이 방에 입장하였습니다.");
        _chatList.Add(chat);
        OnDataChanged?.Invoke();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"[PhotonChat] {user} left {channel}");
        Chat chat = new Chat(EChatType.System, user, $"{user}님이 방에 퇴장하였습니다.");
        _chatList.Add(chat);
        OnDataChanged?.Invoke();
    }

    public void SendChatMessage(string message)
    {
        if (_client != null && _client.CanChat)
        {
            _client.PublishMessage(DEFAULT_CHANNEL, message);
        }
    }
}
