using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private float coolDown;
    private float currentTime;

    public int attackCounter = 0;

    private float lastTimeAttack = 0.0f;
    private float timeToAddCombo = 0.5f;


    public bool isPunching = false;


    public List<KeyCode> inputBuffer = new List<KeyCode>();

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        if (Input.GetKeyDown(KeyCode.L)) // Solo para testear.
        {
            inputBuffer.Clear();
        }
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPunching)
            {
                ChecKAttackCounter();
            }
            else
            {
                addToInputBuffer();
            }
        }
    }


    public void SetPunchToFalse()
    {
        isPunching = false;
        attackCounter = 0;
        animator.SetInteger("attackCounter", attackCounter);
        animator.SetBool("isPunching", isPunching);

        Debug.Log("NO Golpeo");
    }

    public void SetPunchToTrue()
    {
        isPunching = true;

        attackCounter++;
        animator.SetInteger("attackCounter", attackCounter);


        animator.SetBool("isPunching", isPunching);

        Debug.Log("Golpeo");
    }


    public void CheckInputBuffer()
    {
        Debug.Log("CheckInputBuffer");
        if (inputBuffer.Count > 0)
        {
            ChecKAttackCounter();
            inputBuffer.Clear();
        }
        else
        {
            SetPunchToFalse();
            inputBuffer.Clear();
        }
    }

    public void addToInputBuffer()
    {
        if (isPunching && inputBuffer.Count == 0 && attackCounter < 3)
            inputBuffer.Add(KeyCode.Mouse0);
    }

    public void ChecKAttackCounter()
    {
        if (attackCounter < 3)
        {
            SetPunchToTrue();
        }
        else
        {
            SetPunchToFalse();
        }
    }


    //Sistema de combos sin las animaciones.

    // void Combos(int index)
    // {
    //     if (index < hitboxes.Length)
    //     {
    //         DisableHitboxesExcept(hitboxes[index]);
    //         hitboxes[index].SetActive(true);
    //     }
    // }
    //
    //
    //
    // void CheckAttackTimer()
    // {
    //     if ((Time.time - lastTimeAttack) > timeToAddCombo) //Reseteo de contador de combo
    //     {
    //         attackCounter = 0;
    //         lastTimeAttack = Time.time;
    //     }
    //     else
    //     {
    //         // Debug.Log("Se agrega combo");
    //         attackCounter++;
    //     }
    //
    //     if (attackCounter >= hitboxes.Length)
    //         attackCounter = 0;
    //     lastTimeAttack = Time.time;
    //     Combos(attackCounter);
    // }
}