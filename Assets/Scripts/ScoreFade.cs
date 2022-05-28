using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreFade : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float vanishTimer;
    private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        textColor = textMesh.color;
        vanishTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 70f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        vanishTimer -= Time.deltaTime;

        if (vanishTimer < 0)
        {
            textColor.a -= 3f * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
