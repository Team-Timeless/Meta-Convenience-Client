using UnityEngine;
using UnityEngine.UI;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 30f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public ParticleSystem Stars;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    public ServerTime serverTime;       // <! 안쓰넹

    private bool isDayStart = false;
    private float times;
    
    public Text dayRatioText; 

    public void StartDay(float curSecond)
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;

        isDayStart = true;

        TimeOfDay = curSecond / DayDuration;
    }
    
    private void Update()
    {
        if (!isDayStart)
        {
            return;
        }
        
        TimeOfDay += Time.deltaTime / DayDuration;

        times += Time.deltaTime;
        
        // Debug.Log(times);
        if (TimeOfDay >= 1) TimeOfDay -= 1;

        // 조명 설정(스카이박스 및 주 태양)
        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.1f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();

        // 별 투명도
        var mainModule = Stars.main;
        //mainModule.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay));

        // 달과 태양의 자전
        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f + 180f, 180, 0);

        // 달과 태양의 빛의 강도
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);

        dayRatioText.text = "밤 낮 비율 : " + TimeOfDay + " / " + 1;
    }
}
