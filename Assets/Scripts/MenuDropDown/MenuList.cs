﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Menu.UI.Dropdown
{

    public class MenuList : MonoBehaviour
    {
        GameObject menuViewPrefab;
        private List<KeyValuePair<int, GameObject>> MenuItem = new List<KeyValuePair<int, GameObject>>();
        // Use this for initialization
       

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
}
