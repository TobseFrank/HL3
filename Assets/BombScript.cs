using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    public float delayTime;
    public GameObject explosionEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Explode), delayTime);
    }

    void Explode()
    {
        GetComponent<AudioSource>().Play();
        Instantiate(explosionEffectPrefab, transform.position,Quaternion.identity);
        //Destroy(gameObject);
    }
}
