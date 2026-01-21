using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMinion : MonoBehaviour
{
    private BossEnemyAi bigBoss;
    private GameObject Crystal;
    private GameObject CrystalLock;
    public void AssignBoss(BossEnemyAi theBoss)
    {
        bigBoss = theBoss;
    }
    private void OnDestroy()
    {
        bigBoss.DepleteShield();
        Crystal.SetActive(false);
        CrystalLock.SetActive(false);
    }
    public void AssignCrystal(GameObject myCrystal, GameObject crystalLock)
    {
        Crystal = myCrystal;
        CrystalLock = crystalLock;
    }
}
