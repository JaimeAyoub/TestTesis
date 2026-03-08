using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackCounter = 0;
    public bool isPunching = false;
    public List<KeyCode> inputBuffer = new List<KeyCode>();
    public Animator animator;
    
    private float punchTimeout = 1.5f;
    private float punchTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Inputs();

        if (isPunching)
        {
            punchTimer += Time.deltaTime;
            if (punchTimer >= punchTimeout)
            {

                ForceReset();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
            inputBuffer.Clear();
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPunching)
                CheckAttackCounter();
            else
                AddToInputBuffer();
        }
    }

    public void SetPunchToFalse()
    {
        isPunching = false;
        attackCounter = 0;
        punchTimer = 0f; // ← Resetear timer
        animator.SetInteger("attackCounter", attackCounter);
        animator.SetBool("isPunching", false);
        Debug.Log("NO Golpeo");
    }

    public void SetPunchToTrue()
    {
        if (attackCounter >= 3)
        {
            SetPunchToFalse(); // ← En vez de solo "return", forzamos reset
            return;
        }

        isPunching = true;
        punchTimer = 0f; // ← Resetear timer en cada golpe nuevo
        attackCounter++;
        animator.SetInteger("attackCounter", attackCounter);
        animator.SetBool("isPunching", true);
        Debug.Log($"Golpeo {attackCounter}");
    }

    public void CheckInputBuffer()
    {
        Debug.Log($"CheckInputBuffer | buffer: {inputBuffer.Count} | counter: {attackCounter}");

        // Siempre marcar isPunching false PRIMERO antes de decidir qué sigue
        isPunching = false;

        if (inputBuffer.Count > 0 && attackCounter < 3)
        {
            inputBuffer.Clear();
            SetPunchToTrue();
        }
        else
        {
            inputBuffer.Clear();
            SetPunchToFalse();
        }
    }

    public void AddToInputBuffer()
    {
        if (isPunching && inputBuffer.Count == 0 && attackCounter < 3)
            inputBuffer.Add(KeyCode.Mouse0);
    }

    public void CheckAttackCounter()
    {
        if (attackCounter < 3)
            SetPunchToTrue();
        else
            SetPunchToFalse();
    }

    // Failsafe para salir de estados rotos
    private void ForceReset()
    {
        isPunching = false;
        attackCounter = 0;
        punchTimer = 0f;
        inputBuffer.Clear();
        animator.SetBool("isPunching", false);
        animator.SetInteger("attackCounter", 0);
        animator.Play("Idle"); // ← Nombre de tu estado idle en el Animator
    }
}