# Flight Control Simulator

A **C++17 PID flight-attitude simulation** with an optional **Unity C# 3D visualization**. Demonstrates proportional/integral/derivative control, actuator saturation, simplified pitch dynamics, target setpoints, and disturbance rejection.

## Native C++ simulation
```bash
cmake -S native -B build
cmake --build build
ctest --test-dir build --output-on-failure
./build/flight_sim 12 8
```
Arguments: desired pitch in degrees and simulation length in seconds. Output is `time_s,pitch_deg,rate_deg_s,command` CSV, easily graphed.

## Unity visualization
Create a Unity 2022.3+ 3D project. Copy the files in `unity/Assets/Scripts` into `Assets/Scripts`. Add `FlightDemo` to an empty GameObject and press Play. It creates the demonstration aircraft, ground plane, camera and an attitude step-response visualization at runtime. Arrow Up/Down change commanded pitch. The Unity logic duplicates the C++ numerical model for a standalone demo; it does not invoke native code.

## Scope
A simplified **single-axis** rotational system: `pitchAcceleration = torqueGain * command - damping * rate + disturbance`. This is not an aerodynamic model, aircraft guidance system, or safe flight software. MIT licensed.
