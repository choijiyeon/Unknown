using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIContents
{
    public class JYUIResult : JYUIBase
    {

        protected override void InitUIData() { }

        public override void UpdateView() { }

        public override void UpdateView(JYDefines.UISectionFun section, params object[] values)
        {
            switch (section)
            {
                case JYDefines.UISectionFun.RemoveLife:
                    {
                    }
                    break;
            }
        }
       
    }
}