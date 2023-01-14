namespace Common

open Godot
open System.Linq
open Utils

module InputActions =
    let private actionOffsets =
        dict
            [ ("move_up", Vector2.Up)
              ("move_left", Vector2.Left)
              ("move_down", Vector2.Down)
              ("move_right", Vector2.Right) ]

    let ProduceMovement (atSpeed: float32) =
        actionOffsets.Keys
         |> List.ofSeq
         |> List.filter Input.IsActionPressed
         |> List.fold (fun s a -> s + actionOffsets[a]) Vector2.Zero
         |> Vector2.normalize
         |> (*) atSpeed
