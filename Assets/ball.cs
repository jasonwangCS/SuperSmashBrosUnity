using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    private bool fadeOut, fadeIn, blink;
    SpriteRenderer renderer;
    //if hit by a player, start blinking
    //few seconds after blinking, illuminate player

    public void Start()
    {
        fadeOut = true;
        blink = fadeIn = false;
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (fadeOut)
        {
            Color objectColor = renderer.material.color;
            float fadeAmount = objectColor.a - (1 * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            renderer.material.color = objectColor;

            if(objectColor.a <= 0)
            {
                fadeOut = false;
                fadeIn = true;
            }
        }
        if (fadeIn)
        {
            Color objectColor = renderer.material.color;
            float fadeAmount = objectColor.a + (1 * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            renderer.material.color = objectColor;

            if (objectColor.a >= 1)
            {
                fadeOut = true;
                fadeIn = false;
            }
        }
    }

    IEnumerator Blink()
    {
        while (blink)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            blink = true;
            fadeIn = fadeOut = false;
            StartCoroutine("Blink");
            Debug.Log("Blink");
            Destroy(gameObject, 3f);
        }
    }
}
