using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectGlow : MonoBehaviour
{
    public GameObject sprite;

    public void Start()
    {
        shineObject(sprite, 1f, 10f);
    }

    public static void shineObject(GameObject obj, float width, float duration)
    {
        var mono = obj.GetComponent<MonoBehaviour>();
        if (mono != null)
        {
            // change material
            Material mat = Resources.Load("Materials/ShineMaterial", typeof(Material)) as Material;
            var render = obj.GetComponent<Renderer>();
            if (render != null)
            {
                render.material = mat;
            }
            else
            {
                var img = obj.GetComponent<Image>();
                if (img != null)
                {
                    img.material = mat;
                }
                else
                {
                    Debug.LogWarning("cannot get the render or image compoent!");
                }
            }
            mat.SetFloat("_ShineWidth", 5f);
            
            mono.StopAllCoroutines();
            mono.StartCoroutine(shineRoutine(mat, duration));
        }
        else
        {
            Debug.LogWarning("cannot find MonoBehaviour component!");
        }
    }

    static IEnumerator shineRoutine(Material mat, float duration)
    {
        if (mat != null)
        {
            float location = 0f;
            float interval = 0.04f;
            float offsetVal = interval / duration;
            while (true)
            {
                yield return new WaitForSeconds(interval);
                mat.SetFloat("_ShineLocation", location);
                location += offsetVal;
                if (location > 1f)
                {
                    location = 0f;
                }
            }
        }
        else
        {
            Debug.LogWarning("there is no material parameter!");
        }
    }
}
