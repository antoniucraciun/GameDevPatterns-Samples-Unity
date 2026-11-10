# Fixed Timestep: without the pattern

Scene: `FixedTimestepWithout.unity`

| Script | On | What it does |
| --- | --- | --- |
| `Thruster` | Rocket | Reads Space and calls `AddForce` in `Update`. |
| `Jumper` | Jumper | Simulates a jump by hand, one step per frame of size `Time.deltaTime`. |
| `FixedTimestepHud` | Hud | Shows the numbers and switches the frame rate. |

This is the code you would reasonably write first, and it already uses `deltaTime`, so it looks frame-rate independent. It isn't quite:

- **Rocket:** Unity sums every `AddForce` made between two physics steps and applies it in the next step. More frames per second means more pushes per step, so the rocket climbs several times faster at 144 FPS than at 30.
- **Jumper:** each frame is one integration step of a different size. The peak height depends on the frame rate, and changes a little from jump to jump as frame times vary.

## Scene setup

- **Ground:** a Plane at the origin, scaled (2, 1, 1).
- **Rocket:** a Cube at (-2, 0.5, 0) with a Rigidbody (mass 1, Interpolate None, Constraints: freeze position X and Z, freeze rotation X, Y and Z) and `Thruster`.
- **Jumper:** a Sphere at (2, 0.5, 0) with its collider removed, and `Jumper`.
- **Hud:** an empty object with `FixedTimestepHud`, its Jumper and Rocket fields set.
- **Main Camera** at (0, 3, -12), looking straight ahead, plus the default Directional Light.

Compare it with [WithPattern](../WithPattern/README.md): same scene, but both objects behave the same at every frame rate.

Status: implemented and checked in Play Mode in Unity 6000.6.8f1.
