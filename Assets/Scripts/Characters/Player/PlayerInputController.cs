using System.Collections;
using System.Collections.Generic;
using Common;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerInputController : MonoSingleton<PlayerInputController>
    {
        //进入土壤
        public bool enterSoilArea;
        //正在扎根
        public bool isRooting { get; private set; } = false;

        private float xInput;
        private float yInput;

        private CharacterMotor motor;

        [SerializeField] private Transform flipSpriteTrans;

        private MMF_Player rootFeedbacks;
        private MMF_Player unrootFeedbacks;

        [SerializeField] private Animator treeBaseAnim;
        public float rootDelay = 0.5f;
        //土壤使得生命值增加的速度
        public float soilIncreaseSpeed;

        public bool canInput { get; set; } = true;

        public TreeHeight treeHeight { get; private set; }

        [Tooltip("对其他树木造成伤害，让他们生命降低的速度")]
        public float playerDamage = 20;

        private void Start()
        {
            //HealthManager.Instance.healthChangeSpeed = autoDecreasingSpeed;

            motor = GetComponent<CharacterMotor>();
            treeHeight = GetComponentInChildren<TreeHeight>();

            Transform feedbacksRootTrans = transform.Find("feedbacks");
            rootFeedbacks = feedbacksRootTrans.Find("root feedbacks").GetComponent<MMF_Player>();
            unrootFeedbacks = feedbacksRootTrans.Find("unroot feedbacks").GetComponent<MMF_Player>();
        }

        private void Update()
        {
            if (!canInput) { motor.Movement(Vector3.zero); return; }

            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            treeBaseAnim.SetFloat("Input Magnitude", Mathf.Clamp01(new Vector3(xInput, 0, yInput).magnitude));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRooting = !isRooting;

                if (isRooting) rootFeedbacks?.PlayFeedbacks(); else unrootFeedbacks?.PlayFeedbacks();

                //if (enterSoilArea)
                //{
                //    //-20 + 40 = 20
                //    if (isRooting)
                //        HealthManager.Instance.AddHealthChangeSpeed(soilIncreaseSpeed,rootDelay);
                //    //20 - 40 = -20
                //    else
                //        HealthManager.Instance.AddHealthChangeSpeed(-soilIncreaseSpeed,rootDelay);
                //}
                //else
                //{
                //    //-20 + 20 = 0
                //    if (isRooting)
                //        HealthManager.Instance.AddHealthChangeSpeed(-autoDecreasingSpeed, rootDelay);
                //    //0 - 20 = -20
                //    else
                //        HealthManager.Instance.AddHealthChangeSpeed(autoDecreasingSpeed, rootDelay);
                //}
            }
           

            //如果正在扎根 则不能动
            if (isRooting) return;

            if (xInput != 0 || yInput != 0) 
            {
                if (xInput != 0)
                    flipSpriteTrans.localScale = new Vector3(xInput < 0 ? -flipSpriteTrans.localScale.y : flipSpriteTrans.localScale.y, flipSpriteTrans.localScale.y, 1);

                motor.Movement(new Vector3(xInput, 0, yInput).normalized);

                
            }
        }
    }
}
