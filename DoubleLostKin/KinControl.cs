using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using GlobalEnums;
using ModCommon.Util;
using HutongGames.PlayMaker.Actions;
namespace DoubleLostKin
{
    class KinControl:MonoBehaviour
    {
        private void Awake()
        {
            kin1 = GameObject.Find("Lost Kin");
            Modding.Logger.Log("Find Kin 1");
            hmk1 = kin1.GetComponent<HealthManager>();
            
        }
        private void Start()
        {
            kin2 = UnityEngine.Object.Instantiate(kin1);
            kin2.SetActive(true);
            Modding.Logger.Log("Clone Kin");
            hmk2 = kin2.GetComponent<HealthManager>();
            orighp = hmk1.hp;
            StartCoroutine(InitHP());
            spCL1 = kin1.LocateMyFSM("Spawn Balloon");
            spCL2 = kin2.LocateMyFSM("Spawn Balloon");
            if(spCL1!=null && spCL2!=null)
            {
                spCL1.GetAction<IntCompare>("Spawn", 1).integer2.Value = 10;
                spCL2.GetAction<IntCompare>("Spawn", 1).integer2.Value = 10;
                spCL1.GetAction<IntCompare>("Spawn", 3).integer2.Value = 99999;
                spCL2.GetAction<IntCompare>("Spawn", 3).integer2.Value = 99999;
            }
            On.HealthManager.TakeDamage += Count;
            flag = false;
        }
        private void Update()
        {
            if(heathpool>=orighp*2)
            {
                if(!flag)
                {
                    hmk1.Die(new float?(0f), AttackTypes.Nail, true);
                    hmk2.Die(new float?(0f), AttackTypes.Nail, true);
                    flag = true;
                }
                
            }
        }
        private void OnDestroy()
        {
            On.HealthManager.TakeDamage -= Count;
            heathpool = 0;
        }
        private IEnumerator InitHP()
        {
            yield return new WaitForSeconds(2f);
            hmk1.hp = 99999;
            hmk2.hp = 99999;
        }
        

        private void Count(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            if(self.gameObject.name.Contains("Lost Kin"))
            {
                heathpool += hitInstance.DamageDealt;
            }
            orig(self, hitInstance);
        }

        GameObject kin1;
        GameObject kin2;
        HealthManager hmk1;
        HealthManager hmk2;
        PlayMakerFSM spCL1;
        PlayMakerFSM spCL2;
        int heathpool = 0;
        int orighp=0;
        bool flag = false;
    }
}
