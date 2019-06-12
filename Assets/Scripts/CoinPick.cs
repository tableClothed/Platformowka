using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPick : MonoBehaviour {

    GameSession gameSession;
    [SerializeField] AudioClip audioClip;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession.PointCount();
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        Destroy(gameObject);
    }


}
