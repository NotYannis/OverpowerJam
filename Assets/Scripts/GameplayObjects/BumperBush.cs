using UnityEngine;

public class BumperBush : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationFalse()
    {
        animator.SetBool("Bumped", false);
    }
}
