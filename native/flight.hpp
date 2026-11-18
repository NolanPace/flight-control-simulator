#pragma once
struct PID {
 double kp,ki,kd;
 double integral=0,previousError=0;
 double step(double error,double dt);
};
struct Aircraft {
 double pitchDegrees=0;
 double rateDegreesPerSecond=0;
 double damping=.8,torqueGain=24;
 void step(double command,double dt,double disturbance=0);
};
