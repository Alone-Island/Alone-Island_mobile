using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ ����
    public GameObject bookPrefab;
    public GameObject bookAreaPrefab;

    // C : �� xxxxObject�� xxxxLearningObject�� �θ� obejct
    // C : å�� ������ �� ��� �н��ϱ� ��ҿ� �����Ǵ� ���� �����ϱ� ���� �ʿ��� �ۺ� ���� ����
    public GameObject farmObject;
    public GameObject farmLearningObject;
    // C : å�� ������ �� ���� �н��ϱ� ��ҿ� �����Ǵ� ���� �����ϱ� ���� �ʿ��� �ۺ� ���� ����
    public GameObject houseObject;
    public GameObject houseLearningObject;
    // C : å�� ������ �� ���� �н��ϱ� ��ҿ� �����Ǵ� ���� �����ϱ� ���� �ʿ��� �ۺ� ���� ����
    public GameObject craftObject;
    public GameObject craftLearningObject;
    // C : å�� ������ �� ���� �н��ϱ� ��ҿ� �����Ǵ� ���� �����ϱ� ���� �ʿ��� �ۺ� ���� ����
    public GameObject labObject;
    public GameObject labLearningObject;
    // C : å�� ������ �� ���� �÷��̾��� ��ġ�� �����Ǵ� ���� �����ϱ� ���� �ʿ��� �ۺ� ���� ����
    public GameObject playerObject;
    private GameObject tempObject;           // C : å�� ������ �� ���� �÷��̾��� ��ġ�� �����Ǵ� ���� �����ϱ� ���� �ʿ��� temp �ۺ� ���� ����

    public GameManager gameManager;

    int count = 4;                  // J : �� å ����
    private BoxCollider2D area;     // J : �ڽ� �ݶ��̴��� ������ �������� ���� ����
    private List<GameObject> bookList = new List<GameObject>();         // ������ book object�� �����ϱ� ���� ����Ʈ ����

    // J : count��ŭ å ����
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        // C : å�� ������ �� ���� �÷��̾��� ��ġ�� �����Ǵ� ���� �����ϱ� ���� �ʿ��� component instance(GameObject) ����
        tempObject = new GameObject("tempObject");

        StartCoroutine("Spawn");
    }

    // J : ���� ������Ʈ�� �����Ͽ� scene�� �߰�
    private IEnumerator Spawn()
    {
        for (int i = 0; i < count; i++) // J : count��ŭ å ����
        {
            Vector3 spawnPos = GetRandomPosition(); // J :���� ��ġ return

            // J : ����, ��ġ, ȸ������ �Ű������� �޾� å ������Ʈ ����
            // J : Quaternion.identity <- ȸ���� 0
            GameObject book = Instantiate(bookPrefab, spawnPos, Quaternion.identity);
            book.GetComponent<Renderer>().enabled = false;  // J : ������Ʈ�� ������ �ʵ���
            bookList.Add(book); // J : ������Ʈ ������ ���� ����Ʈ�� add
                        
            GameObject bookArea = Instantiate(bookAreaPrefab, spawnPos, Quaternion.identity);   // J : bookArea ������Ʈ ����
            bookArea.transform.SetParent(book.transform);   // J : book Object�� �ڽ� object�� ����
            bookArea.GetComponent<Renderer>().enabled = false;  // J : ������Ʈ�� ������ �ʵ���

        }
        area.enabled = false;       // J : BoxCollider2D ����
        yield return new WaitForSeconds(gameManager.day);   // J : �Ϸ� ����

        for (int i = 0; i < count; i++) // J : å ����
            Destroy(bookList[i].gameObject);

        bookList.Clear();           // J : bookList ����
        area.enabled = true;        // J : BoxCollider2D �ѱ�
        StartCoroutine("Spawn");    // J : å �ٽ� ����
    }

    // J : �� ���� ������ ��ġ�� return
    private Vector2 GetRandomPosition()
    {
        // C : �⺻���� ������ ��ġ �����ϴ� ����
        Vector2 basePosition = transform.position;  // J : ������Ʈ�� ��ġ
        Vector2 size = area.size;                   // J : box colider2d, �� ���� ũ�� ����

        // J : x, y�� ���� ��ǥ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);



        // C : book�� �����Ǹ� �ȵǴ� ��ġ�� �����ϴ� ����
        // C : ���� player ��ġ�� �����̿� book�� �������� �ʵ��� ����
        if (isLimit(tempObject, playerObject, spawnPos, 1))                     // C : ������ spawnPos�� ��ġ�� ���� player ��ġ�� �����̿� �����Ѵٸ�
        {
            return GetRandomPosition();                                         // C : book�� ������ ������ ��ġ(spawnPos) �����
        }
        // C : ��� �н��ϱ� ��ҿ� book�� �������� �ʵ��� ����
        else if (isLimit(farmObject, farmLearningObject, spawnPos, 0))          // C : ������ spawnPos�� ��ġ�� ��� �н��ϱ� ��ҿ� �����Ѵٸ�
        {
            return GetRandomPosition();                                         // C : book�� ������ ������ ��ġ(spawnPos) �����
        }
        // C : ���� �н��ϱ� ��ҿ� book�� �������� �ʵ��� ����
        else if (isLimit(houseObject, houseLearningObject, spawnPos, 0))        // C : ������ spawnPos�� ��ġ�� ���� �н��ϱ� ��ҿ� �����Ѵٸ�
        {
            return GetRandomPosition();                                         // C : book�� ������ ������ ��ġ(spawnPos) �����
        }
        // C : ���� �н��ϱ� ��ҿ� book�� �������� �ʵ��� ����
        else if (isLimit(craftObject, craftLearningObject, spawnPos, 0))        // C : ������ spawnPos�� ��ġ�� ���� �н��ϱ� ��ҿ� �����Ѵٸ�
        {
            return GetRandomPosition();                                         // C : book�� ������ ������ ��ġ(spawnPos) �����
        }
        // C : ���� �н��ϱ� ��ҿ� book�� �������� �ʵ��� ����
        else if (isLimit(labObject, labLearningObject, spawnPos, 0))            // C : ������ spawnPos�� ��ġ�� ���� �н��ϱ� ��ҿ� �����Ѵٸ�
        {
            return GetRandomPosition();                                         // C : book�� ������ ������ ��ġ(spawnPos) �����
        }

        return spawnPos;    // J : ���� ��ġ return
    }


    // C : ������ book�� ���� ��ġ�� ���� ������ ������ true, �ƴϸ� false ��ȯ�ϴ� �Լ�
    // C : 
    private bool isLimit(GameObject baseObject, GameObject learningObject, Vector3 spawnPos, double margin)
    {
        // C : book�� �����Ǹ� �ȵǴ� ���� ���ϱ�
        Vector3 basePos = baseObject.transform.localPosition;                       // C : �н��ϱ� ������ �θ� object�� original���� �����ϴ� ��ġ ���ϱ�
        Vector3 learningPos = learningObject.transform.localPosition;               // C : �н��ϱ� ������ �θ� object�� �������� �����ϴ� ��ġ ���ϱ�
        Vector2 learningSize = learningObject.GetComponent<BoxCollider2D>().size;   // C : �н��ϱ� ������ ũ�� ���ϱ�

        // C : book�� �����Ǹ� �ȵǴ� ������ (�����ϴ�/���� x��ǥ), (������/���� x��ǥ), (�����ϴ�/��� y��ǥ), (������/�ϴ� y��ǥ) ������� ����Ʈ�� �����ϱ�
        // C : ��� ���) �н��ϱ� ������ x/y ��ǥ��, �н��ϱ� ������ x/y ũ�⸦ ���ų� ���ϰ�, �θ� ������ x/y ��ǥ�� ���ų� ���ϰ�, �н��ϱ� ������ x/y ��ǥ�� �߽ɿ� �����ϱ� ������ 0.5�� ���ų� ���Ѵ�.
        // C : ��� ���) �߰��� ������ �ʿ��� ��� ������ ���ų� �����ش�.
        double[] learingArea = new double[] {learningPos.x - learningSize.x + basePos.x - 0.5 - margin,
                                           learningPos.x + learningSize.x + basePos.x + 0.5 + margin,
                                           learningPos.y - learningSize.y + basePos.y - 0.5 - margin,
                                           learningPos.y + learningSize.y + basePos.y + 0.5 + margin};

        // C : book�� �����Ǹ� �ȵǴ� ��ġ�� �ִ��� Ȯ��
        if (spawnPos.x >= learingArea[0] && spawnPos.x <= learingArea[1]
            && spawnPos.y >= learingArea[2] && spawnPos.y <= learingArea[3])
        {
            return true;        // C : �����Ǹ� �ȵǴ� ��ġ�� �����Ѵٸ� true ��ȯ
        }

        return false;           // C : �����Ǹ� �ȵǴ� ��ġ�� �������� �ʴ´ٸ� false ��ȯ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
