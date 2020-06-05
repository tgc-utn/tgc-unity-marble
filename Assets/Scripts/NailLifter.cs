using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailLifter : MonoBehaviour
{
    [SerializeField]
    private float period;

    [SerializeField]
    private float holdDelay;

    [SerializeField]
    private float height;

    [SerializeField]
    private List<GameObject> rows;

    private List<Vector3> initialPositions;

    float timer = 0f;
    int parity = 0;
    bool holding = true;

    void Awake()
    {
        initialPositions = rows.ConvertAll((row) => row.transform.localPosition);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(holding)
        {
            if(timer > holdDelay)
            {
                timer = 0f;
                holding = false;
            }
        }
        else
        {
            int index = 0;
            rows.ForEach(row =>
            {
                if (index % 2 == parity)
                    row.transform.localPosition = Vector3.Lerp(initialPositions[index], initialPositions[index] + Vector3.up * height, timer / period);
                else
                    row.transform.localPosition = Vector3.Lerp(initialPositions[index] + Vector3.up * height, initialPositions[index], timer / period);
                index++;
            });

            if (timer > period)
            {
                parity = 1 - parity;
                timer = 0f;
                holding = true;
            }
        }

        
    }
}
