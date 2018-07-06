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
    //    public bool selected { set; get; }
    //    public List<Data> nestedData { set; get; }
         
    //}


    public class MenuColumnListMain2 : MonoBehaviour
    {
        public RectTransform MenuView;
        public GameObject Menu;
        public GameObject MainMenuPrefab;
        public GameObject SubMenuPrefab;

        private GameObject Menu1;
        private string clickedMenu1;
        private GameObject Menu2;
       

        private List<KeyValuePair<string,GameObject>> MenuItem1 = new List<KeyValuePair<string, GameObject>>();
        private List<KeyValuePair<string, GameObject>> MenuItem2 = new List<KeyValuePair<string, GameObject>>();

        private List<Data> raw = new List<Data>();
       
        private Color tempColor;


        void FeedMenuBar(){
            
            for (int i = 0; i < 6; i++)
            {
                Data lData = new Data();
                lData.key =  i.ToString();
                lData.value = "tag" + i.ToString();
                lData.nestedData = new List<Data>();
                for (int j = 0; j < 10; j++)
                {
                    if( lData.nestedData != null){
                        Data lData1 = new Data();
                        lData1.key = i.ToString() + "-" + j.ToString();
                        lData1.value = "tag" + i.ToString() + "-" + j.ToString();
                        lData1.nestedData = new List<Data>();
                        for (int k = 0; k< 5; k++)
                        {
                            if (lData.nestedData != null)
                            {
                                Data lData2 = new Data();
                                lData2.key = i.ToString() + "-" + j.ToString()+"-" + k.ToString();
                                lData2.value = "tag" + i.ToString() + "-" + j.ToString()+ "-" + k.ToString();

                                lData1.nestedData.Add(lData2);
                            }
                        }
                        lData.nestedData.Add(lData1);
                    }
                }
                raw.Add(lData);
            }
        }
      
        int level = 2;
        // Use this for initialization
        void Start()
        {
            if (MenuView != null && Menu1 == null)
            {
                Menu1 = Instantiate(Menu);
                Menu1.SetActive(false);
                clickedMenu1 = "";
                Menu1.transform.SetParent(MenuView.transform, false);
            }

            FeedMenuBar();

            if (level == 2)
            {
                Menu2 = Instantiate(Menu);
                Menu2.SetActive(false);
                clickedMenu1 = "";
                Menu2.transform.SetParent(MenuView.transform, false);
            }
            //clickedMenu2 = false;
            GenerateMenu();
        }
        // Update is called once per frame
        void Update()
        {

        }

        //Initial the menu bar 
        public void GenerateMenu(){
            if (MenuItem1.Count > 0){
                foreach (Transform t in Menu1.transform)
                    Destroy(t.gameObject);
                
                MenuItem1.Clear();
            }
              
            for (int i = 0; i < raw.Count; i++)
            {
                string current = raw[i].key;
                MenuItem1.Add(new KeyValuePair<string, GameObject>(current, Instantiate(MainMenuPrefab) as GameObject));
                GameObject lData = MenuItem1[i].Value;

                MenuItem1[i].Value.GetComponent<Button>().onClick.AddListener(delegate
                {
                   onClickNextMenuItemButton(current,lData);
                });

               

                if (MenuItem1[i].Value != null)
                {
                    string sub = raw[i].value;

                    MenuItem1[i].Value.GetComponentInChildren<Text>().text =  sub;

                    Menu1.SetActive(true);
                    MenuItem1[i].Value.transform.SetParent(Menu1.transform, false);
                    MenuItem1[i].Value.gameObject.SetActive(true);
                   

                }
            }//for loop ends
        }

       

        public void destoryMenu1DataObjects()
        {
            if (MenuItem1.Count > 0 )
            {
                foreach (Transform t in Menu1.transform)
                    Destroy(t.gameObject);

                MenuItem1.Clear();
            }
          
        }

        public void destoryMenu2DataObjects()
        {
           if (MenuItem2.Count > 0)
            {
                foreach (Transform t in Menu2.transform)
                    Destroy(t.gameObject);

                MenuItem2.Clear();
            }

        }


        public void GenerateSubMenu(List<Data> _menu1Data,GameObject _menu1Prefab, List<Data> _menu2Data, GameObject _menu2Prefab, bool _status1, bool _status2 ){

            if (_menu1Prefab != null)
            {
                destoryMenu1DataObjects();
                Menu1.SetActive(true);
                GenerateMenuData(_menu1Data, _menu1Prefab, MenuItem1, _status1,Menu1);

            }

            if(_menu2Prefab != null){
                destoryMenu2DataObjects();
                Menu2.SetActive(true);
                GenerateMenuData(_menu2Data, _menu2Prefab, MenuItem2, _status2,Menu2);
            }

        }


    
        public void GenerateMenuData( List<Data> _data,GameObject _menuPrefab, List<KeyValuePair<string, GameObject>> _menuItem, bool status, GameObject _parent)
        {

            for (int i = 0; i < _data.Count; i++)
            {
                string current = _data[i].key;
                {
                    
                    _menuItem.Add(new KeyValuePair<string, GameObject>(current, Instantiate(_menuPrefab) as GameObject));
                    GameObject lData = _menuItem[i].Value;
                    _menuItem[i].Value.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        onClickSlectedMenuItemButton(current, lData);
                    });
                }


                if (_menuItem[i].Value != null)
                {
                    string sub = _data[i].value;

                    _menuItem[i].Value.GetComponentInChildren<Text>().text = sub;
                   _menuItem[i].Value.transform.SetParent(_parent.transform, false);
                    _menuItem[i].Value.gameObject.SetActive(true);

                    if(!status) // hide the image on getting the false
                        _menuItem[i].Value.gameObject.GetComponent<MenuSubCell>().toggleImage();
                    //_menuItem[i].Value.gameObject.GetComponent<MenuSubCell>().UpdateData(sub, _data[i].selected);


                }
            }//for loop ends
        }



        private List<Data>FindTheNextSubData(Data _data, string _key){
            if( _data == null) return null;

            if (_data.key == _key) return _data.nestedData;

            if (_data.nestedData != null)
            {
                for (int i = 0; i < _data.nestedData.Count; i++)
                {
                    Data lData = _data.nestedData[i];
                    List<Data> rData = FindTheNextSubData(lData, _key);

                    if (rData != null)
                        return rData;
                }
            }
            return null;
        }


        private GameObject lastClickedObject;
        List<Data> LastClickeddata; 
        List<Data> NewSubdata; 
      
            public void onClickNextMenuItemButton(string _data,GameObject _object)
        {
            Debug.Log("NEXT "+_data);
            string[] d = _data.Split('-');
                int mainIndex = Int16.Parse(d[0]);
            //setting the data to slected
          


            if (_data.Length == 1)
            {
                NewSubdata = FindTheNextSubData(raw[mainIndex], _data);
                for (int i = 0; i < NewSubdata.Count; i++)
                {
                    print(NewSubdata[i].value);
                }

                if (!clickedMenu1.Equals(_data))
                {
                    clickedMenu1 = _data;

                    GenerateSubMenu(null, null, NewSubdata, SubMenuPrefab, false, false);

                    if (lastClickedObject != null)
                        lastClickedObject.GetComponentInChildren<Text>().color = tempColor;

                    tempColor = _object.GetComponentInChildren<Text>().color;
                    _object.GetComponentInChildren<Text>().color = Color.green;

                }
                else
                {
                    Menu2.SetActive(false);
                    clickedMenu1 = "";
                    lastClickedObject.GetComponentInChildren<Text>().color = tempColor;

                }
            }
                      
            lastClickedObject = _object;
            LastClickeddata = NewSubdata;
        }


        public void onClickSlectedMenuItemButton(string _data, GameObject _object )
        {
            Debug.Log("SELECTED "+_data);
            MenuSubCell objMenuSubCell = _object.GetComponent<MenuSubCell>();
            objMenuSubCell.toggleImage();

            if(!_object.GetComponentInChildren<Text>().color.Equals(Color.cyan) ){
                tempColor = _object.GetComponentInChildren<Text>().color;
                _object.GetComponentInChildren<Text>().color = Color.cyan;
                //clickedMenu2 = true;
            }else{
                _object.GetComponentInChildren<Text>().color = tempColor;
               // clickedMenu2 = false;
            }

            Debug.Log("NEXT " + _data);
            string[] d = _data.Split('-');
            int mainIndex = Int16.Parse(d[0]);

            NewSubdata = FindTheNextSubData(raw[mainIndex], _data);
            if (NewSubdata != null)
            {
                for (int i = 0; i < LastClickeddata.Count; i++)
                    print("-" + LastClickeddata[i].value);

                for (int i = 0; i < NewSubdata.Count; i++)
                    print(NewSubdata[i].value);
             }

            if( lastClickedObject != null && NewSubdata != null )
                GenerateSubMenu(LastClickeddata, SubMenuPrefab, NewSubdata, SubMenuPrefab, true, false);



            //else{//for shifing the data 

            //    LastClickeddata = FindTheNextSubData(raw[mainIndex], _data.Substring(0,_data.Length-2));
            //    NewSubdata = FindTheNextSubData(raw[mainIndex], _data);   
            //    for (int i = 0; i < LastClickeddata.Count; i++)
            //        print(LastClickeddata[i].value);
            //    for (int i = 0; i < NewSubdata.Count; i++)
            //        print(NewSubdata[i].value);
            //}

        }

    }
}