using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMenu : MonoBehaviour
{
    //public variables
    public RectTransform panel;
    private List<GameObject> tagData = new List<GameObject>();
    public GameObject tagPrefab;
    public GameObject cancelTagPrefab;
    private static int tagCount = 0;
    private const int FRAME_OFFSET = 20;

    public Dropdown dropDownPrefab;
    private List<string> dropMainData = new List<string>();

    private void Start()
    {
        dropDownPrefab.gameObject.SetActive(false);
        for (int i = 0; i < FRAME_OFFSET; i++)
            dropMainData.Add("Menu" + i);
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

                LoadDropDownMenuData();
            }else{
                panel.sizeDelta = new Vector2(((tagCount+1) * tagData[tagCount].GetComponent<RectTransform>().rect.width) + FRAME_OFFSET, panel.rect.height);
                tagData[tagCount].GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(tagData[tagCount - 1].GetComponent<RectTransform>().anchoredPosition.x + (tagData[tagCount].GetComponent<RectTransform>().rect.width + 2)
                                                                                               , tagData[tagCount].GetComponent<RectTransform>().anchoredPosition.y);
            }
                
            tagCount++;
        }

    }

    private void LoadDropDownMenuData()
    {
        dropDownPrefab.gameObject.SetActive(true);

        dropDownPrefab.ClearOptions();
        dropDownPrefab.AddOptions(dropMainData);

    }

}
