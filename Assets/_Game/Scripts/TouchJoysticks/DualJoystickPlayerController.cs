using UnityEngine;

public class DualJoystickPlayerController : MonoBehaviour
{
    public PlayerController player;

    [Space()]
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script
    //public float moveSpeed = 6.0f; // movement speed of the player character
    public int rotationSpeed = 8; // rotation speed of the player character
    public Transform rotationTarget; // the game object that will rotate to face the input direction    
    public Vector3 leftJoystickInput; // holds the input of the Left Joystick
    public Vector3 rightJoystickInput; // hold the input of the Right Joystick    

    void Start()
    {
        if (leftJoystick == null)
        {
            Debug.LogError("The left joystick is not attached.");
        }

        if (rightJoystick == null)
        {
            Debug.LogError("The right joystick is not attached.");
        }

        if (rotationTarget == null)
        {
            Debug.LogError("The target rotation game object is not attached.");
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

        // if there is no input on the left joystick
        if (leftJoystickInput == Vector3.zero)
        {            
            player.SetMovementAnim(0);
        }
        // if there is no input on the right joystick
        if (rightJoystickInput == Vector3.zero)
        {
            
        }

        if (rightJoystickInput.magnitude > 0.75f)
        {
            player.Shoot();
        }

        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, 0, zMovementLeftJoystick).normalized;
            //leftJoystickInput = transform.TransformDirection(leftJoystickInput);
            //leftJoystickInput *= moveSpeed;

            // rotate the player to face the direction of input
            Vector3 temp = transform.position;
            temp.x += xMovementLeftJoystick;
            temp.z += zMovementLeftJoystick;
            Vector3 lookDirection = temp - transform.position;
            if (lookDirection != Vector3.zero)
            {
                rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
            }
            
            player.SetMovementAnim(lookDirection.magnitude + 0.2f);

            // move the player
            player.MoveToDirection(leftJoystickInput);            
        }

        // if there is only input from the right joystick
        if (leftJoystickInput == Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngle = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
            xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            // rotate the player to face the direction of input
            Vector3 temp = transform.position;
            temp.x += xMovementRightJoystick;
            temp.z += zMovementRightJoystick;
            Vector3 lookDirection = temp - transform.position;

            if (lookDirection != Vector3.zero)
            {
                rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 45f, 0), rotationSpeed * Time.deltaTime);
            }
        }

        // if there is input from both joysticks (Left And Right)
        if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngleInputRightJoystick = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
            xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngleInputRightJoystick));
            zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngleInputRightJoystick));

            // rotate the player to face the direction of input
            Vector3 temp = transform.position;
            temp.x += xMovementRightJoystick;
            temp.z += zMovementRightJoystick;
            Vector3 lookDirection = temp - transform.position;

            if (lookDirection != Vector3.zero)
            {
                rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 45f, 0), rotationSpeed * Time.deltaTime);
            }

            // calculate the player's direction based on angle
            float tempAngleLeftJoystick = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngleLeftJoystick));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngleLeftJoystick));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, 0, zMovementLeftJoystick).normalized;
            //leftJoystickInput = transform.TransformDirection(leftJoystickInput).normalized;
            //leftJoystickInput = moveSpeed;

            // move the player
            player.MoveToDirection(leftJoystickInput);            
        }
    }
}