using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AnimationType
{
    Idle,
    Move,
    Attack
}
public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] bool isAnimationing;
    public bool IsAnimationing { get { return isAnimationing; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isAnimationing = false;    
    }

    public void setAnimation(AnimationType type)
    {
        if (isAnimationing)
            return;
        isAnimationing = true;
        StartCoroutine(PlayAnimation(type));
    }

    public void ChangeMovementValue(float cur,float max)
    {
        animator.SetFloat("MoveSpeed", cur / max);
    }

    IEnumerator PlayAnimation(AnimationType type)
    {
        string input = type.ToString();
        animator.CrossFade(input, 0.1f);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(input))
        {
            yield return null;
        }

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        float speed = animator.GetCurrentAnimatorStateInfo(0).speed;
        float adjustedLength = length / speed;

        yield return new WaitForSeconds(adjustedLength);

        isAnimationing = false;
    }
}
