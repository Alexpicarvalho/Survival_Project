using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces
{
}

public interface IDamageable
{
    public abstract void TakeDamage(float amount);
    public Transform GetTransform();
}

public interface IGatherable
{
    public abstract void Gather();
}

public interface IPickupable
{
    public abstract void PickUp();
}
