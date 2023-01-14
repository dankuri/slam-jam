using Godot;
using Common;

namespace SlamJam;

public class Soldier : KinematicBody2D
{
    [Export] public int Speed = 200;
    [Export] public int RotationSpeed = 10;
    [Export] public int ClipSize = 10;
    [Export] public float FireRate = 0.5f;
    [Export] public PackedScene Bullet;

    private AnimatedSprite _sprite;
    private Position2D _gunTip;

    private float _shootCooldown;

    private Label _ammoIndicator;
    private int _ammo = 10;

    private int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = value;
            _ammoIndicator.Text = value > 0 ? $"Ammo: {_ammo}" : "Empty!";
        }
    }

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite>("Sprite");
        _sprite.Connect("animation_finished", this, nameof(_OnReloadEnd));
        _gunTip = GetNode<Position2D>("GunTip");
        _ammoIndicator = GetNode<Label>("/root/ExampleScene/UI/AmmoIndicator");
        _ammoIndicator.Text = _ammo.ToString();
    }

    public override void _Process(float delta)
    {
        if (_shootCooldown > 0f) _shootCooldown -= delta;
        else _shootCooldown = 0f;

        if (Input.IsActionPressed("fire")) _Shoot();
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(InputActions.ProduceVelocity(Speed));
        _LookAtCursor(delta);
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
        if (_ammo <= 0)
        {
            _Reload();
            return;
        }

        if (_shootCooldown != 0f) return;

        Ammo -= 1;
        _shootCooldown = FireRate;
        _SpawnBullet();
    }

    private void _Reload()
    {
        _ammoIndicator.Text = "Reloading...";
        _sprite.Play();
    }

    private void _OnReloadEnd()
    {
        Ammo = ClipSize;

        _sprite.Stop();
        _sprite.Frame = 0;
    }

    private void _SpawnBullet()
    {
        var bullet = Bullet.Instance<RigidBody2D>();
        bullet.Position = _gunTip.GlobalPosition;
        bullet.Rotation = _gunTip.GlobalRotation;

        GetNode<Node2D>("/root/ExampleScene").AddChild(bullet);
    }
}