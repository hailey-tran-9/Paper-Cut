using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderAnim : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyShooting es;

    // Start is called before the first frame update
    private bool getCanFire()
    {
        return es.canFire();
    }

    // Update is called once per frame
    private void Update()
    {
        if (getCanFire())
        {
            animator.SetBool("Fire", true);
        }
        else
        {
            animator.SetBool("Fire", false);
        }
    }
}
