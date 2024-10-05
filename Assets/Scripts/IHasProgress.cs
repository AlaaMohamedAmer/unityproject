using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgressChangeEventArges> OnProgressChange;
    public class OnProgressChangeEventArges : EventArgs
    {
        public float progressNormalized;
    }

}
