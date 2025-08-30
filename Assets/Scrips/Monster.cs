using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : LivingEntity
{
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die,
    }
    private Status currentStatus;
    public Status CurrentStatus
    {
        get { return currentStatus; }
        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch (currentStatus)
            {
                case Status.Idle:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool("HasTarget", true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case Status.Die:
                    animator.SetTrigger("Death");
                    agent.isStopped = true;
                    monCollider.enabled = false;
                    uiManager.AddScore();
                    audioSource.PlayOneShot(data.deathClip);
                    break;
            }
        }
    }

    private NavMeshAgent agent;
    private Animator animator;
    private Collider monCollider;
    private AudioSource audioSource;

    private Transform target;
    public float traceDistance;
    public float attackDistance;

    private float damage = 10f;
    private float lastAttackTime;
    private float attackInterval = 0.5f;

    public ParticleSystem hitEffect;

    public MonDatas data;

    public UiManager uiManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

        var findGo = GameObject.FindWithTag("UIManager");
        uiManager = findGo.GetComponent<UiManager>();

        MaxHealth = data.maxHp;
        damage = data.damage;
        agent.speed = data.speed;
    }

    private void Update()
    {
        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        monCollider.enabled = true;
        CurrentStatus = Status.Idle;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        audioSource.PlayOneShot(data.damageClip);

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.forward = hitNormal;
        hitEffect.Play();
    }

    protected override void Die()
    {
        base.Die();
        CurrentStatus = Status.Die;
    }

    private void UpdateIdle()
    {
        target = GameObject.FindWithTag("Player").transform;
        if(target != null)
        {
            CurrentStatus = Status.Trace;
        }
    }

    private void UpdateTrace()
    {
        if(target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }

        agent.SetDestination(target.position);
    }

    private void UpdateAttack()
    {
        if(Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if (lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;

            var damagable = target.GetComponent<IDamagable>();
            if(damagable != null)
            {
                damagable.OnDamage(damage, Vector3.zero, Vector3.zero);
                Debug.Log("Ãæµ¹");
            }
        }
    }

    private void UpdateDie()
    {
        
    }

    public void StartSinking()
    {
        agent.enabled = false;
    }
}
