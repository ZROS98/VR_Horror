using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BookPhysics : MonoBehaviour
{
    [field: SerializeField]
    private float MinImpulseForce { get; set; }
    [field: SerializeField]
    private float MaxImpulseForce { get; set; }
    [field: SerializeField]
    private Rigidbody CurrentRigidbody { get; set; }
    
    private WaitForSeconds CurrentWaitForSeconds { get; set; }
    private float TimeToPushObject { get; set; } = 3.0f;

    protected virtual void Start ()
    {
        Initialize();
        StartCoroutine(TimerToPushObjectsProcess());
    }

    public void PushObject ()
    {
        CurrentRigidbody.AddForce(transform.forward * RandomizeImpulseForce(), ForceMode.Impulse);
    }

    private float RandomizeImpulseForce ()
    {
        return Random.Range(MinImpulseForce, MaxImpulseForce);
    }

    private void Initialize ()
    {
        CurrentWaitForSeconds = new WaitForSeconds(TimeToPushObject);
    }

    private IEnumerator TimerToPushObjectsProcess ()
    {
        yield return CurrentWaitForSeconds;
        
        PushObject();
    }
}