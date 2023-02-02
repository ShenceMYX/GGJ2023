using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerInputController : MonoBehaviour
    {
        //进入土壤
        public bool enterSoilArea;
        //正在扎根
        private bool isRooting = false;

        private float xInput;
        private float yInput;

        private CharacterMotor motor;

        [SerializeField] private Transform flipSpriteTrans;

        private MMF_Player rootFeedbacks;
        private MMF_Player unrootFeedbacks;

        [SerializeField] private Animator treeBaseAnim;
        [SerializeField] private float rootDelay = 0.5f;

        private void Start()
        {
            motor = GetComponent<CharacterMotor>();

            Transform feedbacksRootTrans = transform.Find("feedbacks");
            rootFeedbacks = feedbacksRootTrans.Find("root feedbacks").GetComponent<MMF_Player>();
            unrootFeedbacks = feedbacksRootTrans.Find("unroot feedbacks").GetComponent<MMF_Player>();
        }

        private void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            treeBaseAnim.SetFloat("Input Magnitude", Mathf.Clamp01(new Vector3(xInput, 0, yInput).magnitude));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRooting = !isRooting;

                if (isRooting) rootFeedbacks?.PlayFeedbacks(); else unrootFeedbacks?.PlayFeedbacks();

                if (enterSoilArea)
                {
                    if (isRooting)
                        HealthManager.Instance.StartIncreasingHealth(rootDelay);
                    else
                        HealthManager.Instance.StopIncreasingHealth(rootDelay);
                }
                else
                {
                    if (isRooting)
                        HealthManager.Instance.StopDecreasingHealth(rootDelay);
                    else
                        HealthManager.Instance.ContinueDecreasingHealth(rootDelay);
                }
            }
           

            //如果正在扎根 则不能动
            if (isRooting) return;

            if (xInput != 0 || yInput != 0) 
            {
                if (xInput != 0)
                    flipSpriteTrans.localScale = new Vector3(xInput < 0 ? -1 : 1, 1, 1);

                motor.Movement(new Vector3(xInput, 0, yInput).normalized);

                
            }
        }
    }
}
