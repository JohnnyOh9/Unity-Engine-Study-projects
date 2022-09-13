using UnityEngine;

namespace NPC.Chicks
{
    public class ChickAnimationHandler : MonoBehaviour
    {
        private Animator _animator;
        private int _walkHash;
        private int _runHash;
        private int _jumpHash;
        private int _eatHash;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _walkHash = Animator.StringToHash("Walk");
            _runHash = Animator.StringToHash("Run");
            _jumpHash = Animator.StringToHash("Jump");
            _eatHash = Animator.StringToHash("Eat");
            
            ChangeAnimation("Idle");
        }
        public void ChangeAnimation(string animationState)
        {
            switch (animationState)
            {
                case "Idle":
                    _animator.SetBool(_walkHash, false);
                    _animator.SetBool(_runHash, false);
                    _animator.SetBool(_eatHash, false);
                    break;
                case "Walk":
                    _animator.SetBool(_walkHash, true);
                    _animator.SetBool(_runHash, false);
                    _animator.SetBool(_eatHash, false);
                    break;
                case "Run":
                    _animator.SetBool(_walkHash, false);
                    _animator.SetBool(_runHash,  true);
                    _animator.SetBool(_eatHash, false);
                    break;
                case "Jump":
                    _animator.SetBool(_walkHash, false);
                    _animator.SetBool(_runHash, false);
                    _animator.SetBool(_eatHash, false);
                    _animator.SetTrigger(_jumpHash);
                    break;
                case "Eat":
                    _animator.SetBool(_walkHash, false);
                    _animator.SetBool(_runHash, false);
                    _animator.SetBool(_eatHash, true);
                    break;
            }
        }
    }
}
