using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableBase : MonoBehaviour
{
    protected int uses;
    protected int usesLeft;
    protected GameObject itemAfterUse;
    public abstract void Use();
}
