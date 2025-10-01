using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

using UnityEngine.UI;

using System;
using UnityEngine.SceneManagement;

using System.Security.Permissions;
using System.Diagnostics;



public class Player : MonoBehaviour
{
    #region Variables



    Vector3 curPos;

    private float t_adjustedSpeed;
    public float originalSpeed;
    private float speed;

    public float sprintModifier;
    public float jumpForce;
    public int max_health;

    public Camera normalCam;

    public Transform groundDetector;
    public LayerMask ground;
    public float zoom;

    public Rigidbody rig;



    private float movementCounter;
    private float idleCounter;

    private float baseFOV;
    public float sprintFOVModifier;

    public float current_health;



    public AudioClip jump;

    public GameObject hackUI;

    public Transform[] worlds;
    public int currentWorld = 0;

    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {


        current_health = max_health;




        baseFOV = normalCam.fieldOfView;

        //if (Camera.main) Camera.main.enabled = false;










    }

    public void Awake()
    {




    }

    private void Update()
    {

        //Axis
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");


        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftControl);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool pause = Input.GetKeyDown("j");
       



        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;






        //Jumping
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);



        }

        bool changeWorld = Input.GetKeyDown(KeyCode.Q);

        if (changeWorld)
        {
            if (currentWorld == 0)
            {
                this.transform.position = worlds[1].position;
                currentWorld = 1;
            }
            else if (currentWorld == 1)
            {
                this.transform.position = worlds[0].position;
                currentWorld = 0;
            }
        }

    }







    void FixedUpdate()
    {


        //Axis

        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");




        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool aim = Input.GetMouseButton(1);


        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0;
        bool isAiming = aim;
        bool hacking = Input.GetKey(KeyCode.Tab);

       


        //Movement


        Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
        t_direction.Normalize();

        t_adjustedSpeed = originalSpeed;
        if (isSprinting) t_adjustedSpeed *= sprintModifier;



        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.fixedDeltaTime;
        t_targetVelocity.y = rig.linearVelocity.y;
        rig.linearVelocity = t_targetVelocity;


        //FOV
        if (isSprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.fixedDeltaTime * 8f); }
        else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.fixedDeltaTime * 8f); }

        if (hacking) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * zoom, Time.fixedDeltaTime * 8f); }

        else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.fixedDeltaTime * 8f); }


        //UI
        hackUI.SetActive(hacking);

        
        


    }

    #endregion

    #region Private Methods




    #endregion

    #region Public Methods
























    #endregion
}
