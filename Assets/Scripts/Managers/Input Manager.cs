using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    public static Vector2 Movement;
    public static bool mylight;
    private PlayerInput _playerInput;
    private static InputAction _moveAction;
    public static InputAction _lightAttack;
    public static InputAction _heavyAttack;
    public static InputAction _magicPrep;
    public static InputAction _pause;
    //private InputAction _lightAttack;
    //private InputAction _heavyAttack;
    //public static InputManager instance;

    private void Awake()
    {
        //instance = this;
        //Debug.Log("Input Manager is Awake");
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lightAttack = _playerInput.actions["Light_Attack"];
        _heavyAttack = _playerInput.actions["Heavy_Attack"];
        _magicPrep = _playerInput.actions["Magic_Prep"];
        _pause = _playerInput.actions["Pause"];


    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        //Debug.Log(_lightAttack); 
    }

}
