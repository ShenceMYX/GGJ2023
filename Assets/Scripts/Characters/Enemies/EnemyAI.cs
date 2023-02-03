using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class EnemyAI : MonoBehaviour
	{
		enum EnemyState
        {
			idle, //在侦测范围内则由idle切为pursuit  不在侦测范围外则为idle
			attack, //在攻击范围内则进攻，超出攻击范围切为追踪
			persuit
        }

        //侦测范围半径
        [SerializeField] private float visionRange = 3f;
        [SerializeField] private LayerMask playerLayer;
        public Collider[] detectedPlayerCollider = new Collider[1];

        [SerializeField] private EnemyState currentState = EnemyState.idle;

        private bool withinVisionRange;
        private bool withinAttackRange;

        private EnemyHealthController enemyHealthController;
        private CharacterMotor motor;

        private MMF_Player rootFeedbacks;
        private MMF_Player unrootFeedbacks;

        [SerializeField] private Animator baseAnim;

        private void Start()
        {
            enemyHealthController = GetComponent<EnemyHealthController>();
            motor = GetComponent<CharacterMotor>();

            Transform feedbacksRootTrans = transform.Find("feedbacks");
            rootFeedbacks = feedbacksRootTrans.Find("root feedbacks").GetComponent<MMF_Player>();
            unrootFeedbacks = feedbacksRootTrans.Find("unroot feedbacks").GetComponent<MMF_Player>();
        }

        private void Update()
        {
            if (enemyHealthController.isDeath) { motor.Movement(Vector3.zero); return; }

            switch (currentState)
            {
                case EnemyState.idle:
                    IdleActions();
                    break;
                case EnemyState.persuit:
                    PursuitActions();
                    break;
                case EnemyState.attack:
                    AttackActions();
                    break;
                default:
                    break;
            }

            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, visionRange, detectedPlayerCollider, playerLayer);
            //玩家在侦测范围内
            withinVisionRange = hitCount > 0;

            withinAttackRange = enemyHealthController.playerEnterRange;

        }

        private void PursuitActions()
        {
            if (!withinVisionRange)
            {
                currentState = EnemyState.idle;
                motor.Movement(Vector3.zero);
                baseAnim.SetFloat("moveSpeed", 0);
                rootFeedbacks?.PlayFeedbacks();
                return;
            }
            if (withinAttackRange)
            {
                currentState = EnemyState.attack;
                rootFeedbacks?.PlayFeedbacks();
                motor.Movement(Vector3.zero);
                baseAnim.SetFloat("moveSpeed", 0);
                return;
            }

            motor.Movement((PlayerInputController.Instance.transform.position - transform.position).normalized);
            baseAnim.SetFloat("moveSpeed", 1);
            enemyHealthController.isRooting = false;
        }

        private void AttackActions()
        {
            if (!withinAttackRange)
            {
                currentState = EnemyState.persuit;
                unrootFeedbacks?.PlayFeedbacks();
                return;
            }
            enemyHealthController.isRooting = true;
        }

        private void IdleActions()
        {
            if (withinVisionRange)
            {
                currentState = EnemyState.persuit;
                unrootFeedbacks?.PlayFeedbacks();
                return;
            }
            enemyHealthController.isRooting = true;
        }



        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, visionRange);
        }

    }
}
