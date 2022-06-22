using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SliderScript : MonoBehaviour{
	[SerializeField] private SpriteRenderer spriteBackGround, spriteFill;
	[SerializeField] private Transform fillTransform;
	[SerializeField] private float speedFill;
	[SerializeField] private Color lowValue, highValue;
	[SerializeField] private float currentAmount = 1f;
	private Coroutine coroutineFill;
	public void Fill(float targetAmount){
		if(coroutineFill != null){
			StopCoroutine(coroutineFill);
			coroutineFill = null;
		}
		coroutineFill = StartCoroutine(IFillSlider(targetAmount));
	}
	public void Refresh(){
		if(coroutineFill != null){
			StopCoroutine(coroutineFill);
			coroutineFill = null;
		}
		currentAmount = 1f;
		scaleFill.x = 1f;
		posFill.x   = 0;
		fillTransform.localScale      = scaleFill;
		fillTransform.localPosition   = posFill;
		spriteFill.color = highValue;
	}
	Vector3 posFill = new Vector3(0, 0, 0), scaleFill = new Vector3(1, 1, 1);
	IEnumerator IFillSlider(float targetAmount){
		float speed = (targetAmount > currentAmount) ? speedFill : -speedFill;
		while(((currentAmount > targetAmount)&&(speed < 0))||((currentAmount < targetAmount)&&(speed > 0))){
			scaleFill.x  = currentAmount;
			spriteFill.color = Color.Lerp(lowValue, highValue, currentAmount);
			posFill.x    = 0.32f * currentAmount - 0.32f;
			fillTransform.localScale = scaleFill;
			fillTransform.localPosition   = posFill;
			currentAmount += speed;
			yield return null;
		}
		currentAmount = targetAmount;
	}
}