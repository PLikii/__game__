using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Animation : MonoBehaviour
{
    //Animator
    Animator animator;
 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {

    IdleAnim();
    }

    public void IdleAnim()
    {
        GetComponent<UnityEngine.Animation>().CrossFade ("Character_anim_stend_test");
    }

    public void MoveAnim()
    {
        return;
    }

    protected void JumpAnim()
    {
        return;
    }
}
