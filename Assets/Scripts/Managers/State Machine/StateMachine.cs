using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string customName;
    public CharacterStats entityStats;
    private State mainStateType;
    private State finalStateType;

    public State CurrentState {get; private set;}
    private State nextState;
    public State previousState { get; private set; }

    private void Awake()
    {
        //Debug.Log("I am awake and startin the State Machine");
        if (customName == "Combat")
        {
            mainStateType = new IdleCombatState();
            finalStateType = new DeathPlayerState();

        }
        if (customName == "Enemy")
        {
            mainStateType = new IdleEnemyState();
            finalStateType = new DeathEnemyState();
        }
        if (customName =="Boss")
        {
            mainStateType = new IdleBossState();
            finalStateType = new DeathBossState();
        }
        if (GetComponent<CharacterStats>())
        {
            entityStats = GetComponent<CharacterStats>();
        }

        SetNextStateToMain();
    }
    // Update is called once per frame
    void Update()
    {
        if(nextState!=null)
        {
            SetState(nextState);
            nextState = null;
        }
        if(CurrentState!= null)
        {
            CurrentState.OnUpdate();
        }
    }

    private void SetState(State _newState)
    {
        if(CurrentState!=null)
        {
            previousState = CurrentState;
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if(_newState!= null)
        {
            nextState = _newState;
        }
    }
    private void LateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLateUpdate();
        }
    }
    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnFixedUpdate();
        }
    }

    public void SetNextStateToMain()
    {
        //Debug.Log("Setting to main");
        nextState = mainStateType;
    }
    public void SetNextStateToFinal()
    {
       //Debug.Log("I'm deaddd");
        nextState = finalStateType;
    }
    /*
    private void OnValidate()
    {
        if(mainStateType == null)
        {
            if(customName == "Combat")
            {
                mainStateType = new IdleCombatState();
            }
        }
    }
    */
}
