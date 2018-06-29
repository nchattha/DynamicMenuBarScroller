using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeControllerScript : MonoBehaviour
{
    //public variables
    public RectTransform panel;
    public GameObject[] tagData;
    public RectTransform center;

    //private variables
    private float[] distance; //from current position
    private bool draggingPanel = false;

    private int tagDistance; // distance between the buttons
    private int minTagNum; //hold number of button

    private void Start()
    {
        int tagLength = tagData.Length;

        distance = new float[tagLength];
        // get distance between buttons;
        tagDistance = (int)Mathf.Abs(tagData[1].GetComponent<RectTransform>().anchoredPosition.x - tagData[0].GetComponent<RectTransform>().anchoredPosition.x);
        print(tagDistance);

    }

    void Update()
    {
        for (int i = 0; i < tagData.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - tagData[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance); //get the min distance

        for (int a = 0; a < tagData.Length; a++)
        {
            if ( minDistance.Equals(distance[a]))
            {
                minTagNum = a;
            }
        }

        if (!draggingPanel)
        {
            SnapButton((int) (minTagNum *  -minDistance));
        }

    }


    void SnapButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 2f);
        Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPos;
    }

    public void StartDrag(){
        draggingPanel = true;
    }

    public void EndDrag(){
        draggingPanel = false;
    }

}
