using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Vector3 recoil = new Vector3(-2.5f, 1, 1);
    [SerializeField] private float smoothRecoil = 10;
    [SerializeField] private float resetSpeed = 5;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, resetSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, smoothRecoil * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void AddForce()
    {
        targetRotation += new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
    }
}