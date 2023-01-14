module Utils

open Godot

module String =
    let startsWith (v: string) (s: string) = s.StartsWith(v)

module Vector2 =
    let normalize (v: Vector2) = v.Normalized()
