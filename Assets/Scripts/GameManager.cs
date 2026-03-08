using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCombo = 0;
    public float currentMultiplier = 1;
    public TextMeshProUGUI comboNumberText;

    public GameObject enemyObject;

    public float timeToResetCombo = 0.0f;
    private float maxTimeToResetCombo = 10.0f;
    public Slider timeSlider;

    public Transform spawnEnemyPoint;

    public VisualEffect inkDeathEffect;
    
    public List<GameObject> spawnPoints = new List<GameObject>();

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
        if (Input.GetKeyDown(KeyCode.P))
            SpawnEnemy();
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
        Debug.Log("InkAmount multiplier" + calculo);
        return calculo;
    }

    void CheckTimer()
    {
        if (currentCombo > 0 && timeToResetCombo >= 0)
        {
            timeSlider.value = timeToResetCombo;
            timeToResetCombo -= 2 * Time.deltaTime;
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

    public void SpawnEnemy()
    {
        int random =  Random.Range(0, spawnPoints.Count);
        
        Instantiate(enemyObject, spawnPoints[random].transform.position, Quaternion.identity);
    }

    public void SpawnInkDeath(GameObject enemy)
    {
        VisualEffect vfx = Instantiate(inkDeathEffect, enemy.transform.position, Quaternion.identity);
        vfx.SetSkinnedMeshRenderer("EnemyMesh", enemy.GetComponentInChildren<SkinnedMeshRenderer>());
        vfx.SendEvent("OnPlay");
        enemy.SetActive(false);
        Destroy(enemy,1.0f);
    }
}