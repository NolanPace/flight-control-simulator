#include "flight.hpp"
#include <algorithm>
#include <stdexcept>
#include <cmath>
double PID::step(double error,double dt){
 if(!(dt>0)||!std::isfinite(error))throw std::invalid_argument("Invalid controller input");
 integral=std::clamp(integral+error*dt,-20.0,20.0);
 double derivative=(error-previousError)/dt;previousError=error;
 return std::clamp(kp*error+ki*integral+kd*derivative,-1.0,1.0);
}
void Aircraft::step(double command,double dt,double disturbance){
 if(!(dt>0)||!std::isfinite(command))throw std::invalid_argument("Invalid dynamics input");
 rateDegreesPerSecond+=(torqueGain*std::clamp(command,-1.0,1.0)-damping*rateDegreesPerSecond+disturbance)*dt;
 pitchDegrees+=rateDegreesPerSecond*dt;
}
