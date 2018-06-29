using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeControllerScriptNew : MonoBehaviour
{
    //public variables
    public RectTransform panel;
    private List<GameObject> tagData = new List<GameObject>();
    public GameObject tagPrefab;
    public GameObject cancelTagPrefab;
    public ScrollRect myScrollRect;
    private static int tagCount = 0;
    private const int FRAME_OFFSET = 20;


    private void Start()
    {
        
    }

    void Update()
    {
        

    }

    public void OnClickMenuCreateTag(){

        tagData.Add(Instantiate(tagPrefab) as GameObject);
        if( tagData[tagCount] != null){
           tagData[tagCount].transform.SetParent(panel.transform,false);

            if(tagCount == 0){
                
                panel.sizeDelta = new Vector2(tagData[tagCount].GetComponent<RectTransform>().rect.width + FRAME_OFFSET, panel.rect.height);
                tagData[tagCount].GetComponent<RectTransform>().anchoredPosition =
                                     new Vector2((tagData[tagCount].GetComponent<RectTransform>().rect.width) / 2, tagData[tagCount].GetComponent<RectTransform>().anchoredPosition.y);

               
            }else{
                panel.sizeDelta = new Vector2(((tagCount+1) * tagData[tagCount].GetComponent<RectTransform>().rect.width) + FRAME_OFFSET, panel.rect.height);
                tagData[tagCount].GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(tagData[tagCount - 1].GetComponent<RectTransform>().anchoredPosition.x + (tagData[tagCount].GetComponent<RectTransform>().rect.width + 2)
                                                                                               , tagData[tagCount].GetComponent<RectTransform>().anchoredPosition.y);
            }
                
            tagCount++;
            myScrollRect.horizontalNormalizedPosition = 1;
        }

    }

    public void onClickNextMenuItemButton()
    {
        Debug.Log("NEXT");
    }

    public void onClickSlectedMenuItemButton()
    {
        Debug.Log("SELECTED");
    }

}
