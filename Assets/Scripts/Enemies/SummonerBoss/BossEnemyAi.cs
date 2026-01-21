using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAi : MonoBehaviour
{
    public StateMachine stateMachine;
    private Animator animator;
    private CharacterStats bossCharacter;

    [Header("Teleportation")]
    [SerializeField] private Transform[] TeleportationSpot;
    private int oldTele;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float floatingspeed = 3f;

    [Header("Spells")]
    [SerializeField] private GameObject[] Spells;
    [SerializeField] private GameObject spellSpawnPoint;

    [Header("Spawned Enemies")]
    [SerializeField] private GameObject[] enemyToSpawn;
    [SerializeField] private GameObject[] crystal;
    [SerializeField] private GameObject[] spawnPoint;

    [Header("Shield")]
    [SerializeField] private GameObject Shield;
    [SerializeField] private SpriteRenderer ShieldIntegrity;
    [SerializeField] private Sprite[] differendShieldLevels;
    private int shieldHP = 0;
    private int prevAction = 0;
    private bool stunned = true;
    private float evaluateInterval = 3;

    public Transform target;

    Rigidbody2D rb;
    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        bossCharacter = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine("BossPattern");
        //EvaluateBehaviour();
        bossCharacter.invincible = true;
        //rb.MovePosition(new Vector2(0,0));
    }
    public IEnumerator BossPattern()
    {
        for(; ; )
        {
            EvaluateBehaviour();
            yield return new WaitForSeconds(evaluateInterval);
        }
    }

    public void EvaluateBehaviour()
    {
        //Debug.Log("Evaluation in progress");
        if(bossCharacter.Health <=0)
        {
            stateMachine.SetNextStateToFinal();
            return;
        }
        if(shieldHP<=0 && stunned == false)
        {
            stateMachine.SetNextState(new BossStunState());
            stunned = true;
            return;
        }
        else if (shieldHP <= 0)
        {
            //Debug.Log("I need to summon");
            stateMachine.SetNextState(new SummonState());
            evaluateInterval = 3;
            stunned = false;
            return;
        }
        int rnd = Random.Range(0, 3);
        while (rnd == prevAction)
        {
            rnd = Random.Range(0, 3);
        }
        prevAction = rnd;        
        switch (rnd)
        {
            case 0:
                ZeroOutMovement();
                stateMachine.SetNextState(new IdleBossState());
                break;
            case 1:
                //Debug.Log("I should be teleporting");
                stateMachine.SetNextState(new TeleportBossState());
                break;
            case 2:
                stateMachine.SetNextState(new BossAttackState());
                break;
            default:
                break;
        }
    }

    public void randomTeleport()
    {
        int rand = Random.Range(0, TeleportationSpot.Length);
        while (rand == oldTele)
        {
            rand = Random.Range(0, TeleportationSpot.Length);
        }
        oldTele = rand;
        gameObject.transform.position = TeleportationSpot[rand].position;
        Invoke(nameof(FloatToTarget), 0.1f);
    }

    public void centerReset()
    {
        gameObject.transform.position = centerPoint.position;
        //stateMachine.SetNextState(new SummonState());
    }

    public void SummonMinions()
    {
        //Debug.Log("I am summoning");

        for (int j = 0; j < 4; j++)
        {
            GameObject minion = Instantiate(enemyToSpawn[j], spawnPoint[j].transform.position, spawnPoint[j].transform.rotation);
            minion.AddComponent<ShieldMinion>();
            minion.GetComponent<ShieldMinion>().AssignBoss(this);
            minion.GetComponent<ShieldMinion>().AssignCrystal(crystal[j], crystal[j + 4]);
        }
    }

    public void DepleteShield()
    {
        shieldHP--;
        AudioManager.instance.PlayCrackingShield();
        ShieldIntegrity.sprite = differendShieldLevels[shieldHP];
        if(shieldHP<=0)
        {
            ZeroOutMovement();
            bossCharacter.invincible = false;
            AudioManager.instance.PlayBreakingShield();
            AudioManager.instance.PlayBossHurt();
            ShieldIntegrity.sprite = null;
            StopAllCoroutines();
            evaluateInterval = 7;
            StartCoroutine("BossPattern");
        }

    }

    public void AttackSpell()
    {
        //Debug.Log("Smackin");
        Vector2 direction = ((Vector2)(target.position - spellSpawnPoint.transform.position));
        spellSpawnPoint.transform.up = -direction;
        int rand = Random.Range(0, 4);
        Instantiate(Spells[rand], spellSpawnPoint.transform.position, spellSpawnPoint.transform.rotation);
    }

    public void ResetShield()
    {
        shieldHP = 4;
        ShieldIntegrity.sprite = differendShieldLevels[shieldHP];
        for (int j = 0; j < 4; j++)
        {
            crystal[j].SetActive(true);
            crystal[j + 4].SetActive(true);
        }
            bossCharacter.invincible = true;
    }
    private void FloatToTarget()
    {
        ZeroOutMovement();
        Vector2 facingDirection = ((Vector2)target.position - rb.position).normalized;
        rb.velocity = facingDirection * floatingspeed;
    }
    private void ZeroOutMovement()
    {
        rb.velocity = Vector2.zero;
    }
    public void BossRoar()
    {
        AudioManager.instance.PlayBossRoar();
    }
    public void BossHurt()
    {
        AudioManager.instance.PlayBossHurt();
    }
    public void BossDeath()
    {
        AudioManager.instance.PlayBossDeath();
    }

    public void BossWarp()
    {
        AudioManager.instance.PlayWarp();
    }
    public void BossWarpReversed()
    {
        AudioManager.instance.PlayWarpReversed();
    }
    public void BossPowerUp()
    {
        AudioManager.instance.PlayBossPowerUp();
    }
    public void BossSpellFire()
    {
        AudioManager.instance.PlayBossSpellFired();
    }
}
