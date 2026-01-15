using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public BoxCollider box1;
    private float coolDown;
    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }
    void Inputs()
    {
        if(Input.GetMouseButtonDown(0))
        {
            box1.gameObject.SetActive(true);
        }else if(Input.GetMouseButtonUp(0))
            box1.gameObject.SetActive(false);
    }

}
