using System;
using Godot;
using Common;

namespace SlamJam;

public class Soldier : KinematicBody2D
{
    [Export] public int Speed = 200;
    [Export] public int RotationSpeed = 10;
    [Export] public PackedScene Bullet;

    private AnimatedSprite _sprite;
    private Position2D _gunTip;

    private float _shootCooldown;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite>("Sprite");
        _gunTip = GetNode<Position2D>("GunTip");
    }

    public override void _Process(float delta)
    {
        if (_shootCooldown > 0f) _shootCooldown -= delta;
        else _shootCooldown = 0f;
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(InputActions.ProduceVelocity(Speed));
        _LookAtCursor(delta);
    }

    public override void _UnhandledInput(InputEvent e)
    {
        if (Input.IsActionJustPressed("fire")) _Shoot();
    }

    private void _LookAtCursor(float delta)
    {
        GlobalRotation = Mathf.LerpAngle(GlobalRotation,
            (GetGlobalMousePosition() - GlobalPosition).Normalized().Angle(),
            delta * RotationSpeed
        );
    }

    private void _Shoot()
    {
        if (_shootCooldown != 0f) return;

        _SpawnBullet();
        _shootCooldown = 1f;
    }

    private void _SpawnBullet()
    {
        var bullet = Bullet.Instance<RigidBody2D>();
        bullet.Position = _gunTip.GlobalPosition;
        bullet.Rotation = _gunTip.GlobalRotation;

        GetNode<Node2D>("/root/ExampleScene").AddChild(bullet);
    }
}
