using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;
namespace DoubleLostKin
{
    public class DoubleLostKin:Mod,ITogglableMod
    {
        public override string GetVersion()
        {
            return "1.0.0.0";
        }
        public override void Initialize()
        {
            ModHooks.Instance.NewGameHook += Set;
            ModHooks.Instance.SavegameLoadHook += AgainSet;
            ModHooks.Instance.LanguageGetHook += Instance_LanguageGetHook;
        }

        private string Instance_LanguageGetHook(string key, string sheetTitle)
        {
            string result;
            switch(key)
            {
                case "NAME_LOST_KIN":
                    result = "双失落近亲";
                    break;
                case "GG_S_LOST_KIN":
                    result = "失落的深渊之双神";
                    break;
                default:
                    result = Language.Language.GetInternal(key, sheetTitle);
                    break;
            }
            return result;
        }

        private void AgainSet(int id)
        {
            Set();
        }

        private void Set()
        {
            if(GameManager.instance.GetComponent<KinFinder>()==null)
            {
                GameManager.instance.gameObject.AddComponent<KinFinder>();
                Log("Add Finder");
            }
        }
        public void Unload()
        {
            ModHooks.Instance.NewGameHook -= Set;
            ModHooks.Instance.SavegameLoadHook -= AgainSet;
            KinFinder kinFinder = GameManager.instance.gameObject.GetComponent<KinFinder>();
            if(kinFinder!=null)
            {
                UnityEngine.Object.Destroy(kinFinder);
            }
        }
    }
}
