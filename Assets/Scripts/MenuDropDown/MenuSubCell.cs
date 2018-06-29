using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.Dropdown
{
    public class MenuSubCell : MonoBehaviour
    {

        public Text Name;
        public Image Image;
        private bool ImageStatus { get; set; }



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateData(string _name, bool _status)
        {
            if (_name.Length < 0)
            {
                Name.text = _name;
                Image.gameObject.SetActive(_status);
            }
        }
    }
}