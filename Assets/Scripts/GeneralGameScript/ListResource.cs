using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ListResource : ICloneable{

	[SerializeField]
	private List<Resource> resources = new List<Resource>();
	public List<Resource> List{get => resources; set => resources = value;}
	public int Count{get => List.Count;}
	public bool CheckResource(Resource res){ return CheckResource(new ListResource(res)); }
	public bool CheckResource(ListResource resources){
		bool result = true;
		foreach (Resource res in resources.List){
			if(GetResource(res.Name).CheckCount(res) == false){
				result = false;
				break;
			}
		}
		return result;
	}
	public void AddResource(Resource res){
		GetResource(res.Name).AddResource(res);
		UpdateResource();
	}
	public void AddResource(ListResource listResource){
		foreach (Resource res in listResource.List){
			GetResource(res.Name).AddResource(res);
		}
		UpdateResource();
	}

	public void SubtractResource(Resource res){
		GetResource(res.Name).SubtractResource(res);
		UpdateResource();
	}
	public void SubtractResource(ListResource resources){
		foreach (Resource res in resources.List){
			GetResource(res.Name).SubtractResource(res);
		}
		UpdateResource();
	}

	public void SetResource(Resource resource){
		Resource selectResource = GetResource(resource.Name);
		selectResource = resource;
		resources.Add(selectResource);
		UpdateResource();
	}
	public Resource GetResource(TypeResource name){
		Resource result = null;
		foreach (Resource res in resources){
			if(res.Name == name){
				result = res;
				break;
			}
		}
		if(result == null){result = new Resource(name); resources.Add(result);}
		return result;
	}
	public Resource GetResource(int num){
		Resource result = null;
		if(num < resources.Count){
			result = resources[num];
		}
		return result;
	}

	public ListResource(Resource res){
		SetResource(res);
	}
	public ListResource(params Resource[] resources){
		for(int i=0; i < resources.Length; i++){
			SetResource(resources[i]);
		}
	}
	public ListResource(List<Resource> listResource){
		foreach (Resource res in listResource) {
			SetResource(res);
		}
	}
	public ListResource(ListResource listResource){
		foreach (Resource res in listResource.List) {
			SetResource(res);
		}
	}
	public ListResource(){}
	public void Clear(){
		resources.Clear();
	}

	//Operators
	public static ListResource operator* (ListResource resources, float k){
		ListResource result = new ListResource(resources);
		for (int i = 0; i < result.List.Count; i++) {
			result.List[i] = result.List[i] * k;
		}
		return result;
	}


	public object Clone(){
		List<Resource> listRes = new List<Resource>();
		for(int i=0; i < this.resources.Count; i++)
			listRes.Add((Resource) this.resources[i].Clone());
        return new ListResource {resources =  listRes};				
    }

//Observer
	List<ObserverResource> observersResource = new List<ObserverResource>();
    public void UpdateResource(){
		foreach(Resource res in List){
			foreach(ObserverResource obs in observersResource){
				if(res.Name == obs.typeResource){
					obs.ChangeResource(res);
					break;					
				}
			}
		}
	}
    public void RegisterOnChangeResource(Action<Resource> d, TypeResource type){
		bool findObserver = false;
		ObserverResource observer = new ObserverResource(TypeResource.Gold);
		foreach(ObserverResource obs in observersResource){
			if(obs.typeResource == type){
				findObserver = true;
				observer = obs;
				break;
			}
		}
		if(findObserver == false) {
			observer = new ObserverResource(type);
			observersResource.Add(observer);
		}	
		observer.RegisterOnChangeResource(d);
	}
	public void RegisterOnChangeResource(Action<Resource> d, ListResource listRes){
		foreach(Resource res in listRes.List){
			RegisterOnChangeResource(d, res.Name);
		}
	}
	public void UnRegisterOnChangeResource(Action<Resource> d, TypeResource type){
		foreach(ObserverResource obs in observersResource){
			if(obs.typeResource == type){
				obs.UnRegisterOnChangeResource(d);
				break;
			}
		}
	}
	public void UnRegisterOnChangeResource(Action<Resource> d, ListResource listRes){
		for(int i = 0; i < listRes.Count; i++){
			UnRegisterOnChangeResource(d, listRes.List[i].Name);
		}
	}
}


public class ObserverResource{
	public TypeResource typeResource;
	public Action<Resource> delObserverResource;
	public ObserverResource(TypeResource type){
		typeResource = type;
	}

	public void RegisterOnChangeResource(Action<Resource> d){
		delObserverResource += d;
	}
	public void UnRegisterOnChangeResource(Action<Resource> d){
		delObserverResource -= d;
	}
	public void ChangeResource(Resource res){
		if(delObserverResource != null)
			delObserverResource(res);
	}
}