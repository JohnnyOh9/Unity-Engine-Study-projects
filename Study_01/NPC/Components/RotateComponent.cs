using UnityEngine;

namespace NPC.Components
{
    /// <summary>
    /// RotateComponent Script Summary
    /// �ٸ� ��ũ��Ʈ���� StopTurning(), TurnTowards()�� ����� ������ �Է¹���
    /// �Է¹��� ������ ���� ��� �� Update ���� ������Ʈ�� ȸ����Ŵ
    /// �ν����Ϳ��� rotationSpeed(ȸ�� �ӵ�)�� ������ �� ����
    /// 
    /// ������ �߰��� ���
    /// �̵��� ������ �Ÿ��� 0�� ��� ȸ���� ������ ���� -> �̰� �ذ��Ϸ���?
    /// �ڵ����� �ε巯�� �ڳʸ� ����?
    /// </summary>

    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 3.0f;

        private bool _isOrderedToRotate;
        private float _modifiedSpeed;
        private Vector3 _whereToLook;

        private void Update()
        {
            if(_isOrderedToRotate)
            {
                Rotate();
            }
        }
        
        public void StopTurning()
        {
            _isOrderedToRotate = false;
        }
        public void TurnTowards(Vector3 whereToLook)
        {
            _isOrderedToRotate = true;
            _modifiedSpeed = rotationSpeed;
            _whereToLook = whereToLook - transform.position;
        }
        private void Rotate()
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(_whereToLook, Vector3.up);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _modifiedSpeed * Time.deltaTime);
        }
    }
}
