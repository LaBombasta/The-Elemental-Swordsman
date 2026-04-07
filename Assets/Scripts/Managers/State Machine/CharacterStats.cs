using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public bool perfectPress = false;
    [Header("Character Identification")]
    public TeamIdentity team = TeamIdentity.Neutral;
    public string Name;
    public bool boss;
    public EnemyAi enemyAi;

    [Header("Character Health")]
    public float Health;
    public float MaxHealth;
    public Image healthBar;
    public GameObject FloatingText;

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
    public bool Invincible = false;
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
        if(team == TeamIdentity.Environment)
        {
            return;
        }
        if (!Invincible)
        {
            SFXSource.PlayOneShot(stunned);
        } //Hurt animations need this to be changed to else {return;} to prevent multiple hits. In short invicible is being used wrong in this script
        else
        {
            SFXSource.PlayOneShot(shielded);
            if(!boss)
            {
                Health -= damage[0] / 3;

            }
            if (FloatingText)
            {
                int damageUndiv = (int)(damage[0] / 3); 
                SpawnFloatingText("-" + damageUndiv, 5);
                float magicResistI = Resistances[(MagicType)damage[2]];
                float modifierI = 100 - magicResistI;
                if ((MagicType)damage[2] != MagicType.None)
                {
                    modifierI /= 100;
                    float elementalDamageI = damage[1] * modifierI;
                    Health -= elementalDamageI/3;
                    int e = (int)elementalDamageI/3;
                    SpawnFloatingText("-" + e.ToString(), (int)damage[2]);
                }
            }
            return;
        }
        
        float normalResist = Resistances[(MagicType)damage[2]];
        Health -= damage[0];
        stunTime = damage[4];
        if(FloatingText && damage[0] > 0)
        {
            int d = (int)damage[0];
            SpawnFloatingText("-"+ damage[0].ToString(), 5);
        }
        
        float magicResist = Resistances[(MagicType)damage[2]];
        float modifier = 100 - magicResist;
        if ((MagicType)damage[2] != MagicType.None)
        {
            modifier /= 100;
            float elementalDamage = damage[1] * modifier;
            Health -= elementalDamage;
            int e = (int)elementalDamage;
            SpawnFloatingText("-" + e.ToString(), (int)damage[2]);
        }
        
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
                    //stunTime += stunTime * modifier;
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

    public void HealMe(float healAmt)
    {
        Health += healAmt;
        if(Health>MaxHealth)
        {
            Health = MaxHealth;
        }
        if (healthBar)
        {
            healthBar.fillAmount = Health / MaxHealth;
        }
    }
    void SpawnFloatingText(string info, int colorType)
    {
        if(info == "-0")
        {
            return;
        }
        var floatText = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        floatText.GetComponent<TextMeshPro>().text = info;
        floatText.GetComponent<TextMeshPro>().color = ChooseTextColor(colorType);
        floatText.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-.5f,.5f),1)*200);
        Vector3 randomOffset = new Vector3(Random.Range(-.75f, .75f), Random.Range(-.5f, .5f), 0f);
        floatText.transform.localPosition += randomOffset;
    }
    Color ChooseTextColor(int colorPicker)
    {
        switch(colorPicker)
        {
            case 0:
                return Color.yellow;
            case 1:
                return Color.red;
            case 2:
                return Color.cyan;
            case 3:
                return Color.gray;
            default:
                return Color.white;
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
