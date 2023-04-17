using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    private float maxLevelMultiply = 1f;
    private float currentBalance = 0f;
    private int numOfClicks = 0;
    private float clickMultiply = 1f;
    private float extraCase = 0f;
    private int extraCaseChance = 1;
    private int completedAchievements = 0;
    private float achievements = 0f;

    private int countOpenBusiness = 0;
    //Skills
    private int timeOfAction = 0;
    private int cooldown = 0;
    public float Balance { get; set; }

    public float firstLevelOfBusiness()   //Вызывается каждую итерацию бизнеса +++ 
    {
        //Из-вне увеличить кол-во открытых бизнесов и присвоить countOpenBusiness
        bool[] achieveComplete = new bool[5] {false, false, false, false, false };


        float iterationMultiply = 1f;
        float chance = Random.Range(countOpenBusiness, 10); // countOpenBusiness = 1 = 10%;
        if (10 == chance)
        { 
            switch (countOpenBusiness)
            {
                case 1:
                    iterationMultiply = Random.Range(120, 200);
                    
                    if (achieveComplete[1] == false)
                    { 
                        achieveComplete[1] = true;
                        achieveEverything();
                    }
                    break;
                case 2:
                    iterationMultiply = Random.Range(150, 250);
                    break;
                case 3:
                    iterationMultiply = Random.Range(200, 300);
                    break;
                case 4:
                    iterationMultiply = Random.Range(400, 600);
                    break;
                case 5:
                    iterationMultiply = Random.Range(800, 1000);
                    break;
            }
        }
        return iterationMultiply;
    }

    public float maxBusinessLevel(int value, bool maxLevel)   // (Параметры вручную для каждого предприятия, а maxLevel передаётся через переменную определённого бизнеса) +++ 
    {
        if (maxLevel)
        {
            achieveEverything();
            return value;
        }

        /*    
        BoneWeight1 achieveEverything(1.5, maxLevel);
        BoneWeight2 achieveEverything(2, maxLevel);
        BoneWeight3 achieveEverything(4, maxLevel);
        BoneWeight4 achieveEverything(5, maxLevel);
        BoneWeight5 achieveEverything(10, maxLevel);
        */

        return 1;

        /*
        switch (value)
        {
            case 1:
                if (maxLevelMultiply < 1.5f)
                    return 1.5f;    //Для первого бизнеса выставляем этот множитель если макс уровень
                break;
            case 2:
                if (maxLevelMultiply < 2)
                    maxLevelMultiply = 2;
                break;
            case 3:
                if (maxLevelMultiply < 3)
                    maxLevelMultiply = 3;
                break;
            case 4:
                if (maxLevelMultiply < 5)
                    maxLevelMultiply = 5;
                break;
            case 5:
                if (maxLevelMultiply < 10)
                    maxLevelMultiply = 10;
                break;
        }
        
        return maxLevelMultiply;
    */
    }

    public void amountOfMoney(float value)  //Привязана к балансу (Если Выполнено, то функцию отключить)    ---
    {
        currentBalance = value;
        if (currentBalance <= 500)
        {
            timeOfAction = 3;
            cooldown = 8;
            achieveEverything();
        }
        else if (currentBalance <= 10_000)
        {
            timeOfAction = 5;
            cooldown = 7;
            achieveEverything();
        }
        else if (currentBalance <= 500_000)
        {
            timeOfAction = 7;
            cooldown = 6;
            achieveEverything();
        }
        else if (currentBalance <= 1_000_000)
        {
            timeOfAction = 10;
            cooldown = 5;
            achieveEverything();
        }
        else if (currentBalance <= 5_000_000)
        {
            timeOfAction = 15;
            cooldown = 4;
            achieveEverything();
        }
    }
    /*
    private bool isgoing = false;
    public float productivity()
    {
        float t = 0;
        
        if (timeOfAction > t && isgoing)
        {
            t = Time.deltaTime;
        }

        if (cooldown > t && !isgoing)
        {
            t = Time.deltaTime;
        }
        return 0;
    }
    */
    public float numberOfClicks() //Привязана к нажатию кнопки  +++ (Если Выполнено, то функцию отключить)
    {
        numOfClicks++;
        switch(numOfClicks) 
        {    
            case 100:
                clickMultiply = 1.1f;
                achieveEverything();
                break;
            case 1000:
                clickMultiply = 1.3f;
                achieveEverything();
                break;
            case 2500:
                clickMultiply = 1.5f;
                achieveEverything();
                break;
            case 5000:
                clickMultiply = 1.8f;
                achieveEverything();
                break;
            case 10_000:
                clickMultiply = 2f;
                achieveEverything();
                break;
        }
        return clickMultiply * achievements;
    }

    public float OpenCase(float сaseСost)   // Кнопка +++
    {
        var extra = 1;
        if (100 - extraCaseChance <= Random.Range(1, 100))
            extra = 3;
        // 1500 *= 0,4
        // 600 *= 0,4
        // 240 *= 1,5 * 3
        //
        //
        //

        var reward = Random.Range(1, 100);      //Кейс стоит 0,5% от баланса          1500  //Округление 
        if (60 <= reward)                       //60%
            сaseСost *= 0.4f * extra;
        else if (60 > reward && reward <= 85)   //25%
            сaseСost *= 1.5f * extra;
        else if (85 > reward && reward <= 95)   //10%
            сaseСost *= 3 * extra;
        else if (95 > reward && reward <= 99)   //4%
            сaseСost *= 5 * extra;
        else if (100 == reward)                 //1%
            сaseСost *= 20 * extra;

        numberOfOpenCases();
        //Тут вывод на панель
        //outputResult();
        return сaseСost;
    }

    public int numberOfOpenCases()  //Привязана к нажатию кнопки +++ 
    {
        extraCase++;
        switch (extraCase)
        {
            case 10:
                extraCaseChance = 2;
                achieveEverything();
                break;
            case 50:
                extraCaseChance = 5;
                achieveEverything();
                break;
            case 100:
                extraCaseChance = 8;
                achieveEverything();
                break;
            case 250:
                extraCaseChance = 10;
                achieveEverything();
                break;
            case 1000:
                extraCaseChance = 15;
                achieveEverything();
                break;
        }
        return extraCaseChance;
    }

    
    public float achieveEverything()    //Вызывается при выполнении ачивки
    {
        completedAchievements++;

        achievements *= (1 + (completedAchievements * 10 /100)); //10% - Процент за ачивку 1,1

        return achievements;
    }
    
}
