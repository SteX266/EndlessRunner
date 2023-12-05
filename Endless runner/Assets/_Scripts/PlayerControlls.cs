using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WiimoteApi;

public class PlayerControlls : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 direction;

    public float forwardSpeed;
    public float maxSpeed;

    private int currentLane = 1;
    public float laneDistance = 4;

    public float jumpDistance;

    public float gravity = -20;


    private Wiimote wiimote;
    public WiimoteModel model;
    private Vector3 wmpOffset = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!WiimoteManager.HasWiimote())
        {
            if (forwardSpeed < maxSpeed)
            {
                forwardSpeed += 0.1f * Time.deltaTime;

            }


            direction.z = forwardSpeed;
            direction.y += gravity * Time.deltaTime;

            if (characterController.isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }




            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentLane != 2)
                {
                    currentLane++;
                }

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentLane != 0)
                {
                    currentLane--;
                }
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (currentLane == 0)
            {
                targetPosition += Vector3.left * laneDistance;
            }

            else if (currentLane == 2)
            {
                targetPosition += Vector3.right * laneDistance;
            }


            if (transform.position == targetPosition)
            {
                return;
            }
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                characterController.Move(moveDir);
            }
            else
            {
                characterController.Move(diff);
            }

            //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
            //characterController.center = characterController.center;
        }

        else
        {
            wiimote = WiimoteManager.Wiimotes[0];

            int ret;

            do
            {
                ret = wiimote.ReadWiimoteData();

                if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS)
                {
                    Vector3 offset = new Vector3(-wiimote.MotionPlus.PitchSpeed,
                                                    wiimote.MotionPlus.YawSpeed,
                                                    wiimote.MotionPlus.RollSpeed) / 95f; // Divide by 95Hz (average updates per second from wiimote)
                    wmpOffset += offset;

                    model.rot.Rotate(offset, Space.Self);
                }
            } while (ret > 0);



            model.d_left.enabled = wiimote.Button.d_left;
            model.d_right.enabled = wiimote.Button.d_right;
            model.d_up.enabled = wiimote.Button.d_up;






            if (forwardSpeed < maxSpeed)
            {
                forwardSpeed += 0.1f * Time.deltaTime;

            }


            direction.z = forwardSpeed;
            direction.y += gravity * Time.deltaTime;

            if (characterController.isGrounded && model.d_up.enabled)
            {
                Jump();
            }




            if (model.d_right.enabled)
            {
                if (currentLane != 2)
                {
                    currentLane++;
                }

            }
            if (model.d_left.enabled)
            {
                if (currentLane != 0)
                {
                    currentLane--;
                }
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (currentLane == 0)
            {
                targetPosition += Vector3.left * laneDistance;
            }

            else if (currentLane == 2)
            {
                targetPosition += Vector3.right * laneDistance;
            }


            if (transform.position == targetPosition)
            {
                return;
            }
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                characterController.Move(moveDir);
            }
            else
            {
                characterController.Move(diff);
            }



        }



    }

    private void FixedUpdate()
    {
        characterController.Move(direction * Time.fixedDeltaTime);

    }

    private void Jump()
    {
        direction.y = jumpDistance;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }


    [System.Serializable]
    public class WiimoteModel
    {
        public Transform rot;
        public Renderer a;
        public Renderer b;
        public Renderer one;
        public Renderer two;
        public Renderer d_up;
        public Renderer d_down;
        public Renderer d_left;
        public Renderer d_right;
        public Renderer plus;
        public Renderer minus;
        public Renderer home;
    }

}
