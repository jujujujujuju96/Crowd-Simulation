using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateStuff;

public class ThirdState : State<AI>
{
    private static ThirdState _instance;

    private ThirdState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThirdState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThirdState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
       // _owner.TurnLeft();
        _owner.Simulation_Active();
        //Debug.Log("Entering ThirdState");
    }

    public override void ExitState(AI _owner)
    {
        //_owner.EndSimulation_Active();
        //Debug.Log("Exiting ThirdState");
    }

    public override void UpdateState(AI _owner)
    {
        if ((int)_owner.state == 0)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
        if ((int)_owner.state == 2)
        {
            return;
        }
        if ((int)_owner.state == 3)
        {
            _owner.stateMachine.ChangeState(ForthState.Instance);
        }
        /*if ((int)_owner.state == 1)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }*/
    }
}