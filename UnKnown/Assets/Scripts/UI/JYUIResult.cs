using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIContents
{
    public class JYUIResult : JYUIBase
    {
        public GameObject ClearObj;
        public GameObject FailObj;

        protected override void InitUIData() { }

        public override void UpdateView() { }

        public override void UpdateView(JYDefines.UISectionFun section, params object[] values)
        {
            switch (section)
            {
                case JYDefines.UISectionFun.ShowResult:
                    {
                        ShowResult((bool)values[0]);
                    }
                    break;
            }
        }
        private void Start()
        {
            JYUIManager.Instance.AttachUISection(JYDefines.UISectionFun.ShowResult, this);
            ClearObj.SetActive(false);
            FailObj.SetActive(false);
        }

        private void ShowResult(bool isClear)
        {
            if (isClear == true)
                ClearObj.SetActive(true);
            else if (isClear == false)
                FailObj.SetActive(true);
        }
        public void CallGoMain()
        {
            SceneManager.LoadScene("Start");
        }
        public void CallReplay(GameObject obj)
        {
            SceneManager.LoadScene(obj.name);
        }
    }
}