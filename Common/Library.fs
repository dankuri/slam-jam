namespace Common

open Godot
open System.Linq
open Utils

module InputActions =
    let private movementOffsets =
        dict
            [ ("move_up", Vector2.Up)
              ("move_left", Vector2.Left)
              ("move_down", Vector2.Down)
              ("move_right", Vector2.Right) ]

    let private movementKeys = movementOffsets.Keys |> List.ofSeq

    let ProduceVelocity (atSpeed: float32) =
        movementKeys
        |> List.filter Input.IsActionPressed
        |> List.fold (fun s a -> s + movementOffsets[a]) Vector2.Zero
        |> Vector2.normalize
        |> (*) atSpeed
