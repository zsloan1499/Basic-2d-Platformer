using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    //accesses the UI text
    [SerializeField]private Text cherriesText;

    [SerializeField]private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log("Cherries: " + cherries);
            //using the UI text to change it
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
