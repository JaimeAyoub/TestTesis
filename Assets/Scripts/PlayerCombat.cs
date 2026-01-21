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

    public GameObject[] hitboxes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllHitBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckAttackTimer();
        }
    }

    void Combos(int index)
    {
        if (index < hitboxes.Length)
        {
            DisableHitboxesExcept(hitboxes[index]);
            hitboxes[index].SetActive(true);
        }
    }

    void DisableAllHitBoxes()
    {
        foreach (GameObject go in hitboxes)
        {
            go.SetActive(false);
        }
    }

    void CheckAttackTimer()
    {
        float resta = Time.time - lastTimeAttack;
        Debug.Log(resta);
        if ((Time.time - lastTimeAttack) > timeToAddCombo) //Reseteo de contador de combo
        {
            attackCounter = 0;
            lastTimeAttack = Time.time;
        }
        else
        {
            // Debug.Log("Se agrega combo");
            attackCounter++;
        }

        if (attackCounter >= hitboxes.Length)
            attackCounter = 0;
        lastTimeAttack = Time.time;
        Combos(attackCounter);
    }

    void DisableHitboxesExcept(GameObject hitbox)
    {
        foreach (GameObject go in hitboxes)
        {
            if (go == hitbox) continue;
            go.SetActive(false);
        }
    }
}