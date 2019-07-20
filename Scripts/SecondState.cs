using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
public class SecondState : State<AI>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondState();
            }

            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        _owner.Simulation_uIdle();
        Debug.Log("Entering Second State");
    }
    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Second State");
    }
    public override void UpdateState(AI _owner)
    {/*
        if ((int)_owner.state == 0)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
        if ((int)_owner.state == 2)
        {
            _owner.stateMachine.ChangeState(ThirdState.Instance);
        }
        if ((int)_owner.state == 3)
        {
            _owner.stateMachine.ChangeState(ForthState.Instance);
        }
        if ((int)_owner.state == 1)
        {
            _owner.stateMachine.ChangeState(ForthState.Instance);
        }*/
    }
}
