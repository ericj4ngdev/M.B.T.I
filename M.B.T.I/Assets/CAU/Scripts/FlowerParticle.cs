using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerParticle : MonoBehaviour
{
    //�̵��� ���� ���� ������ ���� ����
    Vector2 direction;
    //�̵��ӵ� ������ ���� ����
    public float moveSpeed = 0.05f;

    //ũ�� ���� ���� �ִ��ּ� ����
    public float minSize = 5f;
    public float maxSize = 10f;
    public float sizeSpeed = 1;

    //���� �⺻�� ���� �� ���� ���� ����
    SpriteRenderer sprite;
    //color �迭 ����
    public Color[] colors;
    //���� ���� ����
    public float colorSpeed = 5;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        //ũ�� ���� ����
        float size = Random.Range(minSize, maxSize);
        //���� ����� size�� �ʱ�ȭ
        transform.localScale = new Vector2(size, size);
        //�÷� ���� ����
        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //������ ������ �����ϰ� ����
        transform.Translate(direction * moveSpeed);
        //ũ�Ⱑ 0�� ���������
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);

        //���� ���� ����
        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * colorSpeed);
        sprite.color = color;

        //������� �� ���� - ���İ� ������ ���� ���� �������� �ı�
        if (sprite.color.a <= 0.01f)
            Destroy(gameObject);
    }
}
