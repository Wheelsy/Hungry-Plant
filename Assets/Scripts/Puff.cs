using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puff : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.6f);

        Destroy(gameObject);
    }
}
