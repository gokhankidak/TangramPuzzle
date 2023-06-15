using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


namespace Insula.Tangram
{
    public class TangramPiece : MonoBehaviour
    {
        [SerializeField] private TangramSO tangramSo;
        [SerializeField] private bool isTriangle;
        private float gridSize;
        private Transform pivot;
        private Vector3 clickOffset, pivotOffset, snapOffset, firstClickPos;
        private bool isSelected;

        private void Start()
        {
            pivot = transform.GetChild(0);
            gridSize = tangramSo.gridSize;
            SetOffsets();
        }

        private void SetOffsets()
        {
            pivotOffset = isTriangle ? (pivot.position - transform.position) / 2 : pivot.position - transform.position;
            snapOffset = new Vector3(pivotOffset.x % gridSize, pivotOffset.y % gridSize);
        }

        private void OnMouseDown()
        {
            firstClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickOffset = firstClickPos - transform.position;
            clickOffset.z = 0;
            isSelected = true;
        }

        private void Rotate()
        {
            transform.Rotate(0f, 0f, 45f);
            SetOffsets();
        }


        private void Update()
        {
            if (!isSelected) return;
            if(firstClickPos == Camera.main.ScreenToWorldPoint(Input.mousePosition) )  return;
            
            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - clickOffset ;
            var snappedPos = new Vector3(Mathf.Round(clickPos.x / gridSize) * gridSize, Mathf.Round(clickPos.y / gridSize) * gridSize, transform.position.z) + snapOffset;
            transform.position = snappedPos;
        }

        private void OnMouseUp()
        {
            isSelected = false;
            if (firstClickPos == Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                transform.position -= snapOffset;
                Rotate();
            }
        }
    }
}