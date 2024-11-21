using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderingState : EnemyBaseState
{
    public EnemyWanderingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.NavMeshAgent.Resume();
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

    //이동할 목적지 구하기
    Vector3 GetWanderLocation()
    {
        return stateMachine.Enemy.MonsterWanderDestination[stateMachine.Enemy.CurWanderDestination_index].position;
    }

    //랜덤한 이동 목적지 구하기
    Vector3 GetRandomWanderLocation()
    {
        //현재 오브젝트 위치
        Vector3 curObjectPosition = stateMachine.Enemy.transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * 10f; // 원하는 범위 내의 랜덤한 방향 벡터를 생성합니다.
        randomDirection += curObjectPosition; // 랜덤 방향 벡터를 현재 위치에 더합니다.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
        {
            // NavMesh 위의 랜덤 위치를 반환합니다.
            return hit.position; 
        }
        else
        {
            // NavMesh 위의 랜덤 위치를 찾지 못한 경우 현재 위치를 반환합니다.
            return curObjectPosition; 
        }
    }

    //설정된 목적지에 도착했는지 확인
    public bool HasArrived()
    {
        //경로 계산이 완료 되었을 때만 남은거리 계산하기.
        return !stateMachine.Enemy.NavMeshAgent.pathPending 
            && stateMachine.Enemy.NavMeshAgent.remainingDistance <= stateMachine.Enemy.NavMeshAgent.stoppingDistance;
    }

    public override void Update()
    {
        //일정 범위내로 들어왔으면 플레이어 추적 하기
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        //목적지에 가까워 졌으면 기본 상태로 돌아가기
        if (HasArrived())
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
