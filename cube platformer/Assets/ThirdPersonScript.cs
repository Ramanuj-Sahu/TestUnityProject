using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationspeed;

    public Transform combatLookat;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject  TopdownCam;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
     
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraStyle(CameraStyle.Basic);    

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraStyle(CameraStyle.Combat);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraStyle(CameraStyle.Topdown);

        }

        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, rotationspeed * Time.deltaTime);
            }
        }
        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookat.position - new Vector3(transform.position.x, combatLookat.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            player.forward = dirToCombatLookAt.normalized;

        }


    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        TopdownCam.SetActive(false);

        if(newStyle== CameraStyle.Basic)
        {
            thirdPersonCam.SetActive(true);
        }
        if (newStyle == CameraStyle.Topdown)
        {
            TopdownCam.SetActive(true);
        }
        if (newStyle == CameraStyle.Combat)
        {
            combatCam.SetActive(true);
        }

        currentStyle = newStyle;
    }
}
