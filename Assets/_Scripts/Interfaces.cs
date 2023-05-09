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
    public abstract void Gather(HitInfo hitInfo);
}

public interface IPickupable
{
    public abstract void PickUp();
}

public class HitInfo
{
    public float amount { get; set; }
    public RaycastHit hit { get; set; }
    public Collision collision { get; set; }

    public HitInfo(float amount, RaycastHit hit)
    {
        this.amount = amount;
        this.hit = hit;
    }

    public HitInfo(float amount, Collision collision)
    {
        this.amount = amount;
        this.collision = collision;
    }
}
