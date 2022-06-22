using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeResource{
	Gold = 0,
	ManaPoint = 1,
	Animals = 2,
	Jewerly = 3,
	Silk = 4
}


[System.Serializable]
public class Resource : ICloneable{
	public TypeResource Name;
	[SerializeField]
	protected float count = 0;
	public float Count{ get => count; set => count = value; }
	[SerializeField]
	protected int e10 = 0;
	public int E10{get => e10;  set => e10 = value;}
	public bool PosibleNegative = true;
	public Resource(){
		Name  = TypeResource.Gold;
		count = 0;
		e10   = 0;
	}
	public Resource(TypeResource name, float count = 0f, int e10 = 0){
		this.Name  = name;
		this.count = count;
		this.e10   = e10;
		NormalizeResource();
	}
	
	private void NormalizeResource(){
		if((PosibleNegative == false) && (count < 0)) count = 0;
		while(this.count > 10000){
			this.e10   += 3;  
			this.count *= 0.001f;
		}
		while ((this.count < 1) && (e10 > 0)){
			this.e10 -= 3;
			this.count *= 1000f;
		}
	}
//API
	public override string ToString(){
		return FunctionHelp.BigDigit(this.count, this.E10);
	}
	public bool CheckCount(int count, int e10){
		bool result = false;
		if(this.e10 != e10){
			if(this.e10 > e10){
				if(this.count * (float) Mathf.Pow(10, this.e10 - e10) >= count)
					result = true;
			}else{
				if(this.count >= count * (float) Mathf.Pow(10, e10 - this.e10))
					result = true;
			}
		}else{
			if(this.count >= count){
				result = true;
			}
		}
		return result;
	}
	public void AddResource(float count, float e10 = 0){
		this.count += count * (float) Mathf.Pow(10f, e10 - this.e10);
		NormalizeResource();
	}
	public void SubtractResource(float count, float e10 = 0){
		this.count -= count * (float) Mathf.Pow(10f, e10 - this.e10); 
		NormalizeResource();
	}
	public bool CheckCount(Resource res){
		bool result = false;
		if(this.e10 != res.E10){
			if(this.e10 > res.E10){
				if(this.count * (float) Mathf.Pow(10, this.e10 - res.E10) >= res.Count)
					result = true;
			}else{
				if(this.count >= res.Count * (float) Mathf.Pow(10, res.E10 - this.e10))
					result = true;
			}
		}else{
			if(this.count >= res.Count){
				result = true;
			}
		}
		return result;
	}
	public void AddResource(Resource res){
			this.count += res.Count * (float) Mathf.Pow(10f, res.E10 - this.E10);
			NormalizeResource();
	}
	public void SubtractResource(Resource res){
		this.count -= res.Count * (float) Mathf.Pow(10f, res.E10 - this.E10); 
		NormalizeResource();
	}
	public void Clear(){
		this.count = 0;
		this.e10   = 0;
	}

//Operators
	public static Resource operator* (Resource res, float k){
		Resource result = new Resource(res.Name, Mathf.Ceil(res.Count * k), res.E10);
		return result;
	}	
//Image
	private static Sprite[] spriteAtlas;   
	private Sprite image;
	public Sprite sprite{ 
							get{ 
								if(image == null){
									if(spriteAtlas == null) spriteAtlas = Resources.LoadAll<Sprite>("Sprites/Resources");
									for(int i=0; i < spriteAtlas.Length; i++){
										if((Name.ToString()).Equals(spriteAtlas[i].name)){
											image = spriteAtlas[i];
											break;
										}
									}
								}
								return image;
							}
						}
	public object Clone(){
        return new Resource  { 
        	Name = this.Name,
        	Count = this.count,
        	E10  = this.e10
        };
    }    		
}

public interface ICloneable{
    object Clone();
}