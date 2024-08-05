using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

sealed public class SC_MagicBullet : SC_DirectBullet
{
    override protected void Awake()
    {
        base.Awake();
        //애니메이션 세팅
        if (Animators.Count == 0)
        {
            Animators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Bullet/AC_MagicBullet.overrideController", typeof(AnimatorOverrideController)));
            Animators.Add((AnimatorOverrideController)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Object/Stage/Tower/MagicTower/AnimationClip/Bullet/AC_ArcMageBullet.overrideController", typeof(AnimatorOverrideController)));
        }

        BulletAnimator = gameObject.GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        if (Data.Level == 4)
        {
            BulletAnimator.runtimeAnimatorController = Animators[1];
        }
        else
        {
            BulletAnimator.runtimeAnimatorController = Animators[0];
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Collider[] Hits = Physics.OverlapSphere(transform.position, MagicBoltColScale, BulletLayerMask);

        //Hit
        if (Hits.Length >= 1)
        {
            Hits[0].gameObject.GetComponent<SC_Monster3DCol>().ParentMonster.TakeDamage(CalDamage());
            BulletAnimator.SetBool("IsDeath", true);
            enabled = false;
            return;
        }

        //Miss
        if (Ratio > 1.0f)
        {
            BulletAnimator.SetBool("IsDeath", true);
            enabled = false;
        }
    }

    public void DeathEffectEndEvent()
    {
        Destroy(gameObject);
    }

    Animator BulletAnimator;
    static private readonly float MagicBoltColScale = 0.04f;
    static private readonly List<AnimatorOverrideController> Animators = new List<AnimatorOverrideController>();
}
