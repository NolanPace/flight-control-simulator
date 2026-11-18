#include "flight.hpp"
#include <cassert>
#include <cmath>
#include <iostream>
int main(){
 PID pid{.12,.015,.06};Aircraft a;
 for(int i=0;i<2000;++i){double c=pid.step(10-a.pitchDegrees,.01);a.step(c,.01);}
 assert(std::abs(a.pitchDegrees-10)<1);
 assert(std::abs(a.rateDegreesPerSecond)<2);
 PID limited{10,0,0};assert(limited.step(100,.01)==1);
 bool failed=false;try{a.step(0,-1);}catch(...){failed=true;}assert(failed);
 std::cout<<"Control tests passed\n";
}