using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube item;
    public int level;
    float cubeHeight;
    public float distance = 2.75f;
    Color nextColor;
    public float colorChangeStep = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cubeHeight = item.transform.localScale.y;
        item.gameObject.SetActive(false);
        nextColor = item.GetComponent<Renderer>().material.GetColor(("_ColorTop"));
        CreateCube();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            BreakeCube();

            CreateCube();
        }
    }

    private void BreakeCube()
    {
        if (previousCube == null)
            return;
        //previousCube 영역을 벗아난건 부수자.

    }
    MovingCube previousCube;
    private void CreateCube()
    {
        level++;
        Vector3 startPos;
        if(level % 2 == 1)
        {
            startPos = new Vector3(distance, level * cubeHeight, distance);
        }
        else
        {
            startPos = new Vector3(-distance, level * cubeHeight, distance);
        }
        var newCube = Instantiate(item, startPos, item.transform.rotation);
        newCube.gameObject.SetActive(true);

        // 색상 지정하자.
        // 다음 색 지정하자.
        Color.RGBToHSV(nextColor, out float h, out float s, out float v);
        nextColor = Color.HSVToRGB(h + 1f/256 * colorChangeStep, s,v);

        // 그라데이션 사용할꺼면 아래 로직 사용
        //nextColor = gradient.Evaluate(level % 100f * 0.01f);
        newCube.GetComponent<Renderer>().material.SetColor(("_ColorTop"),nextColor);
        newCube.GetComponent<Renderer>().material.SetColor(("_ColorBottom"), nextColor);

        previousCube = newCube;
        // 카메라 위로 이동하자.
        Camera.main.transform.Translate(0, cubeHeight, 0, Space.World);
    }
    public Gradient gradient;
}
