using System;
using Godot;
using Common;

namespace SlamJam;

public class Soldier : KinematicBody2D
{
    [Export] public int Speed = 200;

    private AnimatedSprite _animatedSprite;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("Sprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(InputActions.ProduceMovement(Speed));
        LookAt(GetGlobalMousePosition());
    }
}
