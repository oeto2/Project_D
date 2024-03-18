using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderingState : EnemyBaseState
{
    public EnemyWanderingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("��Ȳ ���� ����");
        stateMachine.Enemy.NavMeshAgent.Resume();
        //Debug.Log("���ο� ��ǥ�� �̵�����");
        stateMachine.MovementSpeedModifier = 1f;

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
        
        stateMachine.Enemy.NavMeshAgent.SetDestination(GetWanderLocation());
        stateMachine.Enemy.CurWanderDestination_index++;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    //�̵��� ������ ���ϱ�
    Vector3 GetWanderLocation()
    {
        return stateMachine.Enemy.MonsterWanderDestination[stateMachine.Enemy.CurWanderDestination_index].position;
    }

    //������ �̵� ������ ���ϱ�
    Vector3 GetRandomWanderLocation()
    {
        //���� ������Ʈ ��ġ
        Vector3 curObjectPosition = stateMachine.Enemy.transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * 10f; // ���ϴ� ���� ���� ������ ���� ���͸� �����մϴ�.
        randomDirection += curObjectPosition; // ���� ���� ���͸� ���� ��ġ�� ���մϴ�.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas)) // ���� ��ġ�� NavMesh ���� �ִ��� Ȯ���մϴ�.
        {
            // NavMesh ���� ���� ��ġ�� ��ȯ�մϴ�.
            return hit.position; 
        }
        else
        {
            // NavMesh ���� ���� ��ġ�� ã�� ���� ��� ���� ��ġ�� ��ȯ�մϴ�.
            return curObjectPosition; 
        }
    }

    //������ �������� �����ߴ��� Ȯ��
    public bool HasArrived()
    {
        //��� ����� �Ϸ� �Ǿ��� ���� �����Ÿ� ����ϱ�.
        return !stateMachine.Enemy.NavMeshAgent.pathPending 
            && stateMachine.Enemy.NavMeshAgent.remainingDistance <= stateMachine.Enemy.NavMeshAgent.stoppingDistance;
    }

    public override void Update()
    {
        //���� �������� �������� �÷��̾� ���� �ϱ�
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        //�������� ����� ������ �⺻ ���·� ���ư���
        if (HasArrived())
        {
            //Debug.Log("������ ����");
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}