using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
public class FifthState : State<AI>
{
    private static FifthState _instance;

    private FifthState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FifthState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FifthState();
            }

            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        //_owner.TurnRight();
        _owner.Simulation_uActive2();
        // Debug.Log("Entering ForthState");
    }
    public override void ExitState(AI _owner)
    {
        // Debug.Log("Exiting ForthState");
    }
    public override void UpdateState(AI _owner)
    {
        if ((int)_owner.state == 0)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }/*
        if ((int)_owner.state == 2)
        {
            _owner.stateMachine.ChangeState(ThirdState.Instance);
        }
        if ((int)_owner.state == 3)
        {
            return;
        }
        if ((int)_owner.state == 1)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
       */
    }
}
