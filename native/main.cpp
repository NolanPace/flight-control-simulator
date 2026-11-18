#include "flight.hpp"
#include <iostream>
#include <cstdlib>
#include <iomanip>
int main(int argc,char**argv){
 double target=argc>1?std::atof(argv[1]):12;
 double seconds=argc>2?std::atof(argv[2]):8;
 if(seconds<=0||seconds>120){std::cerr<<"Invalid simulation duration\n";return 2;}
 PID pid{.12,.015,.06};
 Aircraft aircraft;constexpr double dt=.01;
 std::cout<<"time_s,pitch_deg,rate_deg_s,command\n"<<std::setprecision(5);
 for(int i=0;i<seconds/dt;++i){
  double t=i*dt;
  double command=pid.step(target-aircraft.pitchDegrees,dt);
  aircraft.step(command,dt,(t>3.5&&t<4.2)?-3.0:0.0);
  if(i%10==0)std::cout<<t<<","<<aircraft.pitchDegrees<<","<<aircraft.rateDegreesPerSecond<<","<<command<<"\n";
 }
}