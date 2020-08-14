using UnityEngine;
 using System.Collections;
 
 public class ScreenWrapController : MonoBehaviour 
 {
  public float leftConstraint = 0.0f;
  public float rightConstraint = 0.0f;
  public float buffer = 1.0f; 
  private float distanceZ = 10.0f;
 
  void Awake() 
  {
      leftConstraint = Camera.main.ScreenToWorldPoint( new Vector3(0.0f, 0.0f, distanceZ) ).x;
      rightConstraint = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, 0.0f, distanceZ) ).x;
      distanceZ = transform.position.z + 10f;
  }
 
  void Update() 
  {
      if (transform.position.x < leftConstraint - buffer) {
          transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
      }
  }
 }