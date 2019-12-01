using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EMode
    {
        AWARE,
        UNAWARE
    }

    EMode m_CurrentMode;
    public EMode CurrentMode
    {
        get
        {
            return m_CurrentMode;
        }
        set
        {
            if (m_CurrentMode == value)
                return;

            m_CurrentMode = value;

            if (OnModeChange != null)
                OnModeChange(value);
        }
    }

    public event System.Action<EMode> OnModeChange;

    void Awake()
    {
        CurrentMode = EMode.UNAWARE;
    }

}
