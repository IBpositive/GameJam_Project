using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowForce = 10f;

    public void Shoot(Transform transform)
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * arrowForce, ForceMode2D.Impulse);
    }
}
