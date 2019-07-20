using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateStuff;

public class FirstState : State<AI>
{
    private static FirstState _instance;

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        _owner.Simulation_Idle();
        //Debug.Log("Entering First State");
    }

    public override void ExitState(AI _owner)
    {
        //Debug.Log("Exiting First State");
    }

    public override void UpdateState(AI _owner)
    {
        if ((int)_owner.state==1)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
        if ((int)_owner.state == 2)
        {
            _owner.stateMachine.ChangeState(ThirdState.Instance);
        }
        //if ((int)_owner.state == 3)
        //{
        //    _owner.stateMachine.ChangeState(ForthState.Instance);
       // }
        if ((int)_owner.state == 0)
        {
            return;
        }
        
    }
}