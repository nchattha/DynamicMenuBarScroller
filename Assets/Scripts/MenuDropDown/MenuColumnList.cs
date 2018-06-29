using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.Dropdown
{
    public class MenuColumnList : MonoBehaviour
    {

        public RectTransform MenuView;
        public GameObject MainMenuPrefab;
        public GameObject SubMenuPrefab;
        private List<KeyValuePair<int,GameObject>> MenuItem = new List<KeyValuePair<int, GameObject>>();
        private static int menuCount = 0;
        private const int FRAME_OFFSET = 5;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GenerateMenu( bool _first){
            int currentItem = menuCount;
            if (_first){
                MenuItem.Add(new KeyValuePair<int, GameObject>(menuCount,Instantiate(MainMenuPrefab) as GameObject));
                MenuItem[menuCount].Value.GetComponent<Button>().onClick.AddListener(delegate
                {
                    onClickNextMenuItemButton(currentItem);
                });
            }
            else{
                MenuItem.Add(new KeyValuePair<int, GameObject>(menuCount, Instantiate(SubMenuPrefab) as GameObject));
                MenuItem[menuCount].Value.GetComponent<Button>().onClick.AddListener(delegate
                {
                    onClickSlectedMenuItemButton(currentItem);
                });
            }
                

            if (MenuItem[menuCount].Value != null)
            {
                MenuItem[menuCount].Value.GetComponentInChildren<Text>().text = "Tag " + menuCount;
                MenuItem[menuCount].Value.transform.SetParent(MenuView.transform, false);
                MenuItem[menuCount].Value.gameObject.SetActive(true);
                if (menuCount == 0)
                {

                    MenuView.sizeDelta = new Vector2(MenuItem[menuCount].Value.GetComponent<RectTransform>().rect.width + FRAME_OFFSET, MenuView.rect.height);
                    //MenuItem[menuCount].GetComponent<RectTransform>().anchoredPosition =
                    //new Vector2((MenuItem[menuCount].Value.GetComponent<RectTransform>().rect.width) / 2, MenuItem[menuCount].Value.GetComponent<RectTransform>().anchoredPosition.y);


                }
                else
                {
                    // set new width & height 
                    MenuView.sizeDelta = new Vector2(MenuView.rect.width,((menuCount + 1) * MenuItem[menuCount].Value.GetComponent<RectTransform>().rect.height) + FRAME_OFFSET);
                    //move the y axis of the view with repsect to the new menuitem 
                    MenuView.anchoredPosition = new Vector2(MenuView.anchoredPosition.x, (MenuView.anchoredPosition.y - (MenuItem[menuCount].Value.GetComponent<RectTransform>().rect.height / 2)));
                    //move the menu item in X axis - by default its an vertical view 
                    //MenuItem[menuCount].GetComponent<RectTransform>().anchoredPosition = new Vector2((MenuItem[menuCount].GetComponent<RectTransform>().rect.width) / 2,
                                                   //    MenuItem[menuCount - 1].GetComponent<RectTransform>().anchoredPosition.y - (MenuItem[menuCount].GetComponent<RectTransform>().rect.height + 2));
                }

                menuCount++;
            }
        }

        public void onClickNextMenuItemButton(int _data)
        {
            Debug.Log("NEXT "+_data);
        }

        public void onClickSlectedMenuItemButton(int _data )
        {
            Debug.Log("SELECTED "+_data);
        }

    }
}