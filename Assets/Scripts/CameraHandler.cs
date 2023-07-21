using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthograhicsize;
    private float targetOrthograhicsize;

    private void Start() {
        orthograhicsize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }
    private void Update() {
    
    MovementHandler();
    ZoomHandler();

  }
  private void MovementHandler()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    Vector3 movDir = new Vector3(x,y).normalized;

    float moveSpeed =20f;
    transform.position += movDir*moveSpeed* Time.deltaTime;
  }
   private void ZoomHandler()
   {
    float zoomSpeed = 2f;
    targetOrthograhicsize += Input.mouseScrollDelta.y* zoomSpeed;
    targetOrthograhicsize = Mathf.Clamp(targetOrthograhicsize , 10f, 30f);
    float zoomAmount = 5f;
    orthograhicsize = Mathf.Lerp(orthograhicsize,targetOrthograhicsize,Time.deltaTime*zoomAmount);
    cinemachineVirtualCamera.m_Lens.OrthographicSize = orthograhicsize;
   }
}
