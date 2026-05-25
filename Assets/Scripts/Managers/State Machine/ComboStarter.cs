using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboStarter : MonoBehaviour
{
    private StateMachine meleeStateMachine;
    private InputManager myInput;
    public float DashTimer = 3;

    //public Collider2D hitbox;
    public HitBoxType hitType;
    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        myInput = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (InputManager._lightAttack.WasPressedThisFrame() && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new LightAttackEntryState());
        }
        if(InputManager._heavyAttack.WasPressedThisFrame() && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            //Debug.Log("meaty thwack");
            meleeStateMachine.SetNextState(new HeavyAttackEntryState());
        }
        if (InputManager._magicPrep.WasPressedThisFrame() && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new MagicNoPreparationState());
        }
        if (InputManager._dash.WasPressedThisFrame() && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            if(DashTimer >=3)
            {
                meleeStateMachine.SetNextState(new DashState());
                DashTimer = 0;
            }
            
        }
        if(DashTimer<3)
        {
            DashTimer += Time.deltaTime;
        }
    }
}
