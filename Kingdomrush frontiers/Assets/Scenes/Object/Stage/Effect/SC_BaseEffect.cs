using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public abstract class SC_BaseEffect : MonoBehaviour
{
    public virtual void EffectEndEvent()
    {
        Destroy(gameObject);
    }
}
