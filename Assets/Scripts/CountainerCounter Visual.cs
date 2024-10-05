using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter ContainerCounter;

    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        ContainerCounter.OnPlayerGrabbedobject += ContainerCounter_OnPlayerGrabbedobject;
    }

    private void ContainerCounter_OnPlayerGrabbedobject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
