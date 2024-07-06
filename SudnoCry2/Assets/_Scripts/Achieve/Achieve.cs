using UnityEngine;
using UnityEngine.Events;

public class Achieve : MonoBehaviour
{
    [Header("StartTime")]
    private float StartTime;
    [SerializeField] private float EndTime = 1f;

    [SerializeField]
    UnityEvent active;

    void Update()
    {
        StartTime += 0.4f * Time.deltaTime;

        if (StartTime >= EndTime)
        {
            gameObject.SetActive(true);
            active.Invoke();
            Invoke("DeketAcieve", 2f);
        }
    }

    private void DeketAcieve()
        => gameObject.SetActive(false);
}
