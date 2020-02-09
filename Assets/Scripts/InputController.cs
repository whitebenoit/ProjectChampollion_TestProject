using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;


    public delegate void InputCallback();
    public delegate void MovementCallback(Vector2 movementInput);
    public List<MovementCallback> movemementCallbacks = new List<MovementCallback>();
    public List<InputCallback> spawningCallbacks = new List<InputCallback>();
    public List<InputCallback> loadingCallbacks = new List<InputCallback>();

    private Camera camera;

    private void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        Vector2 movementInput = new Vector2(forward, right);
        foreach (MovementCallback callback in movemementCallbacks)
            callback(movementInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (InputCallback callback in spawningCallbacks)
                callback();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (InputCallback callback in loadingCallbacks)
                callback();
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
