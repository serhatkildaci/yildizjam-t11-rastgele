
using UnityEngine;

public class Trompe : MonoBehaviour
{
    public GameObject target; // Boyutları ve konumu değiştirilecek nesne

    private void LateUpdate()
    {
        // Hedef nesnenin konumunu ve boyutlarını alıyoruz.
        Vector3 targetPos = target.transform.position;
        Vector3 targetScale = target.transform.lossyScale;

        // Trompe L'oeil nesnesinin konumunu ve boyutlarını ayarlıyoruz.
        transform.position = targetPos;
        transform.localScale = targetScale;

        // Hedef nesnenin dünya koordinatlarındaki pozisyonunu alıyoruz.
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(targetPos);

        // Kamera ile hedef nesne arasındaki uzaklığı hesaplıyoruz.
        float distance = Vector3.Distance(Camera.main.transform.position, targetPos);

        // Hedef nesne ile kamera arasındaki açıyı hesaplıyoruz.
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 targetForward = targetPos - Camera.main.transform.position;
        float angle = Vector3.Angle(camForward, targetForward);

        // Yeni boyutlarımızı hesaplıyoruz.
        Vector3 newScale = Vector3.one * (distance / 10f) * Mathf.Sin(Mathf.Deg2Rad * angle);

        // Trompe L'oeil nesnesinin boyutlarını ayarlıyoruz.
        transform.localScale = newScale;
    }
}

