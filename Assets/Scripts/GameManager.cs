using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCombo = 0;
    public float currentMultiplier = 1;
    public TextMeshProUGUI comboNumberText;

    public float timeToResetCombo = 0.0f;
    private float maxTimeToResetCombo = 10.0f;
    public Slider timeSlider;

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
        timeSlider.maxValue = maxTimeToResetCombo;
        timeSlider.value = timeToResetCombo;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();
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
        timeToResetCombo = maxTimeToResetCombo;
        comboNumberText.text = currentCombo.ToString();
    }

    float Formula(float combo)
    {
        float calculo = 1.0f + 0.5f * (combo / 20.0f);
        return calculo;
    }

    void CheckTimer()
    {
        if (currentCombo > 0 && timeToResetCombo >= 0)
        {
            timeSlider.value = timeToResetCombo;
            timeToResetCombo -= Time.deltaTime;
        }

        if (timeToResetCombo <= 0.0f)
        {
            ResetCombo();
        }
    }

    public void AddToTimer(float timeToAdd)
    {
        if (timeToResetCombo < maxTimeToResetCombo)
            timeToResetCombo += timeToAdd;
    }
}