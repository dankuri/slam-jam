using System;
using Godot;

namespace SlamJam;

public class Bullet : RigidBody2D
{
    public override void _Ready()
    {
        LinearVelocity = Vector2.Right.Rotated(Rotation) * 1000;
        Connect("body_entered", this, nameof(_OnBodyEntered));
    }

    private void _OnBodyEntered(Node body)
    {
        if (body is not IDamageable damageable) return;
        
        damageable.TakeDamage(35);
        QueueFree();
    }
}
