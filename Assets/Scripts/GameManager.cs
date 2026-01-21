using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public int currentCombo = 0;
    public float currentMultiplier = 1;
    public TextMeshProUGUI comboNumberText;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else 
            instance = this;

        DontDestroyOnLoad(gameObject);
        

    }
    void Start()
    {
        if (comboNumberText != null)
            comboNumberText.text = currentCombo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCombo()
    {
        currentCombo++;
        currentMultiplier = Formula(currentCombo);
        comboNumberText.text = currentCombo.ToString();

    }

    public void ResetCombo()
    {
        currentCombo = 0;
        currentMultiplier = 1.0f;
    }

    float Formula(float combo)
    {

        float calculo = 1.0f + 0.5f * (combo / 20.0f);
        return calculo;
    }
}
