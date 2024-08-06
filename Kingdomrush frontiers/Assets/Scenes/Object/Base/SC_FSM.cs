using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SC_FSM : MonoBehaviour
{
    public class State
    {
        public int EnumValue;
        public System.Action Start;
        public System.Action Update;
        public System.Action End;
    };

    // Update is called once per frame
    public void Update()
    {
        if (CurState == null)
        {
            Debug.LogAssertion("CurState Is null. Use ChangeState Before Update FSM.");
            return;
        }

        CurState.Update();
    }

    public void CreateState(int Value, System.Action StateStart, System.Action StateUpdate, System.Action StateEnd)
    {
        if (FindState(Value) != null)
        {
            return;
        }

        AllState.Add(Value, new SC_FSM.State());
        SC_FSM.State Temp = AllState[Value];
        Temp.EnumValue = Value;
        Temp.Start = StateStart;
        Temp.Update = StateUpdate;
        Temp.End = StateEnd;
    }
    public void CreateState<EnumT>(EnumT Value, System.Action StateStart, System.Action StateUpdate, System.Action StateEnd)
    {
        CreateState(Convert.ToInt32(Value), StateStart, StateUpdate, StateEnd);
    }

    public void ChangeState(int Value)
    {
        if (null != CurState)
        {
            if (null != CurState.End)
            {
                CurState.End();
            }
        }

        CurState = FindState(Value);

        if (CurState == null)
        {
            return;
        }

        CurState.Start();
    }
    public void ChangeState<EnumT>(EnumT Value)
    {
        ChangeState(Convert.ToInt32(Value));
    }


    private State FindState(int Value)
    {
        if (AllState.ContainsKey(Value))
        {
            return AllState[Value];
        }

        return null;
    }

    public int GetCurState()
    {
        return CurState.EnumValue;
    }
    public EnumT GetCurState<EnumT>()
    {
        if (CurState.EnumValue is EnumT variable)
        {
            return variable;
        }

        throw new InvalidCastException($"Cannot cast {CurState.EnumValue} to {typeof(EnumT)}");
    }

    private Dictionary<int, State> AllState = new Dictionary<int, State>();
    private State CurState;
}
