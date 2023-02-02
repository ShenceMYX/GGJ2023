using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private SpriteRenderer eyesRenderer;

        private void Start()
        {
            motor = GetComponent<CharacterMotor>();
        }

        private void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");


            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRooting = !isRooting;

                if (enterSoilArea)
                {
                    if (isRooting)
                        HealthManager.Instance.StartIncreasingHealth();
                    else
                        HealthManager.Instance.StopIncreasingHealth();
                }
                else
                {
                    if (isRooting)
                        HealthManager.Instance.StopDecreasingHealth();
                    else
                        HealthManager.Instance.ContinueDecreasingHealth();
                }
            }
           

            //如果正在扎根 则不能动
            if (isRooting) return;

            if (xInput != 0 || yInput != 0) 
            {
                eyesRenderer.flipX = xInput < 0;

                motor.Movement(new Vector3(xInput, 0, yInput).normalized);
            }
        }
    }
}
