using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    [Header("Spell Identification")]
    public string SpellName;
    [SerializeField] protected TeamIdentity teamFlag;
    protected Collider2D[] collidersToDamage = new Collider2D[100];
    protected HitBoxType hitBehaviour;
    protected List<GameObject> collidersDamaged;
    [SerializeField]protected Collider2D hitCollider;

    [Header("Damage Values")]
    [SerializeField] protected MagicType magicType;
    [SerializeField] protected StatusEffect statusEffect;
    [SerializeField] protected float[] _AttackValues = new float[8];
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float magicDamage;
    [SerializeField] protected float stunTime;

    [Header("Motion Values")]
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;

    [Header("Explosion Values")]
    [SerializeField] protected AudioClip SFXCexplosion;
    [SerializeField] protected ParticleSystem VFX_explosion;
    [SerializeField] protected float explosionForceMultiplier = 10;
    [SerializeField] protected float explosionRadius = 1;
    [SerializeField] protected GameObject lingeringEffect;
    
    
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Invoke(nameof(EndSpell), lifeTime);
        if (GetComponent<Rigidbody2D>())
        {rb = GetComponent<Rigidbody2D>();}
        _AttackValues[0] = baseDamage;
        _AttackValues[1] = magicDamage;
        _AttackValues[2] = (float)magicType;
        _AttackValues[3] = (float)statusEffect;
        _AttackValues[4] = stunTime;
        if (GetComponent<Rigidbody2D>())
        { rb.velocity = speed * -transform.up; }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public virtual void EndSpell()
    {
        Destroy(this.gameObject);
    }

    public virtual void Explode()
    {
        AudioSource.PlayClipAtPoint(SFXCexplosion, transform.position,1f);

        if (VFX_explosion != null)
        {
            Destroy(Instantiate(VFX_explosion, transform.position, Quaternion.identity), 3);
        }
        collidersToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D j in collidersToDamage)
        {
            CharacterStats hitEntity = j.GetComponentInChildren<CharacterStats>();


            // Only check colliders with a valid Team Componnent attached
            if (hitEntity && (hitEntity.team != teamFlag))
            {
                Rigidbody2D rb = j.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 distance = j.transform.position - transform.position;
                    float explosionForce = explosionForceMultiplier / distance.magnitude;
                    j.BroadcastMessage("TakeDamage", _AttackValues, SendMessageOptions.DontRequireReceiver);
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(distance.normalized * explosionForce);
                }
            }

        }
        
    }
}
