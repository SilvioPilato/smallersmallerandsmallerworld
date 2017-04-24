using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolShoot : MonoBehaviour {

	public Text bulletText;
	public LayerMask enemyMask;
	public float shootingRange;
	public int clipSize = 6;
    public int pistolDamage = 1;
	int bulletsInClip = 6;
    

	private bool reloading = false;
	public float reloadTime=2f;
	float reloadTimer;

	private void Start()
	{
        UpgradeBulletsText(bulletsInClip.ToString());
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && bulletsInClip>0)
		{
			bulletsInClip--;
            UpgradeBulletsText(bulletsInClip.ToString());

            Ray shootingRay = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(shootingRay, out hitInfo, shootingRange, enemyMask))
			{
                hitInfo.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(pistolDamage);
			}
            
            AudioManager.manager.OnShoot();

		}
		else if (Input.GetButtonDown("Fire1") && !reloading)
		{
			
			reloading = true;
			StopAllCoroutines();
			StartCoroutine(ReloadClip());
		}
	}

	public IEnumerator ReloadClip()
	{
		while (reloadTimer < reloadTime)
		{
            UpgradeBulletsText("Reloading");
            reloadTimer += Time.deltaTime;
			yield return null;
		}
		bulletsInClip = clipSize;
		UpgradeBulletsText(bulletsInClip.ToString());
		reloadTimer = 0;
		reloading = false;
		
		yield return null;
		
	}



	private void UpgradeBulletsText(string text)
	{
        bulletText.text = text;
	}
}
