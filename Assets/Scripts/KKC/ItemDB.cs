using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDB
{
    private Dictionary<int, ClassData> charClass = new();

    public ClassDB()
    {
        var res = Resources.Load<ClassDBSheet>("DB/ClassDBSheet");
        var classSO = Object.Instantiate(res);
        var entities = classSO.Class_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for(int i = 0; i < entityCount; i++)
        {
            var charClass = entities[i];

            if (this.charClass.ContainsKey(charClass.id))
                this.charClass[charClass.id] = charClass;
            else
                this.charClass.Add(charClass.id, charClass);
        }
    }

    public ClassData Get(int id)
    {
        if (charClass.ContainsKey(id))
            return charClass[id];

        return null;
    }
}