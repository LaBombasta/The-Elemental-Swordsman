using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUnhide : MonoBehaviour
{
    public GameObject TextToUnhide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            TextToUnhide.SetActive(true);
        }
    }
}
