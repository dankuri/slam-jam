using System;
using Godot;

namespace SlamJam;

public class Enemy : RigidBody2D, IDamageable
{
    private int _health = 100;
    
    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health <= 0) QueueFree();
    }
}
