using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Menu.UI.Dropdown
{
    //public class Data{
    //    public int level { set; get; }
    //    public string key { set; get; }
    //    public string value { set; get; }
    //    public List<Data> nestedData { set; get; }
         
    //}


    public class MenuColumnListMain : MonoBehaviour
    {
       
        //public RectTransform MenuView;
        //public GameObject MainMenuPrefab;
        //public GameObject SubMenuPrefab;
        //private List<KeyValuePair<string,GameObject>> MenuItem = new List<KeyValuePair<string, GameObject>>();

        //private List<Data> raw = new List<Data>();
       
        //private const int FRAME_OFFSET = 5;



        //void FeedMenuBar(){
            
        //    for (int i = 0; i < 6; i++)
        //    {
        //        Data lData = new Data();
        //        lData.level = 0;
        //        lData.key =  i.ToString();
        //        lData.value = "tag" + i.ToString();
        //        lData.nestedData = new List<Data>();
        //        for (int j = 0; j < 6; j++)
        //        {
        //            if( lData.nestedData != null){
        //                lData.nestedData.Add(new Data()
        //                {
        //                    level = 1,
        //                    key = i.ToString()+"-"+ j.ToString(),
        //                    value = "tag" + i.ToString() + "-" + j.ToString()
        //                });
        //            }

        //        }
        //        raw.Add(lData);
        //    }
        //}

        //// Use this for initialization
        //void Start()
        //{
           
        //    FeedMenuBar();
        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}

        //public void GenerateMenu( bool _first){
           
        //    for (int i = 0; i < raw.Count; i++)
        //    {
        //        string current = raw[i].key;
        //        if (_first)
        //        {
        //            MenuItem.Add(new KeyValuePair<string, GameObject>(current, Instantiate(MainMenuPrefab) as GameObject));
        //            MenuItem[i].Value.GetComponent<Button>().onClick.AddListener(delegate
        //            {
        //                onClickNextMenuItemButton(current);
        //            });
        //        }
        //        else
        //        {
        //            MenuItem.Add(new KeyValuePair<string, GameObject>(current, Instantiate(SubMenuPrefab) as GameObject));
        //            MenuItem[i].Value.GetComponent<Button>().onClick.AddListener(delegate
        //            {
        //                onClickSlectedMenuItemButton(current);
        //            });
        //        }


        //        if (MenuItem[i].Value != null)
        //        {
        //            string sub = raw[i].value;

        //            MenuItem[i].Value.GetComponentInChildren<Text>().text =  sub;
        //            MenuItem[i].Value.transform.SetParent(MenuView.transform, false);
        //            MenuItem[i].Value.gameObject.SetActive(true);
        //            if (i == 0)
        //            {

        //              //  MenuView.sizeDelta = new Vector2(MenuItem[i].Value.GetComponent<RectTransform>().rect.width + FRAME_OFFSET, MenuView.rect.height);
        //                //MenuItem[i].GetComponent<RectTransform>().anchoredPosition =
        //                //new Vector2((MenuItem[i].Value.GetComponent<RectTransform>().rect.width) / 2, MenuItem[i].Value.GetComponent<RectTransform>().anchoredPosition.y);


        //            }
        //            else
        //            {
        //                // set new width & height 
        //           //     MenuView.sizeDelta = new Vector2(MenuView.rect.width, ((i + 1) * MenuItem[i].Value.GetComponent<RectTransform>().rect.height) + FRAME_OFFSET);
        //                //move the y axis of the view with repsect to the new menuitem i
        //        //        MenuView.anchoredPosition = new Vector2(MenuView.anchoredPosition.x, (MenuView.anchoredPosition.y - (MenuItem[i].Value.GetComponent<RectTransform>().rect.height / 2)));
        //                //move the menu item in X axis - by default its an vertical view 
        //                //MenuItem[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((MenuItem[i].GetComponent<RectTransform>().rect.width) / 2,
        //                //    MenuItem[i - 1].GetComponent<RectTransform>().anchoredPosition.y - (MenuItem[i].GetComponent<RectTransform>().rect.height + 2));
        //            }


        //        }
        //    }//for loop ends
        //}

        //public void onClickCreateSubMenu(){
            
        //}

        //public void GenerateSubMenu(List<Data> _data  )
        //{

        //    for (int i = 0; i < _data.Count; i++)
        //    {
        //        string current = _data[i].key;
        //       {
        //            MenuItem.Add(new KeyValuePair<string, GameObject>(current, Instantiate(SubMenuPrefab) as GameObject));
        //            MenuItem[i].Value.GetComponent<Button>().onClick.AddListener(delegate
        //            {
        //                onClickSlectedMenuItemButton(current);
        //            });
        //        }


        //        if (MenuItem[i].Value != null)
        //        {
        //            string sub = _data[i].value;

        //            MenuItem[i].Value.GetComponentInChildren<Text>().text = sub;
        //            MenuItem[i].Value.transform.SetParent(MenuView.transform, false);
        //            MenuItem[i].Value.gameObject.SetActive(true);
        //            if (i == 0)
        //            {

        //                //MenuView.sizeDelta = new Vector2(MenuItem[i].Value.GetComponent<RectTransform>().rect.width + FRAME_OFFSET, MenuView.rect.height);
        //                //MenuItem[i].GetComponent<RectTransform>().anchoredPosition =
        //                //new Vector2((MenuItem[i].Value.GetComponent<RectTransform>().rect.width) / 2, MenuItem[i].Value.GetComponent<RectTransform>().anchoredPosition.y);


        //            }
        //            else
        //            {
        //                // set new width & height 
        //                //MenuView.sizeDelta = new Vector2(MenuView.rect.width, ((i + 1) * MenuItem[i].Value.GetComponent<RectTransform>().rect.height) + FRAME_OFFSET);
        //                //move the y axis of the view with repsect to the new menuitem i
        //                // MenuView.anchoredPosition = new Vector2(MenuView.anchoredPosition.x, (MenuView.anchoredPosition.y - (MenuItem[i].Value.GetComponent<RectTransform>().rect.height / 2)));
        //                //move the menu item in X axis - by default its an vertical view 
        //                //MenuItem[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((MenuItem[i].GetComponent<RectTransform>().rect.width) / 2,
        //                //    MenuItem[i - 1].GetComponent<RectTransform>().anchoredPosition.y - (MenuItem[i].GetComponent<RectTransform>().rect.height + 2));
        //            }


        //        }
        //    }//for loop ends
        //}



        //private List<Data>FindTheNextSubData(Data _data, string _key){
        //    if( _data == null) return null;

        //    if (_data.key == _key) return _data.nestedData;

        //    foreach( Data lData in _data.nestedData){
        //        List<Data>rData = FindTheNextSubData(lData, _key);
        //        if (rData != null)
        //            return rData;
        //    }

        //    return null;
        //}



        //public void onClickNextMenuItemButton(string _data)
        //{
        //    Debug.Log("NEXT "+_data);
        //    string[] d = _data.Split('-');
        //    int mainIndex = Int16.Parse(d[0]);

        //    List<Data> Ldata = FindTheNextSubData(raw[mainIndex], _data);
        //    for (int i = 0; i < Ldata.Count; i++){
        //        print(Ldata[i].value);
        //    }
        //    GenerateSubMenu(Ldata);
        //}

        //public void onClickSlectedMenuItemButton(string _data )
        //{
        //    Debug.Log("SELECTED "+_data);
        //}

    }
}