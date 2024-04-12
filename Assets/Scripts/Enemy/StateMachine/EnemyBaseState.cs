using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected bool isMove = true;

    public EnemyBaseState(EnemyStateMachine ememyStateMachine)
    {
        stateMachine = ememyStateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
        if (isMove)
            Move();
    }

    public virtual void PhysicsUpdate()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    protected void SetTriggerAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetTrigger(animationHash);
    }

    //�̵�
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    // �̵� ���� ���ϱ� = �� ��ġ
    private Vector3 GetMovementDirection()
    {
        return (stateMachine.Enemy.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
    }

    //�� �̵��� ���� ����
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move(((direction * movementSpeed) + stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    //�� ȸ��
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            stateMachine.Enemy.transform.rotation = Quaternion.Slerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }

        //Debug.Log($"�� ȸ�� {stateMachine.Enemy.transform.rotation}");
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool IsInChaseRange()
    {
        //������ Ǫ�� ���꺸�� �ѹ� �� ���ϴ� ������ �޸𸮻� ����
        float playerDistanceSqr = (stateMachine.Enemy.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.monsterChasingRng * stateMachine.Enemy.Data.monsterChasingRng;
    }
}
