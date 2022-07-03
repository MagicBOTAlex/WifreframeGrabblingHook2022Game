using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineShaderPerformanceTester : MonoBehaviour
{
    public bool StartTest = false;
    public GameObject TestPrefab;
    private bool testRunning = false;
    private TextMesh scoreDisplayer;
    private int score = 0;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float value;

    private void Start()
    {
        scoreDisplayer = GetComponent<TextMesh>();
        scoreDisplayer.text = score.ToString();

        startPos = transform.position;
        targetPos = transform.position + new Vector3(1, 0, 0);
        value = 0f;
    }

    private void Update()
    {
        if (StartTest && !testRunning)
        {
            testRunning = true;
        }

        if (testRunning)
        {
            Instantiate(TestPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity, transform);
            score++;
            scoreDisplayer.text = score.ToString();

            //value += Time.deltaTime;
            //transform.position = Vector3.Lerp(startPos, targetPos, Time.deltaTime);

            //if (value >= 1f && Vector3.Distance(startPos, targetPos) < 0.2f)
            //{
            //    var posStore = startPos;
            //    startPos = targetPos;
            //    targetPos = posStore;
            //    value = 0f;
            //}
        }
    }

    //IEnumerator StartActualTest()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSecondsRealtime(0.1f);
    //        Instantiate(TestPrefab, transform.position + new Vector3(0,-2,0), Quaternion.identity, transform);
    //        score++;
    //        scoreDisplayer.text = score.ToString();
    //    }
    //}

    //IEnumerator StartMoving()
    //{
    //    var startPos = transform.position;
    //    var targetPos = transform.position + new Vector3(1,0,0);
    //    var value = 0f;

    //    while (true)
    //    {
    //        yield return new WaitForSecondsRealtime(0.1f);
    //        value += 0.1f;
    //        transform.position = Vector3.Lerp(startPos, targetPos, 0.1f);

    //        if (value >= 1f)
    //        {
    //            var posStore = startPos;
    //            startPos = targetPos;
    //            targetPos = posStore;
    //            value = 0f;
    //        }
    //    }
    //}
}
