using UnityEngine;

public abstract class SC_BaseEffect : MonoBehaviour
{
    public virtual void EffectEndEvent()
    {
        Destroy(gameObject);
    }
}
