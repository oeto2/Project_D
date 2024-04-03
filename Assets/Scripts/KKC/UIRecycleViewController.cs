using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class UIRecycleViewController<T> : MonoBehaviour
    {
        protected List<T> tableData = new List<T>(); // ����Ʈ �׸� ������ ����
        [SerializeField]
        protected GameObject _cellBase = null; // ���� ���� ��
        [SerializeField]
        private RectOffset _padding; // ��ũ���� ������ �е�
        [SerializeField]
        private float _spacingHeight = 4.0f; // �� �� ����
        [SerializeField]
        private RectOffset _visibleRectPadding = null; // visibleRect�� �е�

        private LinkedList<UIRecycleViewCell<T>> _cells = new LinkedList<UIRecycleViewCell<T>>(); // �� ���� ����Ʈ

        private Rect _visibleRect; // ����Ʈ �׸��� ���� �Ť��·� ǥ���ϴ� ������ ��Ÿ���� �簢��

        private Vector2 _prevScrollPos; // �ٷ� �� ��ũ�� ��ġ�� ����

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

        protected virtual void Start()
        {
            // ���� ���� �� ��Ȱ��ȭ
            _cellBase.SetActive(false);

            // Scroll Rect ������Ʈ�� On Value Changed �̺�Ʈ�� �̺�Ʈ ������ ����
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
        }

        // ���̺� �� �ʱ�ȭ �Լ�

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
                // visibleRect�� ������ �� ���� ������ �� �ۼ�
                SetFillVisibleRectWithCells();
            }
            else
            {
                // �̹� ���� ���� ��, ù ������ ������� �����ϴ� ����Ʈ �׸���
                // �ε����� �ٽ� �����ϰ� ��ġ�� ������ ����
                LinkedListNode<UIRecycleViewCell<T>> node = _cells.First;
                UpdateCellForIndex(node.Value, node.Value.Index);
                node = node.Next;

                while (node != null)
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0f, -_spacingHeight);
                    node = node.Next;
                }
                // visibleRect�� ������ �� ���� ������ ���� �ۼ�
                SetFillVisibleRectWithCells();
            }
        }

        // ���� ���̰��� �����ϴ� �Լ�
        protected virtual float GetCellHeightAtIndex(int index_)
        {
            // ���� ���� ��ȯ�ϴ� ó���� ����� Ŭ�������� ����
            // ������ ũ�Ⱑ �ٸ� ��� ��ӹ��� Ŭ�������� �� ����
            return _cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        // ��ũ�� �� ���� ��ü�� ���̸� �����ϴ� �Լ�
        protected void UpdateScrollViewSize()
        {
            // ��ũ�� �� ���� ��ü�� ���̸� ���
            float contentHeight = 0f;
            for (int i = 0; i < tableData.Count; i++)
            {
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    contentHeight += _spacingHeight;
                }
            }

            // ��ũ���� ������ ���̸� ����
            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = _padding.top + contentHeight + _padding.bottom;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        // �� ���� �Լ�
        private UIRecycleViewCell<T> CreateCellForIndex(int index_)
        {
            // ���� ���� ���� �̿��� ���ο� �� ����
            GameObject obj = Instantiate(_cellBase) as GameObject;
            obj.SetActive(true);
            UIRecycleViewCell<T> cell = obj.GetComponent<UIRecycleViewCell<T>>();

            // �θ� ��Ҹ� �ٲٸ� �������̳� ũ�⸦ �ұ� ������ ������ ����
            Vector3 scale = cell.transform.localScale;
            Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;
            Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
            Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

            cell.transform.SetParent(_cellBase.transform.parent);

            // ���� �����ϰ� ũ�� ����
            cell.transform.localScale = scale;
            cell.CachedRectTransform.sizeDelta = sizeDelta;
            cell.CachedRectTransform.offsetMin = offsetMin;
            cell.CachedRectTransform.offsetMax = offsetMax;

            // ������ �ε����� ���� ����Ʈ �׸� �����ϴ� ���� ���� ����
            UpdateCellForIndex(cell, index_);

            _cells.AddLast(cell);

            return cell;
        }

        // �� ���� ���� �Լ�
        private void UpdateCellForIndex(UIRecycleViewCell<T> cell_, int index_)
        {
            // ���� �����ϴ� ����Ʈ �׸��� �ε��� ����
            cell_.Index = index_;

            if(cell_.Index >= 0 && cell_.Index <= tableData.Count - 1)
            {
                // ���� �����ϴ� ����Ʈ �׸��� �ִٸ� ���� Ȱ��ȭ�ؼ� ������ �����ϰ� ���� ����
                cell_.gameObject.SetActive(true);
                cell_.UpdateContent(tableData[cell_.Index]);
                cell_.Height = GetCellHeightAtIndex(cell_.Index);
            }
            else
            {
                // ���� �����ϴ� ����Ʈ �׸��� ������ ���� ��Ȱ��ȭ
                cell_.gameObject.SetActive(false);
            }
        }

        // VisibleRect�� �����ϱ� ���� �Լ�
        private void UpdateVisibleRect()
        {
            // visibleRect�� ��ġ�� ��ũ���� ������ �������κ��� ������� ��ġ
            _visibleRect.x = CachedScrollRect.content.anchoredPosition.x + _visibleRectPadding.left;
            _visibleRect.y = -CachedScrollRect.content.anchoredPosition.y + _visibleRectPadding.top;

            // visibleRect�� ũ��� ��ũ�� ���� ũ�� + �е�
            _visibleRect.width = CachedRectTransform.rect.width + _visibleRectPadding.left + _visibleRectPadding.right;
            _visibleRect.height = CachedRectTransform.rect.height + _visibleRectPadding.top + _visibleRectPadding.bottom;
        }

        // VisibleRect ������ ǥ�õ� ��ŭ�� ���� �����Ͽ� ��ġ�ϴ� �Լ�
        private void SetFillVisibleRectWithCells()
        {
            // ���� ���ٸ� �ƹ� �ϵ� ���� �ʴ´�
            if(_cells.Count < 1)
            {
                return;
            }

            // ǥ�õ� ������ ���� �����ϴ� ����Ʈ �׸��� ���� ����Ʈ �׸��� �ְ�
            // �� ���� visibleRect�� ������ ������ �����ϴ� �� �ۼ�
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

        // ��ũ�Ѻ䰡 �������� �� ȣ��Ǵ� �Լ�
        public void OnScrollPosChanged(Vector2 scrollPos_)
        {
            // visibleRect�� �����Ѵ�
            UpdateVisibleRect();
            // ���� �����Ѵ�
            UpdateCells((scrollPos_.y < _prevScrollPos.y) ? 1 : -1);

            _prevScrollPos = scrollPos_;
        }

        // ���� �����Ͽ� ǥ�ø� �����ϴ� �Լ�
        private void UpdateCells(int scrollDirection_)
        {
            if(_cells.Count < 1)
            {
                return;
            }

            if(scrollDirection_ > 0)
            {
                // ���� ��ũ�� �ϰ� ���� ���� visibleRect�� ������ �������� ���� �ִ� ����
                // �Ʒ��� ���� ������� �̵����� ���� ����
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

                // visibleRect�� ������ ���� �ȿ� �� ���� ������ �� �ۼ�
                SetFillVisibleRectWithCells();
            }
            else if(scrollDirection_ < 0)
            {
                // �Ʒ��� ��ũ���ϰ� ���� ���� visibleRect�� ������ �������� �Ʒ��� �ִ� ����
                // ���� ���� ������� �̵����� ���� ����
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