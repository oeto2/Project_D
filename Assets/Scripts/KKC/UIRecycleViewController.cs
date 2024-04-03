using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class UIRecycleViewController<T> : MonoBehaviour
    {
        protected List<T> tableData = new List<T>(); // 리스트 항목 데이터 저장
        [SerializeField]
        protected GameObject _cellBase = null; // 복사 원본 셀
        [SerializeField]
        private RectOffset _padding; // 스크롤할 내용의 패딩
        [SerializeField]
        private float _spacingHeight = 4.0f; // 각 셀 간격
        [SerializeField]
        private RectOffset _visibleRectPadding = null; // visibleRect의 패딩

        private LinkedList<UIRecycleViewCell<T>> _cells = new LinkedList<UIRecycleViewCell<T>>(); // 셀 저장 리스트

        private Rect _visibleRect; // 리스트 항목을 셀읳 ㅕㅇ태로 표시하는 범위를 나타내는 사각형

        private Vector2 _prevScrollPos; // 바로 전 스크롤 위치를 저장

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

        protected virtual void Start()
        {
            // 복사 원본 셀 비활성화
            _cellBase.SetActive(false);

            // Scroll Rect 컴포넌트의 On Value Changed 이벤트의 이벤트 리스너 설정
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
        }

        // 테이블 뷰 초기화 함수

        protected void InitializeTableView()
        {
            UpdateScrollViewSize();
            UpdateVisibleRect();

            if (_cells.Count < 1)
            {
                Vector2 cellTop = new Vector2(0f, -_padding.top);
                for (int i = 0; i < tableData.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i);
                    Vector2 cellBottom = cellTop + new Vector2(0f, -cellHeight);
                    if ((cellTop.y <= _visibleRect.y && cellTop.y >= _visibleRect.y - _visibleRect.height) ||
                        (cellBottom.y <= _visibleRect.y && cellBottom.y >= _visibleRect.y - _visibleRect.height))
                    {
                        UIRecycleViewCell<T> cell = CreateCellForIndex(i);
                        cell.Top = cellTop;
                        break;
                    }
                    cellTop = cellBottom + new Vector2(0f, _spacingHeight);
                }
                // visibleRect의 범위에 빈 곳이 있으면 셀 작성
                SetFillVisibleRectWithCells();
            }
            else
            {
                // 이미 셀이 있을 때, 첫 셀부터 순서대로 대응하는 리스트 항목의
                // 인덱스를 다시 설정하고 위치와 내용을 갱신
                LinkedListNode<UIRecycleViewCell<T>> node = _cells.First;
                UpdateCellForIndex(node.Value, node.Value.Index);
                node = node.Next;

                while (node != null)
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0f, -_spacingHeight);
                    node = node.Next;
                }
                // visibleRect의 범위에 빈 곳이 있으면 셀을 작성
                SetFillVisibleRectWithCells();
            }
        }

        // 셀의 높이값을 리턴하는 함수
        protected virtual float GetCellHeightAtIndex(int index_)
        {
            // 실제 값을 반환하는 처리는 상속한 클래스에서 구현
            // 셀마다 크기가 다를 경우 상속받은 클래스에서 재 구현
            return _cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        // 스크롤 할 내용 전체의 높이를 갱신하는 함수
        protected void UpdateScrollViewSize()
        {
            // 스크롤 할 내용 전체의 높이를 계산
            float contentHeight = 0f;
            for (int i = 0; i < tableData.Count; i++)
            {
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    contentHeight += _spacingHeight;
                }
            }

            // 스크롤할 내용의 높이를 설정
            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = _padding.top + contentHeight + _padding.bottom;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        // 셀 생성 함수
        private UIRecycleViewCell<T> CreateCellForIndex(int index_)
        {
            // 복사 원본 셀을 이용해 새로운 셀 생성
            GameObject obj = Instantiate(_cellBase) as GameObject;
            obj.SetActive(true);
            UIRecycleViewCell<T> cell = obj.GetComponent<UIRecycleViewCell<T>>();

            // 부모 요소를 바꾸면 스케일이나 크기를 잃기 때문에 변수에 저장
            Vector3 scale = cell.transform.localScale;
            Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;
            Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
            Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

            cell.transform.SetParent(_cellBase.transform.parent);

            // 셀의 스케일과 크기 설정
            cell.transform.localScale = scale;
            cell.CachedRectTransform.sizeDelta = sizeDelta;
            cell.CachedRectTransform.offsetMin = offsetMin;
            cell.CachedRectTransform.offsetMax = offsetMax;

            // 지정된 인덱스가 붙은 리스트 항목에 대응하는 셀로 내용 갱신
            UpdateCellForIndex(cell, index_);

            _cells.AddLast(cell);

            return cell;
        }

        // 셀 내용 갱신 함수
        private void UpdateCellForIndex(UIRecycleViewCell<T> cell_, int index_)
        {
            // 셀에 대응하는 리스트 항목의 인덱스 설정
            cell_.Index = index_;

            if(cell_.Index >= 0 && cell_.Index <= tableData.Count - 1)
            {
                // 셀에 대응하는 리스트 항목이 있다면 셀을 활성화해서 내용을 갱신하고 높이 설정
                cell_.gameObject.SetActive(true);
                cell_.UpdateContent(tableData[cell_.Index]);
                cell_.Height = GetCellHeightAtIndex(cell_.Index);
            }
            else
            {
                // 셀에 대응하는 리스트 항목이 없으면 셀을 비활성화
                cell_.gameObject.SetActive(false);
            }
        }

        // VisibleRect를 갱신하기 위한 함수
        private void UpdateVisibleRect()
        {
            // visibleRect의 위치는 스크롤할 내용의 기준으로부터 상대적인 위치
            _visibleRect.x = CachedScrollRect.content.anchoredPosition.x + _visibleRectPadding.left;
            _visibleRect.y = -CachedScrollRect.content.anchoredPosition.y + _visibleRectPadding.top;

            // visibleRect의 크기는 스크롤 뷰의 크기 + 패딩
            _visibleRect.width = CachedRectTransform.rect.width + _visibleRectPadding.left + _visibleRectPadding.right;
            _visibleRect.height = CachedRectTransform.rect.height + _visibleRectPadding.top + _visibleRectPadding.bottom;
        }

        // VisibleRect 범위에 표시될 만큼의 셀을 생성하여 배치하는 함수
        private void SetFillVisibleRectWithCells()
        {
            // 셀이 없다면 아무 일도 하지 않는다
            if(_cells.Count < 1)
            {
                return;
            }

            // 표시된 마지막 셀에 대응하는 리스트 항목의 다음 리스트 항목이 있고
            // 그 셀이 visibleRect의 범위에 들어오면 대응하는 셀 작성
            UIRecycleViewCell<T> lastCell = _cells.Last.Value;
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellTop = lastCell.Bottom + new Vector2(0f, -_spacingHeight);

            while(nextCellDataIndex < tableData.Count && nextCellTop.y >= _visibleRect.y - _visibleRect.height)
            {
                UIRecycleViewCell<T> cell = CreateCellForIndex(nextCellDataIndex);
                cell.Top = nextCellTop;

                lastCell = cell;
                nextCellDataIndex = lastCell.Index + 1;
                nextCellTop = lastCell.Bottom + new Vector2(0f, -_spacingHeight);
            }
        }

        // 스크롤뷰가 움직였을 때 호출되는 함수
        public void OnScrollPosChanged(Vector2 scrollPos_)
        {
            // visibleRect를 갱신한다
            UpdateVisibleRect();
            // 셀을 재사용한다
            UpdateCells((scrollPos_.y < _prevScrollPos.y) ? 1 : -1);

            _prevScrollPos = scrollPos_;
        }

        // 셀을 재사용하여 표시를 갱신하는 함수
        private void UpdateCells(int scrollDirection_)
        {
            if(_cells.Count < 1)
            {
                return;
            }

            if(scrollDirection_ > 0)
            {
                // 위로 스크롤 하고 있을 때는 visibleRect에 지정된 범위보다 위에 있는 셀을
                // 아래를 향해 순서대로 이동시켜 내용 갱신
                UIRecycleViewCell<T> firstCell = _cells.First.Value;
                while(firstCell.Bottom.y > _visibleRect.y)
                {
                    UIRecycleViewCell<T> lastCell = _cells.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);
                    firstCell.Top = lastCell.Bottom + new Vector2(0f, -_spacingHeight);

                    _cells.AddLast(firstCell);
                    _cells.RemoveFirst();
                    firstCell = _cells.First.Value;
                }

                // visibleRect에 지정된 범위 안에 빈 곳이 있으면 셀 작성
                SetFillVisibleRectWithCells();
            }
            else if(scrollDirection_ < 0)
            {
                // 아래로 스크롤하고 있을 때는 visibleRect에 지정된 범위보다 아래에 있는 셀을
                // 위를 향해 순서대로 이동시켜 내용 갱신
                UIRecycleViewCell<T> lastCell = _cells.Last.Value;
                while(lastCell.Top.y < _visibleRect.y - _visibleRect.height)
                {
                    UIRecycleViewCell<T> firstCell = _cells.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index - 1);
                    lastCell.Bottom = firstCell.Top + new Vector2(0f, _spacingHeight);

                    _cells.AddFirst(lastCell);
                    _cells.RemoveLast();
                    lastCell = _cells.Last.Value;
                }
            }
        }
    }
}