namespace SlamJam;

using System;
using Common;
using Godot;

public class Example : Node
{
	public override void _Ready()
	{
		Console.WriteLine(Say.hello("C#"));
	}
}
