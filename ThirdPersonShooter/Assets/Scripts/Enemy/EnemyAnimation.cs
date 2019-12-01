using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    Vector3 lastPosition;
    PathFinder pathFinder;
    EnemyPlayer enemyPlayer;

    bool m_IsCrouched;
    public bool IsCrouched
    {
        get
        {
            return m_IsCrouched;
        }
        internal set
        {
            m_IsCrouched = true;
            GameManager.instance.Timer.Add(CheckIsSaveToStand, 25);
        }
    }

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("isWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE);
        animator.SetFloat("Vertical", velocity / pathFinder.Agent.speed);
        animator.SetBool("isAiming", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
        animator.SetBool("isCrouching", IsCrouched);
    }

    void CheckIsSaveToStand()
    {
        bool isUnaware = enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE;

        if (isUnaware)
            IsCrouched = false;
    }
}
