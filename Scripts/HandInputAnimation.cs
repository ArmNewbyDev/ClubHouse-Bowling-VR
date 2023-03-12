using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandInputAnimation : MonoBehaviour
{
    public InputActionProperty triggerPress;
    public InputActionProperty gripPress;

    public Animator handAnimator;

    private static int _trigger = Animator.StringToHash("Trigger");
    private static int _grip = Animator.StringToHash("Grip");
    
    private void Update()
    {
        var triggerPressValue = triggerPress.action.ReadValue<float>();
        handAnimator.SetFloat(_trigger, triggerPressValue);

        var gripPressValue = gripPress.action.ReadValue<float>();
        handAnimator.SetFloat(_grip, gripPressValue);
    }
}
