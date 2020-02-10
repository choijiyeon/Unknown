using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIContents
{
    public class JYUIMain : JYUIBase
    {
        public GameObject lifeObjRoot;
        protected JYPlayer player;
        public GameObject uiRoot;
        private GameObject option;
        private GameObject[] lifeObj;

        protected override void InitUIData() { }

        public override void UpdateView() { }

        public override void UpdateView(JYDefines.UISectionFun section, params object[] values)
        {
            switch (section)
            {
                case JYDefines.UISectionFun.RemoveLife:
                    {
                        int lifeCount = (int)values[0];
                        RemoveLife(lifeCount);
                    }
                    break;
            }
        }

        private void Start()
        {
            JYUIManager.Instance.AttachUISection(JYDefines.UISectionFun.RemoveLife, this);
            lifeObj = new GameObject[5];
            for (int i = 0; i < lifeObj.Length; i++)
            {
                lifeObj[i] = lifeObjRoot.transform.Find(i.ToString()).gameObject;
            }
            option = this.transform.Find("option").gameObject;
        }
      
        public void RemoveLife(int lifeCnt)
        {
            for (int i = 0; i < lifeObj.Length; i++)
            {
                if (i <= (lifeCnt - 1))
                    lifeObj[i].SetActive(true);
                else
                    lifeObj[i].SetActive(false);
            }
        }

        protected void FindPlayer()
        {
            player = uiRoot.transform.Find("CharacterRoot/player(Clone)").GetComponent<JYPlayer>();
        }

        /// <summary>
        /// callbackFunction
        /// </summary>
        public void CallJumpBtn()
        {
            //if (player == null)
            //    FindPlayer();
            //player.DoJump();
        }
        public void CallOption()
        {
            option.SetActive(true);
        }
        public void CallOptionExit()
        {
            option.SetActive(false);
        }
        public void CallGoMain()
        {
            SceneManager.LoadScene("Start");
        }
        public void CallGameExit()
        {
            Application.Quit();
        }
        public void CallTutorial()
        {
            JYUIManager.Instance.Notify(JYDefines.UISectionFun.ShowTutorial);
        }
    }
}