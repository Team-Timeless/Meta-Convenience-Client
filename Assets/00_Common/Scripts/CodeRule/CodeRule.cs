using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코드 규칙 (주석 잘쓰기!)

public class CodeRule : MonoBehaviour
{
    // 변수는 알아서 판단해서 주석달기 (굳이 일일히 안달아도 된다는 뜻)

    // 변수 이름은 소문자 시작 
    // 외부 클래스 생성 변수(?), 상속 멤버변수는 _(언더바) 시작
    // private 이면 명시해주기
    // 변수 생성후 초기화 자료형에 따라서 0, 0.0f, null, "" 초기화 해주기 (초기값은 Awake, Start, 생성자 에서)
    // ex)
    int a = 0;      // x
    private int b = 0;

    // var 사용 x
    // 왠만하면 find 함수 사용 x

    // ----

    // 함수 이름은 대문자 시작
    // 함수 주석
    /**
     * @bref 간단한 함수 설명
     * @param 인자값 쓰고 인자값에 대한 설명 (인자값이 두개이상이다 그럼 여러줄 쓰기)
     */

    // ex)
    /**
     * @bref a랑 b를 더하는 함수
     * @param int a 첫번째 값
     * @param int b 두번째 값
     */
    void Add(int a, int b)
    {
    }

    // 기억이안남... 암튼 이정도
}
