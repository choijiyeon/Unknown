using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIContents
{
    public class JYUITutorial : JYUIBase
    {
        public GameObject TutorialObj;

        protected override void InitUIData() { }

        public override void UpdateView() { }

        public override void UpdateView(JYDefines.UISectionFun section, params object[] values)
        {
            switch (section)
            {
                case JYDefines.UISectionFun.ShowTutorial:
                    {
                        ShowTutorial();
                    }
                    break;
            }
        }
        private void Start()
        {
            JYUIManager.Instance.AttachUISection(JYDefines.UISectionFun.ShowTutorial, this);
            DisableTutorial();
        }

        private void ShowTutorial()
        {
            TutorialObj.SetActive(true);
            Invoke("DisableTutorial", 3f);
        }

        private void DisableTutorial()
        {
            TutorialObj.SetActive(false);
        }
    }
}
