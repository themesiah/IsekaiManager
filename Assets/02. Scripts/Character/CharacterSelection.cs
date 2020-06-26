using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Characters
{
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectionObject = default;

        private bool isSelected = false;

        public void Select()
        {
            isSelected = true;
            selectionObject.SetActive(true);
        }

        public void Unselect()
        {
            isSelected = false;
            selectionObject.SetActive(false);
        }

        public bool IsSelected
        {
            get => isSelected;
        }
    }
}