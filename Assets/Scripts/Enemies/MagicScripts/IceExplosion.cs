using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosion : BasicSpell
{
    public override void Start()
    {
        base.Start();
        Explode();
    }
}
