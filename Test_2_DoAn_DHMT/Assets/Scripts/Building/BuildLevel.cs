using System.Collections.Generic;
using UnityEngine;

public class BuildLevel : SaiBehaviour
{
    [SerializeField] protected List<Transform> levels;
    [SerializeField] protected int currentLevel = 0;

    protected override void OnEnable()
    {
        this.ShowBuilding();
    }

    protected override void LoadComponents()
    {
        this.LoadLevels();
    }

    protected virtual void LoadLevels()
    {
        if (this.levels.Count > 0) return;
        Transform buildTran = transform.Find("Buildings");
        foreach (Transform child in buildTran)
        {
            this.levels.Add(child);
            child.gameObject.SetActive(false);
        }

        this.levels[0].gameObject.SetActive(true);

        Debug.Log(transform.name + ": LoadBuildings");
    }

    /// <summary>
    /// Call from InvokeRepeating
    /// </summary>
    protected virtual void ShowNextBuild() //kiểm tra level hiện tại đã max chưa
    {
        if (this.currentLevel >= this.levels.Count - 2) return;

        this.currentLevel++;
        this.ShowBuilding();
    }

    public virtual void ShowLastBuild() //hiện level cuối cùng
    {
        this.currentLevel = this.levels.Count - 1;
        this.ShowBuilding();
    }

    protected virtual void ShowBuilding()
    {
        this.HideLastBuild();
        Transform currentBuild = this.levels[this.currentLevel]; //ngoặc vuông lấy ra phần tử
        currentBuild.gameObject.SetActive(true);
    }

    protected virtual void HideLastBuild() //ẩn cây level trước
    {
        int lastBuildIndex = this.currentLevel - 1;
        if (lastBuildIndex < 0) return;
        Transform lastBuild = this.levels[lastBuildIndex];
        lastBuild.gameObject.SetActive(false);
    }
}
