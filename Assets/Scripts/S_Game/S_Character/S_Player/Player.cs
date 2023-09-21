using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonObject<Player>
{
    #region State

    public DashState dashState {get; private set;}
    public IdleState idleState {get; private set;}
    public InAirState inAirState {get; private set;}
    public HitState hitState {get; private set;}
    public JumpState jumpState {get; private set;}
    public MoveState moveState {get; private set;}
    public MeleeAttackState meleeAttackState {get; private set;}

    #endregion

    #region Animation Clip Data

    int dashId = Animator.StringToHash("Dash");
    int idleId = Animator.StringToHash("Idle");
    int hitId = Animator.StringToHash("Hit");
    int moveId = Animator.StringToHash("Run");

    #endregion

    #region Core Component

    private Health health;
    private Combat combat;

    #endregion

    [SerializeField] PlayerData data;
    [SerializeField] private float explosionForce = 2f;

    StateMachine stateMachine;
    Core core;
    public InputManager inputManager {get; private set;}

    #region Set up
    
    protected override void Awake() 
    {
        base.Awake();

        inputManager = GetComponent<InputManager>();
        core = GetComponentInChildren<Core>();
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => core.GetCoreComponent<Health>() != null);
        health = core.GetCoreComponent<Health>();
        health.onDie += OnResetPlayer;

        combat = core.GetCoreComponent<Combat>();
    }

    private void OnDisable()
    {
        health.onDie -= OnResetPlayer;
    }

    private void OnResetPlayer()
    {
        transform.position = GameSettings.Instance.CheckPoint.position;
        SetUpHealthComponent();
        combat.EnableCollider();

        stateMachine.ChangeState(idleState);
    }

    void Start() 
    {
        CreateState();
        
        stateMachine.Initialize(idleState);

        GetCoreComponent();
    }

    void CreateState()
    {
        stateMachine = new StateMachine();
        
        dashState = new DashState(this, core, stateMachine, data, dashId);
        idleState = new IdleState(this, core, stateMachine, data, idleId);
        hitState = new HitState(this, core, stateMachine, data, hitId);
        moveState = new MoveState(this, core, stateMachine, data, moveId);
    }

    void GetCoreComponent()
    {
        SetUpCombatComponent(core.GetCoreComponent<Combat>());
        SetUpHealthComponent();
        SetUpRecoveryComponent(core.GetCoreComponent<RecoveryController>());
    }

    void SetUpHealthComponent()
    {
        if (health == null) health = core.GetCoreComponent<Health>();
        health.SetHealth(data.healthData);
    }

    void SetUpCombatComponent(Combat combat)
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Player, IDamageable.KnockbackType.player); 
    }

    void SetUpRecoveryComponent(RecoveryController recoveryController)
    {
        recoveryController.SetHitData(data.hitData);
    }

    #endregion

    void Update() 
    {
        stateMachine.Update();
    }

    void FixedUpdate() 
    {
        stateMachine.FixedUpdate();
    }


    #region Get 

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        return core.GetCoreComponent<T>();
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ExplodeEnemy(other.gameObject);
        }
    }

    private void ExplodeEnemy(GameObject enemy)
    {
        Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

        if (enemyRigidbody != null)
        {
            Vector2 explosionDirection = (enemy.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
        }

        Destroy(enemy);
    }

    #region On Draw Gizmos


    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(data.meleeAttackData.leftAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
        Gizmos.DrawWireSphere(data.meleeAttackData.rightAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
    }


    #endregion

}
