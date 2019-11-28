#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class CameraShake : MonoBehaviour
{
    #region Singleton
    private static CameraShake instance;
    public static CameraShake Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraShake>();
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    private Vector3 startPos;
    private float strength, duration, decreaseAmount;
    #endregion
    public void ShakeCamera(float strength, float duration, float decreaseAmount)
    {
        startPos = transform.localPosition;
        this.strength = strength;
        this.duration = duration;
        this.decreaseAmount = decreaseAmount;
    }

    private void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = startPos + (Random.insideUnitSphere * strength);
            duration -= Time.deltaTime * decreaseAmount;
        }
    }
}
