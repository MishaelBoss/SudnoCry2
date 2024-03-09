using UnityEngine;

public class Achieve : MonoBehaviour
{
    [Header("StartTime")]
    private float StartTime;
    [SerializeField] private float EndTime = 1f;

    [Header("Audio")]
    public AudioSource Audio;

    [Header("GameObjectAchieve")]
    public GameObject _achieve;

    [Header("Animator")]
    Animator animator;

    void Start()
        => animator = GetComponent<Animator>();

    void Update()
    {
        StartTime += 0.4f * Time.deltaTime;

        if (StartTime >= EndTime)
        {
            _achieve.gameObject.SetActive(true);
            animator.SetTrigger("Black");
            Invoke("DeketAcieve", 2f);
            
        }
    }

    private void DeketAcieve()
    {
        Audio.gameObject.SetActive(false);
        _achieve.gameObject.SetActive(false);
    }
}
