using UnityEngine;
using System.Collections;

public class CaracController : MonoBehaviour {

	// life
	// y = ax + b
	private float lifeB = 200.0f;
	private float lifeA = 10.0f;

	// regen
	// y = ax + b
	private float regenB = 3.0f;
	private float regenA = 1.0f;

	// damage
	// y = ax + b
	private float damageB = 100.0f;
	private float damageA = 10.0f;

	// fire rate
	// y = 1 / (ax + b)
	private float frateB = 10.0f;
	private float frateA = 1.0f;

	// extra shot
	// y = (x >= a) ? 3 : 2
	private int shotA = 10;

	// cost
	// c = ax + b
	private float costB = 0.0f;
	private float costA = 10000.0f;

	


	public int Cost(int level)
	{
		return (int)(costB + costA * (level+1));
	}


	public void UpdateLife()
	{
		int lvl = DataController.UpLife;

		if (DataController.GlobalScore >= Cost (lvl))
		{
			DataController.GlobalScore -= Cost (lvl);
			DataController.UpLife++;
		}
	}

	public void UpdateRegen()
	{
		int lvl = DataController.UpRegen;
		
		if (DataController.GlobalScore >= Cost (lvl))
		{
			DataController.GlobalScore -= Cost (lvl);
			DataController.UpRegen++;
		}
	}

	public void UpdateDamage()
	{
		int lvl = DataController.UpDmg;
		
		if (DataController.GlobalScore >= Cost (lvl))
		{
			DataController.GlobalScore -= Cost (lvl);
			DataController.UpDmg++;
		}
	}

	public void UpdateFireRate()
	{
		int lvl = DataController.UpFireRate;
		
		if (DataController.GlobalScore >= Cost (lvl))
		{
			DataController.GlobalScore -= Cost (lvl);
			DataController.UpFireRate++;
		}
	}
	public void UpdateShot()
	{
		int lvl = DataController.UpShot;
		
		if (DataController.GlobalScore >= Cost (lvl))
		{
			DataController.GlobalScore -= Cost (lvl);
			DataController.UpShot++;
		}
	}



	public float GetLife()
	{
		return GetLife (DataController.UpLife);
	}
	public float GetLife(int level)
	{
		return lifeB + lifeA * level;
	}

	public float GetRegen()
	{
		return GetRegen(DataController.UpRegen);
	}
	public float GetRegen(int level)
	{
		return regenB + regenA * level;
	}

	public float GetDamage()
	{
		return GetDamage(DataController.UpDmg);
	}
	public float GetDamage(int level)
	{
		return damageB + damageA * level;
	}

	public float GetFireRate()
	{
		return GetFireRate(DataController.UpFireRate);
	}
	public float GetFireRate(int level)
	{
		return 1.0f / (frateB + frateA * level);
	}

	public int GetShot()
	{
		return GetShot(DataController.UpShot);
	}
	public int GetShot(int level)
	{
		return (level >= shotA) ? 3 : 2;
	}

	public string GetLabelLife()
	{
		return GetLabel("Max life", 
		                DataController.UpLife,
		                "Maximum life is " + GetLife() + " HP");
	}

	public string GetLabelRegen()
	{
		return GetLabel("Life regenration",
		                DataController.UpRegen,
		                "Regenerate " + GetRegen() + " HP/s");
	}

	public string GetLabelDamage()
	{
		return GetLabel("Damage",
		                DataController.UpDmg,
		                "Make " + GetDamage() + " damage");
	}
	public string GetLabelFireRate()
	{
		return GetLabel("Fire rate",
		                DataController.UpFireRate,
		                "Fire " + (1.0f/GetFireRate()) + " shots / s");
	}

	public string GetLabelExtraShot()
	{
		int c = Cost (DataController.UpShot);
		return "<b><color=teal>Extra shot</color></b>\n"
			+ "<color=" + ((DataController.GlobalScore >= c) ? "green" : "red") + ">[Points: " + c.ToString("0,0") + "]</color>\n"
			+ "<i>Fire 3 shots instead of 2</i>";
	}

	
	string GetLabel(string title, int level, string desc)
	{
		int c = Cost (level);
		return "<b><color=teal>" + title + "</color></b>\n"
			+ "<color=" + ((DataController.GlobalScore >= c) ? "green" : "red") + ">[lvl: " + level + " "
				+ "Points: " + c.ToString("0,0") + "]</color>\n"
			+ "<i>" + desc + "</i>";
	}
}
