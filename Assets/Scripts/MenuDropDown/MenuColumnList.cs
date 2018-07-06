using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Menu.UI.Dropdown
{
    public class Data{
        public int level { set; get; }
        public string key { set; get; }
        public string value { set; get; }
        public bool selected { set; get; }
        public List<Data> nestedData { set; get; }

    }

    public class MyComparer : IComparer<Data>
    {
        public int Compare(Data x, Data y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return String.Compare(x.key, y.key);

        }
    }

    public class MenuColumnList : MonoBehaviour
    {
        public RectTransform MenuView;
        public GameObject Menu;
        public GameObject MainMenuPrefab;
        public GameObject SubMenuPrefab;
        public GameObject tagPrefab;

        private GameObject Menu1;
        private GameObject Menu2;
        private Data latestClickedTab = null;

        private List<KeyValuePair<string,GameObject>> MenuItem1 = new List<KeyValuePair<string, GameObject>>();
        private List<KeyValuePair<string, GameObject>> MenuItem2 = new List<KeyValuePair<string, GameObject>>();

        private List<Data> raw = new List<Data>(); //main lsit containg all data
        private Color tempColor;

        //for upper tags
        public RectTransform panel;
        public ScrollRect myScrollRect;
        private List<GameObject> tagGameObject = new List<GameObject>();
        private List<Data> tagData = new List<Data>(); //main lsit containg all data
        private static int tagCount = 0;
        private bool newTagAdded = false;



        void FeedMenuBar(){
            
            for (int i = 0; i < 26; i++)
            {
                Data lData = new Data();
                lData.level = 0;
                lData.key =  i.ToString();
                lData.value = "tag" + i.ToString();
                lData.selected = false;

                lData.nestedData = new List<Data>();
                for (int j = 0; j < 20; j++)
                {
                    if( lData.nestedData != null){
                        Data lData1 = new Data();
                        lData1.level = 1;
                        lData1.key = i.ToString() + "-" + j.ToString();
                        lData1.value = "tag" + i.ToString() + "-" + j.ToString();
                        lData1.selected = false;

                        lData1.nestedData = new List<Data>();
                        for (int k = 0; k< 7; k++)
                        {
                            if (lData.nestedData != null)
                            {
                                Data lData2 = new Data();
                                lData2.level = 2;
                                lData2.key = i.ToString() + "-" + j.ToString()+"-" + k.ToString();
                                lData2.value = "tag" + i.ToString() + "-" + j.ToString()+ "-" + k.ToString();
                                lData2.selected = false;

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
                Menu1.transform.SetParent(MenuView.transform, false);
            }

            FeedMenuBar();

            if (level == 2)
            {
                Menu2 = Instantiate(Menu);
                Menu2.SetActive(false);
                Menu2.transform.SetParent(MenuView.transform, false);
            }
            GenerateMenu();// generate the intial view 
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if( newTagAdded){
                myScrollRect.horizontalNormalizedPosition = 1;
                newTagAdded = false;
            }

          

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
                Data lDataObj = raw[i];
                MenuItem1.Add(new KeyValuePair<string, GameObject>(current, Instantiate(MainMenuPrefab) as GameObject));
                GameObject lGameObj = MenuItem1[i].Value;

                MenuItem1[i].Value.GetComponent<Button>().onClick.AddListener(delegate
                {
                    onClickSlectedMenuItemButton(lDataObj, lGameObj);
                });

               

                if (MenuItem1[i].Value != null)
                {
                    string sub = raw[i].value;

                    MenuItem1[i].Value.GetComponentInChildren<Text>().text =  sub;

                    Menu1.SetActive(true);
                    MenuItem1[i].Value.transform.SetParent(Menu1.GetComponent<ScrollRect>().content, false);
                    MenuItem1[i].Value.gameObject.SetActive(true);
                   
                }
            }//for loop ends
            LastClickeddata = raw;
        }

       

        public void destoryMenu1DataObjects()
        {
            if (MenuItem1.Count > 0 )
            {
                foreach (Transform t in Menu1.GetComponent<ScrollRect>().content)
                    Destroy(t.gameObject);

                MenuItem1.Clear();
            }
          
        }

        public void destoryMenu2DataObjects()
        {
           if (MenuItem2.Count > 0)
            {
                foreach (Transform t in Menu2.GetComponent<ScrollRect>().content)
                    Destroy(t.gameObject);

                MenuItem2.Clear();
            }

        }


        public void GenerateSubMenu(List<Data> _menu1Data,GameObject _menu1Prefab, List<Data> _menu2Data, GameObject _menu2Prefab ){

            if (_menu1Prefab != null)
            {
                destoryMenu1DataObjects();
                Menu1.SetActive(true);
                //GenerateMenuData(_menu1Data, _menu1Prefab, MenuItem1,Menu1.GetComponent<ScrollRect>().content);
                RectTransform parent = Menu1.GetComponent<ScrollRect>().content;
                if (parent != null)
                    GenerateMenuData(_menu1Data, _menu1Prefab, MenuItem1, parent);
            }

            if(_menu2Prefab != null){
                destoryMenu2DataObjects();
                Menu2.SetActive(true);

                RectTransform parent = Menu2.GetComponent<ScrollRect>().content;
                if( parent != null)
                    GenerateMenuData(_menu2Data, _menu2Prefab, MenuItem2,parent);

                Menu2.SetActive(true);

            }

        }


    
        public void GenerateMenuData( List<Data> _data,GameObject _menuPrefab, List<KeyValuePair<string, GameObject>> _menuItem,  RectTransform _parent)
        {

            for (int i = 0; i < _data.Count; i++)
            {
                string current = _data[i].key;
                Data lDataobj = _data[i];
                {
                    
                    _menuItem.Add(new KeyValuePair<string, GameObject>(current, Instantiate(_menuPrefab) as GameObject));
                    GameObject lGameObj = _menuItem[i].Value;
                    _menuItem[i].Value.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        onClickSlectedMenuItemButton(lDataobj, lGameObj);
                    });



                }
               

                if (_menuItem[i].Value != null)
                {
                    string sub = _data[i].value;

                   _menuItem[i].Value.GetComponentInChildren<Text>().text = sub;
                    _menuItem[i].Value.transform.SetParent(_parent, false);
                    _menuItem[i].Value.gameObject.SetActive(true);

                    if(_data[i].selected) // hide the image on getting the false
                        _menuItem[i].Value.gameObject.GetComponent<MenuSubCell>().toggleImage();



                }


                //if (!clickedMenu1.Equals(_data))
                //{
                //    clickedMenu1 = _data;

                //    GenerateSubMenu(null, null, NewSubdata, SubMenuPrefab, false, false);

                //    if (lastClickedObject != null)
                //        lastClickedObject.GetComponentInChildren<Text>().color = tempColor;

                //    tempColor = _object.GetComponentInChildren<Text>().color;
                //    _object.GetComponentInChildren<Text>().color = Color.green;

                //}
                //else
                //{
                //    Menu2.SetActive(false);
                //    clickedMenu1 = "";
                //    lastClickedObject.GetComponentInChildren<Text>().color = tempColor;

                //}
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
      

        public void onClickSlectedMenuItemButton(Data _data, GameObject _object )
        {
            
            //MenuSubCell objMenuSubCell = _object.GetComponent<MenuSubCell>();
            //objMenuSubCell.toggleImage();

            //if(!_object.GetComponentInChildren<Text>().color.Equals(Color.cyan) ){
            //    tempColor = _object.GetComponentInChildren<Text>().color;
            //    _object.GetComponentInChildren<Text>().color = Color.cyan;
            //    clickedMenu2 = true;
            //}else{
            //    _object.GetComponentInChildren<Text>().color = tempColor;
            //    clickedMenu2 = false;
            //}

            Debug.Log("NEXT " + _data.key);
            string[] d = _data.key.Split('-');
            int mainIndex = Int16.Parse(d[0]);

            if (latestClickedTab == null || !latestClickedTab.Equals(_data)) //forward motion 
            {
                
                bool first = false;
                List<Data> lSubdata;

                if (_data.key.Length < 2)
                { //for showing the first list
                    first = true;
                }

                lSubdata = FindTheNextSubData(raw[mainIndex], _data.key);// for showing the other items 
               
                // autheticate data before displaying the grid
                if (LastClickeddata != null && lSubdata != null)
                {

                    //update the dat check varaibles
                    if (NewSubdata != null && !LastClickeddata.Contains(_data))
                        LastClickeddata = NewSubdata;

                    NewSubdata = lSubdata; //update the new data list
                    //search the index

                    //need to reset the all others
                    for (int i = 0; i < LastClickeddata.Count; i++)
                        LastClickeddata[i].selected = false;


                    for (int i = 0; i < LastClickeddata.Count; i++)
                        print("last :: " + LastClickeddata[i].value);

                    for (int i = 0; i < NewSubdata.Count; i++)
                        print("new :: " + NewSubdata[i].value);
                    // set the only being clicked
                   // MyComparer dc = new MyComparer();
                    int pos = LastClickeddata.FindIndex(index => index.key == _data.key);

                    LastClickeddata[pos].selected = true;

                    if (first)// showing the first tab intially
                        GenerateSubMenu(LastClickeddata, MainMenuPrefab, NewSubdata, SubMenuPrefab);
                    else
                        GenerateSubMenu(LastClickeddata, SubMenuPrefab, NewSubdata, SubMenuPrefab);
                   
                    latestClickedTab = _data;// setting data to check on next click
                }else{
                    // ist the last tab to generate click
                   
                    //NewSubdata[pos].selected = true;


                    OnClickMenuCreateTag(_data);
                }

            }else{
                string key = _data.key.Substring(0, _data.key.Length - 2);
                bool first = false;
                List<Data> lSubdata;
               
                if (key.Length < 2)
                { //for showing the first list
                    lSubdata = raw;
                    first = true;
                }
                else 
                    lSubdata = FindTheNextSubData(raw[mainIndex], key);// for showing the other items 
                
              
                // autheticate data before displaying the grid
                if (LastClickeddata != null && lSubdata != null )
                {
                    int pos = LastClickeddata.FindIndex(index => index.key == _data.key);
                    LastClickeddata[pos].selected = false;

                    //update the dat check varaibles
                    if (NewSubdata != null && !NewSubdata.Contains(_data))
                        NewSubdata = LastClickeddata;

                    LastClickeddata = lSubdata; //update the new data list

                    if( first )// showing the first tab intially
                        GenerateSubMenu(LastClickeddata, MainMenuPrefab, NewSubdata, SubMenuPrefab);
                    else
                        GenerateSubMenu(LastClickeddata, SubMenuPrefab, NewSubdata, SubMenuPrefab);

                }
                latestClickedTab = null; // setting data to check back on second click i.e. null


            }



        }


        public void OnClickMenuCreateTag(Data _data)
        {
          // find whether the dat aexist or not 
            Data pos = tagData.Find(x => x.key == _data.key );

            print("==>" + pos);

            if (pos == null ) // add new tag -avoid the repetative tags
            {
                
                tagGameObject.Add(Instantiate(tagPrefab) as GameObject);
                if (tagGameObject[tagCount] != null)
                {
                    tagGameObject[tagCount].transform.SetParent(panel.transform, false);
            

                    GameObject lObject = tagGameObject[tagCount];
                    lObject.name = tagCount.ToString(); //setting the name
                    tagGameObject[tagCount].GetComponent<Button>().onClick.AddListener(delegate
                    {
                        onClickSlectedTagButton(_data,lObject);
                    });

                    tagGameObject[tagCount].GetComponentInChildren<Text>().text = _data.value;
                    tagGameObject[tagCount].gameObject.SetActive(true);
                    //panel.sizeDelta = new Vector2(tagGameObject[tagCount].GetComponent<RectTransform>().rect.width + 20, panel.rect.height);

                    tagCount++;
                    tagData.Add(_data);
                    newTagAdded = true;
                }
            }
        }

        public void onClickSlectedTagButton(Data _data,GameObject _object){
            print("CANCEL tag - " + _data.value);

            //removing the data from the data list 
            var itemToRemove = tagData.Find(r => r.key == _data.key);
            if (itemToRemove != null)
                tagData.Remove(itemToRemove);

            //removing the data from the game object
            GameObject  objectItemToRemove = tagGameObject.Find(r => r.name == _object.name);
            if (objectItemToRemove != null)
            {
                tagGameObject.Remove(objectItemToRemove);
                Destroy(objectItemToRemove.gameObject);
            }
            tagCount--;
        } 
    }
}