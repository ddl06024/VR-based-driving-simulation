/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace VRKeyboard.Utils
{
    public class KeyboardManager : MonoBehaviour
    {
        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;
        public int maxInputLength;

        [Header("UI Elements")]
        public GameObject InputObject;
        public Text inputText;

        [Header("Essentials")]
        public Transform keys;
        #endregion

        #region Private Variables
        private string Input
        {
            get
            {
                if (InputObject != null)
                {
                    Component[] components = InputObject.GetComponents(typeof(Component));

                    foreach (Component component in components)
                    {
                        PropertyInfo prop = component.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
                        if (prop != null)
                            return prop.GetValue(component, null) as string; ;
                    }
                    return InputObject.name;
                }
                return "";
            }

            set
            {
                if (InputObject != null)
                {
                    Component[] components = InputObject.GetComponents(typeof(Component));

                    foreach (Component component in components)
                    {
                        PropertyInfo prop = component.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
                        if (prop != null)
                        {
                            prop.SetValue(component, value, null);
                            return;
                        }
                    }
                    InputObject.name = value;
                }
            }
            /*
            get { return inputText.text; }
            set { inputText.text = value; }
            */
        }
        private Key[] keyList;
        private bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        void Awake()
        {
            keyList = keys.GetComponentsInChildren<Key>();

            // keyboard 비활성화

        }

        void Start()
        {
            /* Debug
            if (InputObject.GetComponent<Text>() == null)
            {
                Debug.Log("Text is null");
            }
            */
            foreach (var key in keyList)
            {
                key.OnKeyClicked += GenerateInput;
            }

            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Backspace()
        {
            if (Input.Length > 0)
            {
                Input = Input.Remove(Input.Length - 1);
            }
            else
            {
                return;
            }
        }

        public void Clear()
        {
            Input = "";
        }

        public void CapsLock()
        {
            foreach (var key in keyList)
            {
                if (key is Alphabet)
                {
                    key.CapsLock(capslockFlag);
                }
            }
            capslockFlag = !capslockFlag;
        }

        public void Shift()
        {
            foreach (var key in keyList)
            {
                if (key is Shift)
                {
                    key.ShiftKey();
                }
            }
        }

        public void GenerateInput(string s)
        {
            Debug.Log("Generate Input");
            if (Input.Length > maxInputLength) { return; }
            Input += s;
            Debug.Log($"Input key {s}");
            Debug.Log($"Input Text {Input}");

            ReactivateInputField(InputObject.GetComponent<InputField>());
        }

        void ReactivateInputField(InputField inputField)
        {
            if (inputField != null)
            {
                StartCoroutine(ActivateInputFiedlWithoutSelection(inputField));
            }
        }

        IEnumerator ActivateInputFiedlWithoutSelection(InputField inputField)
        {
            inputField.ActivateInputField();

            yield return new WaitForEndOfFrame();

            if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
            {
                inputField.MoveTextEnd(false);
            }
        }
        #endregion
    }
}