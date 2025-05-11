using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static movement;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    
    Vector3 currentvelocity;
    public float distance;
    
    // Start is called before the first frame update
      void Start()
      {
          if (offset == Vector3.zero && Player != null)
          {
              offset = transform.position - Player.position;
          }
      }
      void LateUpdate()
      {
          // Ensure the player reference is assigned
          if (Player != null)
          {
            // Update the camera's position to follow the player with the offset
            //   transform.position = Player.position + offset;
            Vector3 target = Player.position + offset.normalized * distance;
            transform.position = Vector3.SmoothDamp(transform.position, target+offset, ref currentvelocity, smoothTime);
            transform.LookAt(Player);
        }
    
      }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
