using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Babylonian
{
    public class PlayerAttack : PlayerBase
    {
        public Transform ProjectileSpawn;

        public Vector2 Direction;
        public float FreeTime = 1f;
        public float Power;
        public float LifeTime = 3f;
        public float Delay = 1;
        public int SprayAmmount = 3;
        private float lastThrowDate;
        private bool singleplayer = false;

        private void Start()
        {
            lastThrowDate =Time.time;
            if (ServerBehaviour.Instance == null && ClientBehaviour.Instance == null)
                singleplayer = true;
        }

        private void OnEnable()
        {
            OnPlayerAttack += Attack;
        }

        private void FixedUpdate()
        {

            var shoot = InputState.GetButtonValue(InputButtons[0]);

            if (shoot)
            {
                //FireProjectile(Power, Direction);
            }

        }
        private void Update()
        {
            UpdateAimInput();
        }
        float angle;
        private void UpdateAimInput()
        {
            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            if(!attackFromClient)
                ProjectileSpawn.rotation = Quaternion.Euler(0, 0, angle);
        }
        bool attackFromClient = false;
        public void Attack(float _angle)
        {
            if (singleplayer) return;
            attackFromClient = true;
            ProjectileSpawn.rotation = Quaternion.Euler(0,0, _angle);

            
            Debug.Log("Attack coming from client"+ProjectileSpawn.rotation);
            FireProjectile(Power, Direction);
        }


        public void OnAttack(InputAction.CallbackContext value)
        {
            attackFromClient = false;
            FireProjectile(Power, Direction);
        }

        private void FireProjectile(float power, Vector2 dir)
        {
            if (Time.time - lastThrowDate > Delay)
            {
                SpumAnimator.PlayAnimation(5);

                var mouse = Input.mousePosition;
                var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
                var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
                var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                Camera cam = Camera.main;

                Vector3 mousePos = Input.mousePosition;
                dir = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
                BaseProjectile[] proj = new BaseProjectile[SprayAmmount];

                for (int i = 0; i < SprayAmmount; i++)
                {
                    proj[i] = ObjectPool.Instance.SpawnFromPool<BaseProjectile>(ProjectileSpawn.position, ProjectileSpawn.rotation) as BaseProjectile;
                    proj[i].gameObject.SetActive(true);
                    proj[i].MoveProjectile(Random.Range(power-10,power+10), dir, LifeTime);
                    if (!singleplayer)
                    {
                        NetProjectilePosition projectileMsg = new NetProjectilePosition(proj[i].GetInstanceID(), ProjectileSpawn.rotation.x, ProjectileSpawn.rotation.y, angle);
                        //send to server
                        if (ClientBehaviour.Instance != null) ClientBehaviour.Instance.SendToServer(projectileMsg);
                    }
                    //UpdatePlayerPosition.Instance.ActiveProjectiles.Add(proj[i]);
                }

                lastThrowDate = Time.time;

                StartCoroutine(WaitForDownSlide(FreeTime));

            }
        }

        private IEnumerator WaitForDownSlide(float time)
        {

            ToggleScripts(false);
            Body2D.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(time);
            Body2D.constraints = RigidbodyConstraints2D.None;
            Body2D.constraints = RigidbodyConstraints2D.FreezeRotation;


            ToggleScripts(true);



        }

        private void OnDisable()
        {
            OnPlayerAttack -= Attack;
        }
    }
}
