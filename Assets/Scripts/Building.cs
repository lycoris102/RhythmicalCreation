using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public int size;
    public ParticleSystem buildingParticle;
    public bool isPushed = false;

    public void PlayParticle () {
        buildingParticle.Play();
    }
}
