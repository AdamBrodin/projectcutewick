#pragma warning disable CS0649
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    #region Singleton
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Color startBlinkColor, endBlinkColor;
    [SerializeField] private float healthBarLerpSpeed, healthBarBlinkTime, healthBarBlinkPauseTime;
    private Image sliderImage;
    public Action OnPlayerDeath;
    #endregion

    protected override void Start()
    {
        base.Start();
        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = StartHealth;
            healthSlider.value = CurrentHealth;

            sliderImage = GameObject.Find("Fill")?.GetComponent<Image>();
        }
    }

    protected override void OnHitEffect()
    {
        base.OnHitEffect();
        StartCoroutine(HealthbarFlash());
    }

    protected override void OnDeathEffect()
    {
        OnPlayerDeath?.Invoke();
    }

    private IEnumerator HealthbarFlash()
    {
        if (sliderImage != null)
        {
            StartCoroutine(ColorLerpOverTime(sliderImage, sliderImage.color, startBlinkColor, healthBarBlinkTime));
            yield return new WaitForSecondsRealtime(healthBarBlinkPauseTime + healthBarBlinkTime);
            StartCoroutine(ColorLerpOverTime(sliderImage, sliderImage.color, endBlinkColor, healthBarBlinkTime));
        }
    }

    private IEnumerator ColorLerpOverTime(Image targetImage, Color startColor, Color endColor, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            targetImage.color = Color.Lerp(startColor, endColor, (elapsedTime / time));
            yield return null;
        }
    }

    private void SetHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.MoveTowards(healthSlider.value, CurrentHealth, healthBarLerpSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        SetHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Pickup")
        {
            col.gameObject?.GetComponent<PickupBase>().Pickup();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Danger Zone")
        {
            // Initiate danger zone
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Danger Zone")
        {
            // Exit danger zone
        }
    }
}
