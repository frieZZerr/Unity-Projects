using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class XelisMenuController : MonoBehaviour
{
    public Material body, body1, hair;
    public GameObject book;
    Animator animator;

    public float animTimer, dissTimer;
    private int triggerCount;
    private bool doOnce, doOnceMore;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animTimer = 0f;
        dissTimer = 0f;
        triggerCount = 0;
        doOnce = true;
        doOnceMore = true;
        book.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        animTimer += Time.deltaTime;
        dissTimer += Time.deltaTime;

        if (animTimer >= 20f)
        {
            triggerCount++;

            switch(triggerCount)
            {
                case 1:
                    animator.SetTrigger("Trigger1");
                    book.SetActive(true);
                    break;

                case 2:
                    animator.SetTrigger("Trigger2");
                    book.SetActive(false);
                    break;

                case 3:
                    animator.SetTrigger("Trigger3");
                    triggerCount = 0;
                    break;
            }

            animTimer = 0f;
            dissTimer = 0f;
            doOnce = true;
            doOnceMore = true;
        }

        if (dissTimer >= 4f && doOnce)
        {
            animator.SetTrigger("canDissolve");
            doOnce = false;
        }

        if (dissTimer >= 16f && doOnceMore)
        {
            animator.SetTrigger("canDissolve");
            doOnceMore = false;
        }
    }
}
