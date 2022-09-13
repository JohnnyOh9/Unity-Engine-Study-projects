using UnityEngine;

namespace NPC.Components
{
    /// <summary>
    /// MoveComponent Script Summary
    /// �ٸ� ��ũ��Ʈ���� Stop(), WalkTo(), RunTo()�� ����� ������ �Է¹���
    /// �Է¹��� ������ ���� ��� �� FixedUpdate ���� ������Ʈ�� �̵���Ŵ
    /// �ν����Ϳ��� movementSpeed(�̵��ӵ�)�� runMultiplier(�޸��� ���)�� ������ �� ����
    /// 
    /// ������ �߰��� ���
    /// ���ӵ� �̵�, ���ӵ� �̵�, ���Ӱ� ���ӵ� �̵� �߰��ϱ� (�ڵ��� ���� NPC�� ���ؼ�)
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
