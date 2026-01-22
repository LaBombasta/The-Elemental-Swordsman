using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : BasicSpell
{
    private CircleCollider2D coll;
    public LayerMask enemyLayer;

    public GameObject chainLightningEffect;

    public GameObject beenStruck;

    public int amountToChain;

    private GameObject startObject;
    public GameObject endObject;

    private Animator ani;

    public ParticleSystem parti;

    private int singleSpawns;

    override public void Start()
    {
        base.Start();
        if(amountToChain <= 0)
        {
            Destroy(gameObject);
        }
        coll = GetComponent<CircleCollider2D>();

        ani = GetComponent<Animator>();

        parti = GetComponent<ParticleSystem>();

        startObject = this.gameObject;
        singleSpawns = 1;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(enemyLayer == (enemyLayer | (1<< collision.gameObject.layer)) && !collision.GetComponentInChildren<Destroymyself>())
        {
            //Debug.Log("Hi");
            if (singleSpawns != 0)
            {
                if(collision.gameObject.GetComponent<CharacterStats>())
                {
                    endObject = collision.gameObject;
                    amountToChain -= 1;
                    Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity);

                    Instantiate(beenStruck, collision.gameObject.transform);

                    collision.gameObject.GetComponent<CharacterStats>().TakeDamage(_AttackValues);

                    ani.StopPlayback();

                    coll.enabled = false;

                    singleSpawns--;

                    parti.Play();

                    var emitParams = new ParticleSystem.EmitParams();
                    emitParams.position = startObject.transform.position;

                    parti.Emit(emitParams, 1);

                    emitParams.position = endObject.transform.position;

                    parti.Emit(emitParams, 1);

                    Destroy(gameObject, 1);
                }
                
            }

        }
    }
}
