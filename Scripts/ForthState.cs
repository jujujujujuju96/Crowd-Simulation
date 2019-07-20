using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
public class ForthState : State<AI>
{
    private static ForthState _instance;

    private ForthState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ForthState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ForthState();
            }

            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        //_owner.TurnRight();
        _owner.Simulation_uActive();
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
        }
        if ((int)_owner.state == 4)
        {
            _owner.stateMachine.ChangeState(FifthState.Instance);
        }
        if ((int)_owner.state == 2)
        {
            _owner.stateMachine.ChangeState(ThirdState.Instance);
        }
            /*
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
