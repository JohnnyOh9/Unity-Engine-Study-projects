using UnityEngine;

namespace NPC.Components
{
    /// <summary>
    /// MoveComponent Script Summary
    /// 다른 스크립트에서 Stop(), WalkTo(), RunTo()를 사용해 동작을 입력받음
    /// 입력받은 동작이 있을 경우 매 FixedUpdate 마다 오브젝트를 이동시킴
    /// 인스펙터에서 movementSpeed(이동속도)와 runMultiplier(달리기 배수)를 지정할 수 있음
    /// 
    /// 앞으로 추가할 기능
    /// 가속된 이동, 감속된 이동, 가속과 감속된 이동 추가하기 (자동차 같은 NPC를 위해서)
    /// </summary>
    public class MoveComponent : MonoBehaviour
    {
        Rigidbody _rigidbody;

        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float runMultiplier = 2;

        private bool _isOrderedToMove;
        private float _modifiedSpeed;
        private Vector3 _whereToGoTo;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if(_isOrderedToMove)
            {
                Move();
            }
        }
        public void Stop()
        {
            _isOrderedToMove = false;
        }
        public void WalkTo(Vector3 whereToGoTo)
        {
            _isOrderedToMove = true;
            _modifiedSpeed = movementSpeed;
            _whereToGoTo = whereToGoTo;
        }
        public void RunTo(Vector3 whereToGoTo)
        {
            _isOrderedToMove = true;
            _modifiedSpeed = movementSpeed * runMultiplier;
            _whereToGoTo = whereToGoTo;
        }
        private void Move()
        {
            _rigidbody.MovePosition(transform.position + _whereToGoTo * (_modifiedSpeed * Time.deltaTime));
        }
    }
}
