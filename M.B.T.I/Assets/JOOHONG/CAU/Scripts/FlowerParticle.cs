using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerParticle : MonoBehaviour
{
    //이동할 방향 정보 가지는 변수 선언
    Vector2 direction;
    //이동속도 조절할 변수 선언
    public float moveSpeed = 0.05f;

    //크기 랜덤 조절 최대최소 변수
    public float minSize = 5f;
    public float maxSize = 10f;
    public float sizeSpeed = 1;

    //별의 기본색 변경 및 투명 설정 변수
    SpriteRenderer sprite;
    //color 배열 선언
    public Color[] colors;
    //투명도 조절 변수
    public float colorSpeed = 5;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        //크기 랜덤 조절
        float size = Random.Range(minSize, maxSize);
        //로컬 사이즈를 size로 초기화
        transform.localScale = new Vector2(size, size);
        //컬러 랜덤 설정
        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //실행할 때마다 랜덤하게 생성
        transform.Translate(direction * moveSpeed);
        //크기가 0에 가까워지게
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);

        //투명도 조절 설정
        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * colorSpeed);
        sprite.color = color;

        //쓸모없는 별 삭제 - 알파값 가지고 일정 이하 떨어지면 파괴
        if (sprite.color.a <= 0.01f)
            Destroy(gameObject);
    }
}
