using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{


    public GameObject[] fields;
    public float spawnPoint = 0;
    public float fieldLength = 26;
    public int numberOfFields = 5;

    private List<GameObject> activeFields = new List<GameObject>();

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfFields; i++)
        {
            if (i == 0)
            {
                spawnNewField(0);
            }
            else
            {
                spawnNewField(Random.Range(0, fields.Length));
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 26 > spawnPoint - (numberOfFields * fieldLength))
        {
            spawnNewField(Random.Range(0, fields.Length));
            deleteTile();

        }
    }

    public void spawnNewField(int fieldIndex)
    {
        GameObject newField = Instantiate(fields[fieldIndex], transform.forward * spawnPoint, transform.rotation);
        spawnPoint += fieldLength;
        activeFields.Add(newField);


    }

    private void deleteTile()
    {
        Destroy(activeFields[0]);
        activeFields.RemoveAt(0);
    }
}
