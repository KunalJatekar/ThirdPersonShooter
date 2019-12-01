using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public event System.Action<Player> OnLocalPlayerJoin;
    public bool IsPaused { get; set;}

    GameObject gameObject;
    static GameManager m_Instance;
    InputController m_InputController;
    Player m_LocalPlayer;
    Timer m_Timer;
    EventBus m_EventBus;
    Respawner m_Respawner;

    public static GameManager instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_GameManager");
                m_Instance.gameObject.AddComponent<InputController>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respawner>();
            }
            return m_Instance;
        }
    }

    public InputController GetInputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();
            }
            return m_InputController;
        }
    }

    public Player LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value;
            if(OnLocalPlayerJoin != null)
            {
                OnLocalPlayerJoin(m_LocalPlayer);
            }
        }
    }

    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    public EventBus EventBus
    {
        get
        {
            if (m_EventBus == null)
                m_EventBus = new EventBus();
            return m_EventBus;
        }
    }

    public Respawner Respawner
    {
        get
        {
            if (m_Respawner == null)
                m_Respawner = gameObject.GetComponent<Respawner>();
            return m_Respawner;
        }
    }
}
