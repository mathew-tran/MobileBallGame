using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    JoyButton joyButton;

    
    public float charge = 0;
    public float chargeRate = 5;
    public float maxCharge = 100;

    Vector3 target;
    public float power = 10f;

    public delegate void OnChargeChangeEvent();
    public event OnChargeChangeEvent OnChargeChange;

    public delegate void OnTargetingEvent();
    public event OnTargetingEvent OnTarget;

    public GameManager managerReference;

    public bool canMove = true;
    public bool canVibrate = true;

    public AudioSource clickSound;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joystick.OnClick += () =>
        {
            PlaySound();
        };
        joyButton = FindObjectOfType<JoyButton>();
        managerReference = FindObjectOfType<GameManager>();

        managerReference.OnGameEnd += OnGameEnd;
    }

    public void PlaySound()
    {
        AudioManager._instance.PlaySFX(clickSound.clip);
    }

    public void OnGameEnd()
    {
        if (canVibrate)
        {
            Handheld.Vibrate();
        }
        canMove = false;
        joystick.gameObject.SetActive(false);
    }
    // Update is called once per frame
    public void RetainPlayerInput()
    {
        var newTarget = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        
        if(newTarget.x == 0 && newTarget.y == 0)
        {

        }
        else
        {
            target = newTarget;
        }
    }
    void Update()
    {
        RetainPlayerInput();
        if (canMove)
        {
            if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            {
                if (charge < maxCharge)
                {
                    charge += chargeRate;
                    OnChargeChange();
                }
            }


            if (Input.GetMouseButton(0))
            {
                target = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                OnTarget();
            }
        }       

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var rb = GetComponent<Rigidbody>();

            rb.AddForce(target * charge * power);
            target = Vector3.zero;            
        }

        if (joystick.Horizontal == 0f && joystick.Vertical == 0f)
        {
            charge = 0;
            OnChargeChange();
            OnTarget();
        }
    }
}
