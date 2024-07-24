using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : Room
{
    public Boss Boss;
   // public GameObject endDoor;
    public GameObject healthBar;

    public GameObject[] monstersGroup;
    public GameObject[] littleMapmonstersGroup;
    int monstersGroupNumber;
    List<Monster> monsters = new List<Monster>();

    public override void Initialization()
    {
        base.Initialization();
        Boss.Initialization(this);

        //设置当前房间的怪物显示，读取当前怪物表，设置怪物的room
        monstersGroupNumber = 0;
        for (int i = 0; i < monstersGroup.Length; i++)
        {
            monstersGroup[i].SetActive(false);
        }
    }

    void LoadMonstersInGroup(int num)
    {
        //相当于生成怪物
        monstersGroup[num].SetActive(true);
        //将怪物存储到
        Monster[] theMonsters = monstersGroup[num].GetComponentsInChildren<Monster>();
        for (int i = 0; i < theMonsters.Length; i++)
        {
            monsters.Add(theMonsters[i]);
            theMonsters[i].Initialization(this);//标记怪物属于该房间
        }
    }

    public override void UpdateRoomInfo()
    {
        base.UpdateRoomInfo();

        if (Boss.hp<=Boss.currentHP/2)
        {
            LoadMonstersInGroup(monstersGroupNumber);
            ActivationMonster();

        }
    }

    /// <summary>
    /// 唤醒房间中的怪物
    /// </summary>
    void ActivationMonster()
    {
        foreach (Monster monster in monsters)
        {
            monster.isStart = true;
        }
    }

    public override void MonsterDie(Monster monster)
    {
        monsters.Remove(monster);
    }

    public override void BossDie(Boss boss)
    {
        base.BossDie(boss);
       // endDoor.SetActive(true);
        isExplored = true;

        foreach (Monster monster in monsters)
        {
            monster.die();

        }
        foreach (GameObject items in littleMapmonstersGroup)
        {
            items.SetActive(false);

        }
    }
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        if (!isExplored)
        {
            //CloseDoor();
            Boss.isStart = true;
            healthBar.SetActive(true);
        }
    }
}
