using UnityEngine;
using NPC.Components;

namespace NPC.Chicks
{
    public class ChickStates : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private float distanceToStop = 0.7f;
        [SerializeField] private float standingOffsetX;
        [SerializeField] private float standingOffsetZ;
        [SerializeField] private float startRunningDistance = 2.0f;
        
        private MoveComponent _move;
        private RotateComponent _rotate;
        private ChickAnimationHandler _animate;
        
        private string _characterState;
        private Vector3 _whereIam;
        private Vector3 _whereIsParent;
        private Vector3 _whereToGoTo;
        private float _distanceFromParent;
        private float _distanceFromGoTo;
        private float _timerCount;
       
        private void Start()
        {
            _move = GetComponent<MoveComponent>();
            _rotate = GetComponent<RotateComponent>();
            _animate = GetComponent<ChickAnimationHandler>();
        }
        private void Update()
        {
            switch(_characterState)
            {
                case "Idle"  : Idle();    break;
                case "Walk"  : Walk();   break;
                case "Run"   : Run();   break;
                case "Jump"  : Jump();   break;
                //case "Wandering": Wandering(); break;
                default: 
                    Debug.Log("this " + gameObject.name + " has error in the area of character state stuffs.");
                    ChangeCharacterState("Idle");
                    _animate.ChangeAnimation("Idle");
                    break;
            }
        }
        
        private void Idle()
        {
            //Character State Initialize 
            UpdateInformation();
            Timer();

            //Character State Action Routine

            //Character State Transitions
            if (_distanceFromParent >= distanceToStop + (distanceToStop * 0.2))
            {
                ChangeCharacterState("Walking");
                _animate.ChangeAnimation("Walk");
            }
            //else if (timerCount >= 3.0f)
            //{
            //    ChangeCharacterState("Wandering");
            //    UpdateAnimation("Eat");
            //}
        }
        private void Walk()
        {
            //Character State Initialize 
            _whereToGoTo = GetGoTo(GetBehind(parent));
            UpdateInformation();

            //Character State Action Routine
            _move.WalkTo(_whereToGoTo);
            _rotate.TurnTowards(_whereToGoTo);

            //Character State Transitions
            if (_distanceFromGoTo == 0f)
            {
                ChangeCharacterState("Idle");
                _animate.ChangeAnimation("Idle");
            }
            else if (_distanceFromParent > startRunningDistance)
            {
                ChangeCharacterState("Jump");
                _animate.ChangeAnimation("Jump");
            }
        }
        private void Run()
        {
            //Character State Initialize
            _whereToGoTo = GetGoTo(GetBehind(parent));
            UpdateInformation();

            //Character State Action Routine
            _move.RunTo(_whereToGoTo);
            _rotate.TurnTowards(_whereToGoTo);

            //Character State Transitions
            if (_distanceFromGoTo <= 0f)
            {
                ChangeCharacterState("Idle");
                _animate.ChangeAnimation("Idle");
            }
        }
        private void Jump()
        {
            //Character State Initialize
            _whereToGoTo = GetGoTo(GetBehind(parent));
            UpdateInformation();
            Timer();

            //Character State Action Routine
            _move.WalkTo(_whereToGoTo);
            _rotate.TurnTowards(_whereToGoTo);

            //Character State Transitions
            if (_timerCount > 0.35f)
            {
                ChangeCharacterState("Run");
                _animate.ChangeAnimation("Run");
            }
            //Coroutines && Local Methods
        }
        
        private void UpdateInformation()
        {
            _whereIam = transform.position;
            _whereIsParent = parent.transform.position;
            _distanceFromParent = Vector3.Distance(_whereIam, _whereIsParent);
            _distanceFromGoTo = Vector3.Distance(_whereIam, _whereToGoTo);
        }
        private void ChangeCharacterState(string characterState)
        {
            _characterState = characterState;
            _timerCount = 0f;
        }

        private Vector3 GetBehind(GameObject target)
        {
            return target.transform.position + target.transform.forward * (distanceToStop * -1);
        }
        private Vector3 GetGoTo(Vector3 where)
        {
            float x = where.x + standingOffsetX;
            float z = where.z + standingOffsetZ;
            return new Vector3(x, 0, z);
        }
        private void Timer() => _timerCount += Time.deltaTime;
    }
}
