using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeamIdentity : sbyte
{
    None = -1,
    Neutral = 0,
    Player =1,
    Enemy = 2,
    Environment = 3,
}
public enum StatusEffect : sbyte
{
    None = 0,
    Frozen = 1,
    Shocked = 2,
}
public class CharacterStats : MonoBehaviour
{
    [Header("Character Identification")]
    public TeamIdentity team = TeamIdentity.Neutral;
    public string Name;
    public bool boss;
    public EnemyAi enemyAi;

    [Header("Character Health")]
    public float Health;
    public float MaxHealth;
    public Image healthBar;

    [Header("Visual Effects")]
    public ParticleSystem hitEffect;

    [Header("Driving Values")]
    public AttackEditorValues[] AttackValue;
    private StateMachine meleeStateMachine;
    public Collider2D hitbox;
    public HitBoxType hitType;
    private float stunTime;
    public float MoveSpeed;
    
    public GameObject thisCharacter;

    [Header("Status Effect Values")]
    public StatusEffect statusEffect;
    Dictionary<MagicType, float> Resistances = new Dictionary<MagicType, float>();
    public float[] DamageTypeResitances; //This must have a value for all damage types even if it is 0;
    public float[] StatusTypeResistances;//This must have a value for all status types even if it is 0;

    public bool alive = true;
    public bool invincible { get; set; }
    public bool noMove = false;

    [Header("Status Effect Values")]
    [SerializeField]AudioSource SFXSource;
    [SerializeField]private AudioClip stunned;
    [SerializeField] private AudioClip shielded;
    [SerializeField]private AudioClip burned;
    [SerializeField] private AudioClip shocked;
    [SerializeField] private AudioClip frozen; 


    private void Start()
    {
        thisCharacter = this.gameObject;
        meleeStateMachine = GetComponent<StateMachine>();
        Resistances.Add(MagicType.Lightning, DamageTypeResitances[0]);
        Resistances.Add(MagicType.Fire, DamageTypeResitances[1]);
        Resistances.Add(MagicType.Water, DamageTypeResitances[2]);
        Resistances.Add(MagicType.Metal, DamageTypeResitances[3]);
        Resistances.Add(MagicType.None, DamageTypeResitances[4]);

        if(noMove)
        {
            MoveSpeed = 0;
        }
        if (GetComponent<EnemyAi>())
        {
            enemyAi = GetComponent<EnemyAi>();
        }
    }
    public void TakeDamage(float[] damage)
    {
        if (!invincible)
        {
            SFXSource.PlayOneShot(stunned);
        }
        else
        {
            SFXSource.PlayOneShot(shielded);
            return;
        }
        //Debug.Log(this.gameObject.name + " is taking damage?");
        Health -= damage[0];
        stunTime = damage[4];
        //this works
        //Debug.Log(damage[2]);
        //Debug.Log((MagicType)damage[2]);j
        float magicResist = Resistances[(MagicType)damage[2]];
        float modifier = 100 - magicResist;
        modifier /= 100;
        float elementalDamage = damage[1] * modifier;
        Health -= elementalDamage;

        
        if (healthBar)
        {
            healthBar.fillAmount = Health / MaxHealth;
        }
        if (meleeStateMachine == null)
        {
            return;
        }
        
        
        if (Health <= 0)
        {
            meleeStateMachine.SetNextStateToFinal();
            return;
        }
        if (boss)
        {
            return;
        }
        //Debug.Log(Name);
        if (stunTime>0&& meleeStateMachine.CurrentState != new FrozenState())
        {
            switch ((StatusEffect)damage[3])
            {
                case StatusEffect.None:
                    meleeStateMachine.SetNextState(new StunnedState());
                    break;
                case StatusEffect.Frozen:
                    stunTime += stunTime * modifier;
                    meleeStateMachine.SetNextState(new FrozenState());
                    break;
                case StatusEffect.Shocked:
                    meleeStateMachine.SetNextState(new StunnedState());
                    break;
                default:
                    meleeStateMachine.SetNextState(new StunnedState());
                    break;
            }
                    
        }
        
    }
    
    public float GetStunTime()
    {
        return stunTime;
    }
   
    
}

[System.Serializable]
public class AttackEditorValues
{
    [Header("Damage Values")]
    //baseDamage is damage that will happen regardless
    [SerializeField] public float baseDamage;
    //magicDamage will be calculated with resistances in mind
    [SerializeField] public float magicDamage;

    [Header("Secondary effect")]
    [SerializeField] public MagicType magicType;
    [SerializeField] public StatusEffect statusEffect;
    

    [Header("Active Values")]
    [SerializeField] public float Speed;
    [SerializeField] public float Duration;
    [SerializeField] public float Knockback;
    [SerializeField] public float StunTime;


}
