using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class MapSwitcher : MonoBehaviour
{
    public GameObject[] maps; // �� ������Ʈ �迭
    public Transform[] spawnPoints; // ��������Ʈ �迭
    public Transform player; // �÷��̾� Transform
    public TextMeshProUGUI stageText; // StageText UI ����
    public ClearPopupController clearPopupController; // �˾�â ��Ʈ�ѷ�

    private int currentMapIndex = 0; // ���� �� �ε���

    void Start()
    {
        // ��� �� ��Ȱ��ȭ
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }

        // ù �� Ȱ��ȭ
        currentMapIndex = 0;
        maps[currentMapIndex].SetActive(true);

        SetPlayerPosition();
        UpdateStageText();
    }

    public void EnterDoor(bool isCorrect)
    {
        if (isCorrect)
        {
            MoveToNextMap();
        }
        else
        {
            ResetToFirstMap();
        }
    }

    private void MoveToNextMap()
    {
        maps[currentMapIndex].SetActive(false); // ���� �� ��Ȱ��ȭ
        currentMapIndex++;

        if (currentMapIndex >= maps.Length)
        {
            // ������ ���� �Ѿ�� ���� ���� ó��
            clearPopupController.ShowClearPopup();
            return;
        }

        maps[currentMapIndex].SetActive(true); // ���� �� Ȱ��ȭ
        SetPlayerPosition();
        UpdateStageText();
    }

    private void ResetToFirstMap()
    {
        // ��� �� ��Ȱ��ȭ
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }

        currentMapIndex = 0; // ù ������ ���ư���
        maps[currentMapIndex].SetActive(true);

        SetPlayerPosition();
        UpdateStageText();
    }

    private void SetPlayerPosition()
    {
        Vector3 spawnPosition = spawnPoints[currentMapIndex].position;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        player.position = spawnPosition;

        if (rb != null)
        {
            rb.simulated = true;
        }
    }

    private void UpdateStageText()
    {
        if (stageText != null)
        {
            stageText.text = $"Stage {currentMapIndex + 1}";
        }
    }
}