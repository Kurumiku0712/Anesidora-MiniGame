using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class RoomOrdinary : Room
{
    public GameObject[] monstersGroup;
    public GameObject[] doors;
  
    int monstersGroupNumber;
    List<Monster> monsters = new List<Monster>();
    public override void Initialization()
    {
        base.Initialization();
        //设置当前房间的怪物显示，读取当前怪物表，设置怪物的room
         monstersGroupNumber = 0;
        for (int i = 1; i < monstersGroup.Length; i++)
        {
            monstersGroup[i].SetActive(false);
        }
        LoadMonstersInGroup(monstersGroupNumber);
        
    }
    public override void UpdateRoomInfo()
    {
        base.UpdateRoomInfo();
        //如果怪物孵化器空，开门。
        if (monsters.Count == 0)
        {
            monstersGroupNumber++;
            if (monstersGroupNumber < monstersGroup.Length)
            {
                LoadMonstersInGroup(monstersGroupNumber);
                ActivationMonster();
            }
            else
            {
                
                foreach(GameObject door in doors)
                door.SetActive(true);
                //v
            }
        }
 

    }
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        // if (!isExplored)
        // {
        foreach (GameObject door in doors)
            door.SetActive(false);//v
                                 //  CloseDoor();
        ActivationMonster(); //v
      //  }
    }
    public override void MonsterDie(Monster monster)
    {
        monsters.Remove(monster);
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
    
}
