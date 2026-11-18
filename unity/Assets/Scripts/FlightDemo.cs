using UnityEngine;
public class FlightDemo : MonoBehaviour {
 public float targetPitch=12f;
 private float pitch,rate,integral,lastError;
 private Transform aircraft;
 void Start(){
  Camera cam=Camera.main;
  if(cam==null){GameObject cameraObj=new("Main Camera");cam=cameraObj.AddComponent<Camera>();cameraObj.tag="MainCamera";}
  cam.transform.position=new Vector3(0,5,-14);
  cam.transform.LookAt(Vector3.zero);cam.backgroundColor=new Color(.07f,.13f,.2f);
  var light=new GameObject("Sun").AddComponent<Light>();light.type=LightType.Directional;light.intensity=1.2f;light.transform.rotation=Quaternion.Euler(35,-40,0);
  var fuselage=GameObject.CreatePrimitive(PrimitiveType.Capsule);fuselage.name="Aircraft";aircraft=fuselage.transform;aircraft.localScale=new Vector3(.75f,2f,.75f);aircraft.rotation=Quaternion.Euler(90,0,0);
  var wing=GameObject.CreatePrimitive(PrimitiveType.Cube);wing.name="Wings";wing.transform.SetParent(aircraft);wing.transform.localPosition=Vector3.zero;wing.transform.localScale=new Vector3(5,.10f,.5f);
  var tail=GameObject.CreatePrimitive(PrimitiveType.Cube);tail.name="Tailplane";tail.transform.SetParent(aircraft);tail.transform.localPosition=new Vector3(0,-.8f,0);tail.transform.localScale=new Vector3(2,.08f,.5f);
  var ground=GameObject.CreatePrimitive(PrimitiveType.Plane);ground.transform.position=new Vector3(0,-3,0);ground.transform.localScale=Vector3.one*2;
 }
 void Update(){
  if(Input.GetKey(KeyCode.UpArrow))targetPitch+=20*Time.deltaTime;
  if(Input.GetKey(KeyCode.DownArrow))targetPitch-=20*Time.deltaTime;
  float dt=Mathf.Min(Time.deltaTime,.05f);
  if(dt<=0)return;
  float error=targetPitch-pitch;
  integral=Mathf.Clamp(integral+error*dt,-20,20);
  float derivative=(error-lastError)/dt;lastError=error;
  float command=Mathf.Clamp(.12f*error+.015f*integral+.06f*derivative,-1,1);
  rate+=(24*command-.8f*rate)*dt;
  pitch+=rate*dt;
  aircraft.rotation=Quaternion.Euler(pitch,0,90);
 }
 void OnGUI(){
  GUI.color=Color.white;
  GUI.Label(new Rect(20,20,450,30),"FLIGHT CONTROL DEMO / PID PITCH AXIS");
  GUI.Label(new Rect(20,50,450,30),$"Target: {targetPitch:F1}°     Pitch: {pitch:F1}°");
  GUI.Label(new Rect(20,80,450,30),"UP / DOWN to adjust pitch target");
 }
}