using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    PlayerLocomotion playerLocomotion;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");

    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMovement == 1 && playerLocomotion.isSprinting)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement == 1)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement == -1 && playerLocomotion.isSprinting)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement == -1)
        {
            snappedHorizontal = 0.5f;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMovement == 1 && playerLocomotion.isSprinting)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement == 1)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement == -1 && playerLocomotion.isSprinting)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement == -1)
        {
            snappedVertical = 0.5f;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
