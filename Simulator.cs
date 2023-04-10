using UnityEngine;
using SFCore.Utils;
using Modding;
using System.IO;
using System;
using ModCommon;

public class Simulator : MonoBehaviour {
    private GameObject knight;
    private CollisionDetector collisionDetector;
    private GameObject ascendBeam = null;
    private GameObject eyeBeamGlow = null;
    private PlayMakerFSM attackChoices;
    private GameObject[] lasers;
    private bool lasersInstantiated = false;
    private bool ascendBeamRemoved = false;
    private float x = 48;
    private float y = 151.5f;
    private float timer = 5;
    private int[,] percentageMatrix = new int[244,61];
    private bool finishedDataGathering = false;
    private bool startedDataGathering = false;
    private bool writtenToFile = false;

    private void Awake() {
        knight = GameObject.Find("Knight");
        lasers = new GameObject[100];
        ModHooks.TakeDamageHook += TakeDamage;
        collisionDetector = knight.GetComponent<CollisionDetector>();
        attackChoices = base.gameObject.LocateMyFSM("Attack Choices");
    }

    private void Update() {
        if (!ascendBeam) {
            ascendBeam = GameObject.Find("Ascend Beam");
        }
        if (!eyeBeamGlow) {
            eyeBeamGlow = GameObject.Find("Eye Beam Glow");
        }

        if (ascendBeam != null && eyeBeamGlow != null) {
            if (!ascendBeamRemoved) {
                GameObject.Find("Ascend Beam").LocateMyFSM("Control").RemoveAction("Antic", 0);
                GameObject.Find("Ascend Beam").LocateMyFSM("Control").RemoveAction("Fire", 1);
                ascendBeamRemoved = true;
            }

            for (int i = 0; i < 100; i++) {
                if (!lasersInstantiated) {
                    lasers[i] = GameObject.Instantiate(ascendBeam);
                }

                PlayMakerFSM laserControl = lasers[i].LocateMyFSM("Control");
                if (laserControl.ActiveStateName == "Init" || laserControl.ActiveStateName == "Inert") {
                    laserControl.SendEvent("ANTIC");
                } else {
                    laserControl.SendEvent("FIRE");
                }
            }
            lasersInstantiated = true;

            if (base.gameObject.transform.GetPositionY() > 150f && attackChoices.ActiveStateName == "A2 End" && !finishedDataGathering && lasers[0].LocateMyFSM("Control").ActiveStateName == "Fire") {
                if (!startedDataGathering) {
                    knight.GetComponent<Rigidbody2D>().gravityScale = 0;
                    GameObjectExtensions.PrintSceneHierarchyTree(GameObject.Find("Hazard Plat/Radiant Plat Small (1)"));
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Small (1)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Wide (3)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Wide (4)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (10)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (11)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (12)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (4)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (5)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (6)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (7)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (8)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (9)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (3)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (4)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (5)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (6)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (2)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (3)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (4)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (5)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (6)").transform.GetChild(0).GetComponent<BoxCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Small (1)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Wide (3)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Hazard Plat/Radiant Plat Wide (4)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (10)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (11)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (12)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (4)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (5)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (6)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (7)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (8)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Small (9)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (3)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (4)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (5)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Thick (6)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (2)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (3)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (4)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (5)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                    Physics2D.IgnoreCollision(knight.GetComponent<BoxCollider2D>(), GameObject.Find("Ascend Set/Radiant Plat Wide (6)").transform.GetChild(0).GetComponent<PolygonCollider2D>());
                }
                Modding.Logger.Log("----------------");
                Modding.Logger.Log(x);
                Modding.Logger.Log(y);
                if (timer == 5) {
                    knight.transform.position = new Vector3(x, y, 0.004f);
                    x += 0.5f;
                    if (x > 78) {
                        y -= 0.5f;
                        x = 48;
                        if (y < 30) {
                            finishedDataGathering = true;
                        }
                    }
                    timer -= 1;
                    for (int i = 0; i < 100; i++) {
                        SetLaserPosition(lasers[i], i);
                    }
                } else if (timer > 0) {
                    timer -= 1;
                } else if (timer == 0) {
                    percentageMatrix[(int)((151.5 - y) * 2),(int)((x - 48) * 2)] = collisionDetector.GetNumLasersColliding();
                    timer = 5;
                }
            }
        }

        if (finishedDataGathering && !writtenToFile) {
            StreamWriter writer = new StreamWriter("percentageMatrix2");
            for (int i = 0; i < 244; i++) {
                for (int j = 0; j < 61; j++) {
                    if (j == 60) {
                        writer.Write(percentageMatrix[i,j]);
                        writer.Write(Environment.NewLine);
                    } else {
                        writer.Write(percentageMatrix[i,j]);
                        writer.Write(" ");
                    }
                }
            }
            writer.Close();
            writtenToFile = true;
        }
    }

    private void SetLaserPosition(GameObject laser, int index) {
        Vector3 knightPos = knight.transform.position;
        Vector3 eyeBeamGlowPos = eyeBeamGlow.transform.position;

        laser.SetActive(true);
        laser.transform.position = eyeBeamGlowPos;
        laser.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, knightPos + new Vector3(0, -0.5f, 0) - eyeBeamGlowPos) - 5 + (0.1f * index));

        laser.LocateMyFSM("Control").SendEvent("FIRE");
    }

    public void OnTriggerEnter(Collider other) {
        Modding.Logger.Log("collision");
    }

    public int TakeDamage(ref int hazardType, int damage) {
        return damage;
    }
}