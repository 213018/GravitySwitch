# GravitySwitch

## 1.Опис на проблемот
Замислата на овој проект е да се имплементира едноставен 2D платформер. Целта на играчот е да ги собере сите кристали и да стигне до обележаната крајна дестинација движејќи се ни терен исполнет со препреки, кои го принудуваат играчот да је менува гравитацијата. Главниот проблем на играчот е тоа што при допир на некоја na препреките тој губи "Health", а при повеќе такви судири се враќа на почетокот од левелот.  Играчот не е временски ограничен и цели на завршување на двата левели со сите собрани кристали. 
## 2.Решение на проблемот
Решението се состои од 7 класи.
Во PlayerMovement класата се поставуваат пареметрите на играчот како што се слика, позиција насока, брзина на движење, висина со која ќе скока. Исто така има посебна функција со која се проверува со какви објекти играчот има интеркација, за пример ќе го земеме следниот код каде се проверува дали игачот доаѓа во допир со некој од препреките
```
else if (collision.tag == "Spike")
        {
            healthBar.Damage(0.25f);
        }
```
При што се одзема “Health” од неговиот HealthBar.
Тоа е реализирано во функциите Health и HealthBar.
```
public class Health : MonoBehaviour
{
   public static float totHealth = 1f;
}

public void Damage(float dmg){
        if ((Health.totHealth - dmg)  > 0 )
        {
            Health.totHealth -= dmg;
        }
        else {
            Health.totHealth = 0;
        }
        
        if (Health.totHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        
        UpdateSize(Health.totHealth);
    }

    public void UpdateSize(float size)
    {
        hBar.localScale = new Vector3(size, 1f);
    }
```
Во GameManager класата се состои од два Game објеки и повеќе функции кои се повикуваат при клик на копчињата што ги имаат Главното Мени, менито што се појавува при губење на целиот “Health”, односно GameOver менито и менито при успешно поминување на двата левели.
## 3.Изглед на играта
### --Почетно Мени
![Screenshot_1](https://github.com/213018/GravitySwitch/assets/129883425/1c1db7ff-0241-4feb-bd8f-ff05a86d79ce)
### --How to play
 ![Screenshot_4](https://github.com/213018/GravitySwitch/assets/129883425/466937b6-da2e-4bd5-8a3c-4d22ebab1960)
### --Левел 1
 ![Screenshot_2](https://github.com/213018/GravitySwitch/assets/129883425/12fc4aee-afaa-42b6-94cd-7c5e54d90ba2)
### --GameOver
 ![Screenshot_3](https://github.com/213018/GravitySwitch/assets/129883425/1ce41d92-ddaf-4400-9d3f-f13806a650ac)
### --YouWon
 ![Screenshot_5](https://github.com/213018/GravitySwitch/assets/129883425/f330fab3-f0a5-41eb-8aa3-018ad1da8a20)

