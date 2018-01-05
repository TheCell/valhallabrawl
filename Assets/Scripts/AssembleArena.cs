using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleArena : MonoBehaviour
{
    public float assemblyTimeSeconds = 2.0f;
    public float startDelaySeconds = 2.0f;
    private float startTime = 0.0f;
    private ArrayList childOriginPos = new ArrayList();
    private ArrayList childOffsetPos = new ArrayList();
    private MeshFilter[] meshfilters;
    private float maxOffset = 15.0f;

    /*
    private Vector3 position;
    private Vector3 currentPosition;
    private Vector3 offPosition;
    */
    private bool finished = false;

	// Use this for initialization
	void Start ()
    {
        meshfilters = gameObject.GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter meshfilter in meshfilters)
        {
            float xVec = Random.value * maxOffset;
            float yVec = Random.value * maxOffset;
            float zVec = Random.value * maxOffset;

            if (Random.value < 0.5)
            {
                xVec *= -1;
            }
            if (Random.value < 0.5)
            {
                yVec *= -1;
            }
            if (Random.value < 0.5)
            {
                zVec *= -1;
            }

            Vector3 randomVec = new Vector3(xVec, yVec, zVec);
            childOriginPos.Add(meshfilter.transform.position);
            meshfilter.transform.position += randomVec;
            childOffsetPos.Add(meshfilter.transform.position);
            
        }

        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;

        if (finished)
        {
            return;
        }

        if (startTime + assemblyTimeSeconds + startDelaySeconds < currentTime)
        {
            finished = true;
        }
        // gameObject.GetComponent<Transform>().position = position;

        float currentPos = 1.0f;

        if (!finished)
        {
            currentPos = 0.0f;

            if (currentTime > startTime + startDelaySeconds)
            {
                float momentPos = (currentTime - (startTime + startDelaySeconds));
                float posToReach = (assemblyTimeSeconds);
                currentPos = momentPos / posToReach;
                // Debug.Log("momentPos: " + momentPos + " posToReach: " + posToReach + " isCalculated: " + currentPos);
            }
        }

        for (int i = 0; i < meshfilters.Length; i++)
        {
            Vector3 originPos = (Vector3) childOriginPos[i];
            Vector3 originOffset = (Vector3) childOffsetPos[i];
            Vector3 calculatedTemporalPos;
            calculatedTemporalPos.x = Mathf.Lerp(originOffset.x, originPos.x, currentPos);
            calculatedTemporalPos.y = Mathf.Lerp(originOffset.y, originPos.y, currentPos);
            calculatedTemporalPos.z = Mathf.Lerp(originOffset.z, originPos.z, currentPos);
            meshfilters[i].transform.position = calculatedTemporalPos;
        }
    }

    /*
    private IEnumerable jumpIntoPlace()
    {
        yield return null; // return here next frame
    }
    */
}
