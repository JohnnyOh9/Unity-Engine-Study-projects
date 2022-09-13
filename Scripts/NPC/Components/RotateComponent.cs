using UnityEngine;

namespace NPC.Components
{
    /// <summary>
    /// RotateComponent Script Summary
    /// 다른 스크립트에서 StopTurning(), TurnTowards()를 사용해 동작을 입력받음
    /// 입력받은 동작이 있을 경우 매 Update 마다 오브젝트를 회전시킴
    /// 인스펙터에서 rotationSpeed(회전 속도)를 지정할 수 있음
    /// 
    /// 앞으로 추가할 기능
    /// 이동을 끝마쳐 거리가 0일 경우 회전할 방향이 없음 -> 이걸 해결하려면?
    /// 자동차의 부드러운 코너링 구현?
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
