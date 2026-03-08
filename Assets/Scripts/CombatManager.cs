using UnityEngine;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine.VFX;
using Unity.VisualScripting;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public CinemachineVirtualCameraBase cinemachine;
    private CinemachineBasicMultiChannelPerlin noise;
    public GameObject Player;

    [Header("----Valores para Camara Shake---")]
    public float shakeTimer;

    public float intensityCameraShake;
    public float amplitudeCameraShake;

    [Header("----Valores para Freeze on impact---")]
    public float timeFreeze;

    [Header("----VFX para hit---")] public VisualEffect hitVFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (cinemachine == null)
            cinemachine = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        if (cinemachine != null)
        {
            noise = cinemachine.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    }

    public async Awaitable FreezeOnHit()
    {
        Time.timeScale = 0;
        await Task.Delay((int)(timeFreeze * 1000));
        Time.timeScale = 1;
    }

    public async Awaitable CameraShake()
    {
        if (cinemachine == null)
            cinemachine = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        if (cinemachine != null)
        {
            noise = cinemachine.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        noise.AmplitudeGain = amplitudeCameraShake;
        noise.FrequencyGain = intensityCameraShake;
        await Task.Delay((int)(shakeTimer * 1000));
        noise.AmplitudeGain = 0;
        noise.FrequencyGain = 0;
    }

    public void SpawnHitVFX(Vector3 position, float rotation)
    {
        if (hitVFX != null)
        {
            VisualEffect vfx = Instantiate(hitVFX, position, Quaternion.identity);

            //Debug.Log(position);
            // rotation = Mathf.Deg2Rad * rotation;

            if (Player == null)
                Player = GameObject.FindGameObjectWithTag("Player");
            Vector3 newTarget = new Vector3(Player.transform.position.x,
                vfx.gameObject.transform.position.y, Player.transform.position.z);
            vfx.gameObject.transform.LookAt(newTarget);
            //vfx.SetFloat("AngleFloat", rotation);
            //  vfx.SetVector3("Angle",rotation);
            vfx.SendEvent("OnPlay");
            Destroy(vfx.gameObject, 2.0f);
        }
        else
        {
            Debug.Log("No hitVFX found");
        }
    }
}