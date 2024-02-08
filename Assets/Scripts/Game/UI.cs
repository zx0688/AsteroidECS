using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI : MonoBehaviour
{
    [SerializeField] private Text velocity;
    [SerializeField] private Text position;
    [SerializeField] private Text angle;
    [SerializeField] private Text ammo;
    [SerializeField] private Slider reload;
    [SerializeField] private Text scoreGameOver;
    [SerializeField] private Text scoreSmall;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject startPanel;


    public void SetVelocity(float v) => velocity.text = string.Concat("Velocity: ", v);
    public void SetPosition(Vector3 p) => position.text = string.Concat("Position: ", p.ToString());
    public void SetAngle(float a) => angle.text = string.Concat("Angle: ", a);
    public void SetAmmo(int a, int max) => ammo.text = string.Concat("Ammo: ", a, "/", max);
    public void SetReload(float r) => reload.value = r;
    public void SetScore(int s) => scoreGameOver.text = scoreSmall.text = string.Concat("Score: ", s);

    public void SetGameOverVisible(bool enable) => gameOver.SetActive(enable);
    public void SetStartPanelVisible(bool enable) => startPanel.SetActive(enable);


}

