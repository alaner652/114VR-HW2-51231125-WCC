using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    void Update()
    {
        // 按 A 或 左鍵 → 往左移
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * 5 * Time.deltaTime;

        // 按 D 或 右鍵 → 往右移
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * 5 * Time.deltaTime;

        // 按 W 或 上鍵 → 往上移
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.position += Vector3.up * 5 * Time.deltaTime;

        // 按 S 或 下鍵 → 往下移
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.down * 5 * Time.deltaTime;
    }
}
