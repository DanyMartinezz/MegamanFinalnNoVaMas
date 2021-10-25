using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public float Speed = 10f;
    private Rigidbody2D MyRB;
    GameObject playerG;
    Animator Myanimator;
    float dir;
    bool puedeMoverse;
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        playerG = GameObject.FindGameObjectWithTag("Player");
        dir = playerG.transform.localScale.x;

        Myanimator = GetComponent<Animator>();
        puedeMoverse = true;
    }

    private void MoverBalla()
    {
        MyRB.velocity = new Vector2(dir * Speed * Time.deltaTime, 0);
        Myanimator.SetLayerWeight(0, 1);
    }

    private void Update()
    {
        if (puedeMoverse)
            MoverBalla();
    }

    IEnumerator DestruirBala()
    {
        Myanimator.SetLayerWeight(0, 0);
        Myanimator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "BulletTag")
        {
            puedeMoverse = false;
            StartCoroutine(DestruirBala());
        }
    }
}
