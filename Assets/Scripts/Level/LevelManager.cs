using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ2023.TileObject;
using GGJ2023.UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using GGJ2023.Audio;
using GGJ2023.Event;
using Unity.Mathematics;
using UnityEngine.AddressableAssets;
using UnityEngine.VFX;

namespace GGJ2023.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public Grid grid;
        public Tilemap mapTilemap;
        public Tilemap objectTilemap;
        private LevelUIManager uiManager;

        public LevelUIManager UIManager
        {
            get
            {
                if (uiManager == null)
                {
                    uiManager = GetComponentInChildren<LevelUIManager>();
                }

                return uiManager;
            }
        }

        public Transition transition;
        private Dictionary<Vector3Int, List<TileObject.TileObject>> currentObjectMap;

        public Dictionary<Vector3Int, List<TileObject.TileObject>> CurrentObjectMap
        {
            get
            {
                if (currentObjectMap == null)
                {
                    currentObjectMap = new Dictionary<Vector3Int, List<TileObject.TileObject>>();
                    UpdateTileObjectMap();
                }

                return currentObjectMap;
            }
        }

        private Dictionary<Vector3Int, MapTile.MapTile> currentMapTiles;

        public Dictionary<Vector3Int, MapTile.MapTile> CurrentMapTiles
        {
            get
            {
                if (currentMapTiles == null)
                {
                    currentMapTiles = new Dictionary<Vector3Int, MapTile.MapTile>();
                    foreach (Transform trans in mapTilemap.transform)
                    {
                        if (trans.TryGetComponent(out MapTile.MapTile mapTile))
                        {
                            Vector3Int pos = grid.WorldToCell(trans.position);
                            if (!CurrentMapTiles.ContainsKey(pos))
                            {
                                CurrentMapTiles.Add(pos, mapTile);
                            }
                        }
                    }
                }

                return currentMapTiles;
            }
        }

        public Dictionary<RegistryTileObject, int> seedsCounts;
        public PriorityAction<LevelManager> onRoundExecute;
        public PriorityAction<LevelManager> onRoundExecuted;
        public PriorityAction<LevelManager> onRoundEnd;
        private TumbleweedTileObject player;

        public TumbleweedTileObject Player
        {
            get
            {
                if (player == null)
                {
                    player = objectTilemap.transform.GetComponentInChildren<TumbleweedTileObject>();
                }

                return player;
            }
        }

        public Transform centerMarker;

        [HideInInspector] public int roundCount = 0;
        [HideInInspector] public int flowerCount = 0;
        [HideInInspector] public DirectionType windDirection;

        public Color leavesColor;
        
        public int levelIndex;
        public string levelIndexName;
        public string levelName;

        public int seedExtinguishingTree, seedFireBlocker, seedMadWillow;
        public int victoryFlowerAmount;
        [TextArea] public string enterText;

        [HideInInspector] public bool isWon;

        private void Awake()
        {
            Instance = this;
            controlAble = false; //等待场景加载完成才可以控制
        }

        private void OnDisable()
        {
            RemoveActions();
        }

        private void OnDestroy()
        {
            RemoveActions();
        }

        public void InitLevel()
        {
            controlAble = true;
            onRoundExecute = new PriorityAction<LevelManager>();
            onRoundExecuted = new PriorityAction<LevelManager>();
            onRoundEnd = new PriorityAction<LevelManager>();
            roundCount = 0;
            flowerCount = -1;
            seedsCounts = new Dictionary<RegistryTileObject, int>();
            timedAnims = new List<TimedAnim>();
            seedsCounts.Add(TileObjectsReferences.extinguishingTree, seedExtinguishingTree);
            seedsCounts.Add(TileObjectsReferences.fireBlockerTree, seedFireBlocker);
            seedsCounts.Add(TileObjectsReferences.madWillow, seedMadWillow);
            AddActions();
            UIManager.InitUI(this);
            foreach (Transform trans in objectTilemap.transform)
            {
                if (trans.TryGetComponent(out TileObject.TileObject tileObject))
                {
                    tileObject.Init();
                }
            }

            transition.SetLevelInfo(levelIndexName, levelName);
            transition.TransitionIn();
            uiManager.OnFlowerAmountChanged(flowerCount);
        }

        private void Update()
        {
            InputUpdate();
            if (Input.GetKeyDown(KeyCode.M))
            {
                Win(); // todo debug
            }
        }

        #region Input

        private KeyCode keyBuffer;
        private float keyBufferCountDown;
        private float keyBufferDuration = 1.5f;

        private KeyCode[] usedKeyCode =
        {
            KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.N
        };

        private void InputUpdate()
        {
            //更新按键缓存
            if (Input.anyKeyDown)
            {
                foreach (var code in usedKeyCode)
                {
                    DetectKeyDown(code);
                }

                HandleInput(keyBuffer);
            }

            //计时
            keyBufferCountDown = Mathf.Max(0, keyBufferCountDown - Time.deltaTime);
        }

        private void DetectKeyDown(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyBuffer = keyCode;
                keyBufferCountDown = keyBufferDuration;
            }
        }

        public void HandleInput(KeyCode keyCode)
        {
            if (keyBufferCountDown == 0) return;
            if (controlAble)
            {
                keyBufferCountDown = 0;
                if (keyCode == KeyCode.W)
                {
                    windDirection = DirectionType.Forward;
                }

                if (keyCode == KeyCode.A)
                {
                    windDirection = DirectionType.Left;
                }

                if (keyCode == KeyCode.S)
                {
                    windDirection = DirectionType.Back;
                }

                if (keyCode == KeyCode.D)
                {
                    windDirection = DirectionType.Right;
                }

                if (keyCode == KeyCode.D || keyCode == KeyCode.A || keyCode == KeyCode.W || keyCode == KeyCode.S)
                {
                    //不能卡墙移动
                    if (!Player.CanMoveTo(this, Player.GetMoveToPos()))
                    {
                        //todo 提醒玩家不能向水中移动

                        //如果是防火墙，可以卡
                        if (!this.HasTileType(Player.GetMoveToPos(), TileObjectsReferences.wall))
                            return;
                    }

                    NextRound();
                }

                if (keyCode == KeyCode.J)
                {
                    SownAt(Player.CellPos, TileObjectsReferences.extinguishingTree);
                }

                if (keyCode == KeyCode.K)
                {
                    SownAt(Player.CellPos, TileObjectsReferences.fireBlockerTree);
                }

                if (keyCode == KeyCode.L)
                {
                    SownAt(Player.CellPos, TileObjectsReferences.madWillow);
                }
            }

            if (Input.GetKeyDown(KeyCode.N) && isWon)
            {
                uiManager.NextLevel();
            }
        }

        #endregion

        #region Seed

        public void SownAt(Vector3Int pos, RegistryTileObject treeType)
        {
            if (seedsCounts[treeType] > 0 && CanAddTileObject(pos, treeType))
            {
                controlAble = false;
                Player.animator.Play("sow"); //播放播种动画
                StartCoroutine(WaitSowStop(this, pos, treeType));
                ReduceSeed(treeType);
            }
        }

        IEnumerator<int> WaitSowStop(LevelManager levelManager, Vector3Int pos, RegistryTileObject treeType)
        {
            yield return 0;
            AnimatorStateInfo stateinfo = Player.animator.GetCurrentAnimatorStateInfo(0);

            //播种动画结束后，才添加种子瓦片
            if (stateinfo.IsName("sow") && (stateinfo.normalizedTime > 0.95f))
            {
                AddTileObject(pos, treeType);
                UpdateTileObjectMap();
                controlAble = true;
            }
            else
            {
                StartCoroutine(WaitSowStop(levelManager, pos, treeType));
            }
        }

        public void AddSeed(RegistryTileObject seedType, int amount = 1)
        {
            if (seedsCounts.ContainsKey(seedType))
            {
                seedsCounts[seedType] += amount;
            }

            UIManager.OnSeedChanged();
        }

        public void ReduceSeed(RegistryTileObject seedType, int amount = 1)
        {
            if (seedsCounts.ContainsKey(seedType))
            {
                seedsCounts[seedType] = Mathf.Max(seedsCounts[seedType] - 1, 0);
            }

            UIManager.OnSeedChanged();
        }

        #endregion

        #region Wind

        public GameObject smallWindPrefab;
        public GameObject bigWindPrefab;
        private GameObject smallWindInstance;

        public GameObject leavesWithWindPrefab;

        public void ProcessWind()
        {
            float r = 0;
            switch (windDirection)
            {
                case DirectionType.Right:
                    r = 90;
                    break;
                case DirectionType.Back:
                    r = 180;
                    break;
                case DirectionType.Left:
                    r = 270;
                    break;
            }

            ProcessSmallWind(r);
            ProcessBigWind(r);
            AudioManager.Instance.Play("effect", "wind");
        }

        public void ProcessSmallWind(float rotation = 0)
        {
            if (smallWindInstance != null) Destroy(smallWindInstance);
            smallWindInstance = Instantiate(smallWindPrefab, centerMarker);
            smallWindInstance.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        public void ProcessBigWind(float rotation = 0)
        {
            GameObject bigWindInstance = Instantiate(bigWindPrefab, centerMarker);
            bigWindInstance.transform.rotation = Quaternion.Euler(0, rotation, 0);

            GameObject leaves = Instantiate(leavesWithWindPrefab, centerMarker);
            
            leaves.GetComponentInChildren<VisualEffect>().SetVector4("Color", new Vector4(leavesColor.r,leavesColor.g,leavesColor.b,leavesColor.a));
            leaves.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        #endregion

        #region Round

        public List<TimedAnim> timedAnims;
        [HideInInspector] public bool controlAble = false;

        public void AddAnimation(TimedAnim anim)
        {
            timedAnims.Add(anim);
        }

        public float GetMaxAnimDuration(List<TimedAnim> anims)
        {
            float i = 0;
            foreach (TimedAnim timedAnim in anims)
            {
                i = Mathf.Max(timedAnim.duration, i);
            }

            return i;
        }

        public void NextRound()
        {
            controlAble = false;
            timedAnims.Clear();
            ProcessWind();
            UpdateTileObjectMap();
            onRoundExecute.Invoke(this); //执行逻辑和添加动画
            roundCount++;
            UpdateTileObjectMap();
            onRoundExecuted.Invoke(this);
            //HandleTileCollision();
            Invoke(nameof(WaitForRoundExecuteAnim), GetMaxAnimDuration(timedAnims));
        }

        public void WaitForRoundExecuteAnim()
        {
            timedAnims.Clear();
            onRoundEnd.Invoke(this); //执行逻辑和添加动画
            UpdateTileObjectMap();
            HandleTileCollision();
            Invoke(nameof(RoundEnd), GetMaxAnimDuration(timedAnims));
        }

        public void RoundEnd()
        {
            controlAble = true;
            UIManager.OnFlowerAmountChanged(flowerCount);
            DetectWin(); //检测胜利
            HandleInput(keyBuffer);
        }

        public void GameOver()
        {
            GameManager.Instance.LoadLevel(levelIndex);
            Debug.Log("Game Over!");
        }

        public bool DetectWin()
        {
            if (flowerCount == victoryFlowerAmount)
            {
                Win();
                return true;
            }

            return false;
        }

        public void Win()
        {
            //audioSource.Play();
            AudioManager.Instance.Play("effect", "win");
            isWon = true;
            controlAble = false;
            uiManager.OnWin();
        }

        /// <summary>
        /// 处理碰撞, 若有碰撞则触发 OnTileCollide
        /// </summary>
        void HandleTileCollision()
        {
            foreach (var pair in CurrentObjectMap)
            {
                List<TileObject.TileObject> tileObjects = pair.Value;
                if (tileObjects?.Count > 1)
                {
                    for (int i = 0; i < tileObjects.Count; i++)
                    {
                        TileObject.TileObject obj = tileObjects[i];
                        if (!obj.isDead)
                        {
                            List<TileObject.TileObject> list = new List<TileObject.TileObject>(tileObjects);
                            list.Remove(obj);
                            obj.OnTileCollide(this, list.ToArray(), pair.Key);
                        }
                    }
                }
            }
        }

        #endregion

        #region Map

        public bool HasAndGetMapTile(Vector3Int pos, out MapTile.MapTile mapTile)
        {
            bool flag = CurrentMapTiles.ContainsKey(pos);
            mapTile = flag ? CurrentMapTiles[pos] : null;
            return flag;
        }

        /// <summary>
        /// 更新当前状态下所有瓦片物体的字典
        /// </summary>
        void UpdateTileObjectMap()
        {
            CurrentObjectMap.Clear();
            foreach (Transform trans in objectTilemap.transform)
            {
                if (trans.TryGetComponent(out TileObject.TileObject tileObject))
                {
                    if (!CurrentObjectMap.ContainsKey(tileObject.CellPos))
                    {
                        CurrentObjectMap.Add(tileObject.CellPos, new List<TileObject.TileObject>());
                        //Debug.Log(tileObject.cellPos + " : " + tileObject.registryName);
                    }

                    CurrentObjectMap[tileObject.CellPos].Add(tileObject);
                }
            }
        }

        /// <summary>
        /// 添加新的瓦片物体
        /// </summary>
        /// <param name="cellPos">位置</param>
        /// <param name="registryTile">物体实例</param>
        public bool AddTileObject(Vector3Int cellPos, RegistryTileObject registryTile)
        {
            if (registryTile.prefab.TryGetComponent(out TileObject.TileObject tileObject))
            {
                if (tileObject.OnTileAdded(this, cellPos, out GameObject instance))
                {
                    TileObject.TileObject tileInstance = instance.GetComponent<TileObject.TileObject>();
                    instance.transform.position = grid.CellToWorld(cellPos);
                    instance.transform.parent = objectTilemap.transform;
                    tileInstance.Init();
                    AddObjActions(tileInstance);
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 判断是否可以添加新的瓦片物体
        /// </summary>
        public bool CanAddTileObject(Vector3Int cellPos, RegistryTileObject registryTile)
        {
            if (registryTile.prefab.TryGetComponent(out TileObject.TileObject tileObject))
            {
                if (tileObject.CanBeAddedAt(this, cellPos))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取格子上的所有物体
        /// </summary>
        public List<TileObject.TileObject> GetTileObjectsOnPos(Vector3Int cellPos)
        {
            if (!CurrentObjectMap.ContainsKey(cellPos)) return new List<TileObject.TileObject>();
            return CurrentObjectMap[cellPos];
        }

        /// <summary>
        /// 某个格子是否有特定物体
        /// </summary>
        public bool HasTileType(Vector3Int cellPos, RegistryTileObject registryTile)
        {
            if (!CurrentObjectMap.ContainsKey(cellPos)) return false;
            List<TileObject.TileObject> tileObjects = CurrentObjectMap[cellPos];
            bool flag = false;
            if (tileObjects != null)
            {
                foreach (var tileObject in tileObjects)
                {
                    flag = flag || tileObject.Is(registryTile);
                }
            }

            return flag;
        }

        public bool HasAndGetTileObject(Vector3Int cellPos, out TileObject.TileObject[] objects)
        {
            objects = null;
            if (!CurrentObjectMap.ContainsKey(cellPos)) return false;
            List<TileObject.TileObject> tileObjects = CurrentObjectMap[cellPos];
            if (tileObjects != null && tileObjects.Count > 0)
            {
                objects = tileObjects.ToArray();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 周围八个格子是否有特定物体
        /// </summary>
        public bool Neighbour8TilesHasType(Vector3Int cellPos, RegistryTileObject registryTile)
        {
            bool flag = false;
            foreach (var pos in DirectionTypeUtils.Get8Neighbour(cellPos))
            {
                flag = flag || HasTileType(pos, registryTile);
            }

            return flag;
        }

        public bool HasAndGetNeighbour8Tiles(Vector3Int cellPos, out List<TileObject.TileObject[]> tileLists)
        {
            tileLists = new List<TileObject.TileObject[]>();
            bool flag = false;
            Vector3Int[] poses = DirectionTypeUtils.Get8Neighbour(cellPos);
            for (int i = 0; i < poses.Length; i++)
            {
                Vector3Int pos = poses[i];
                if (HasAndGetTileObject(pos, out TileObject.TileObject[] tiles))
                {
                    flag = true;
                    tileLists.Add(tiles);
                }
            }

            return flag;
        }

        public bool HasAndGetNeighbour4Tiles(Vector3Int cellPos,
            out Dictionary<DirectionType, TileObject.TileObject[]> tileLists)
        {
            tileLists = new Dictionary<DirectionType, TileObject.TileObject[]>();
            bool flag = false;
            Vector3Int[] poses = DirectionTypeUtils.Get4Neighbour(cellPos);
            for (int i = 0; i < poses.Length; i++)
            {
                Vector3Int pos = poses[i];
                if (HasAndGetTileObject(pos, out TileObject.TileObject[] tiles))
                {
                    flag = true;
                    tileLists.Add((DirectionType)i, tiles);
                }
            }

            return flag;
        }

        /// <summary>
        /// 周围四个格子是否有特定物体
        /// </summary>
        public bool Neighbour4TilesHasType(Vector3Int cellPos, RegistryTileObject registryTile)
        {
            bool flag = false;
            foreach (var pos in DirectionTypeUtils.Get4Neighbour(cellPos))
            {
                flag = flag || HasTileType(pos, registryTile);
            }

            return flag;
        }

        #endregion

        #region Event

        public void AddObjActions(TileObject.TileObject tileObject)
        {
            onRoundExecute.AddListener(tileObject.OnRoundExecute);
            onRoundExecuted.AddListener(tileObject.OnRoundExecuted);
            onRoundEnd.AddListener(tileObject.OnRoundEnd);
        }

        public void RemoveObjActions(TileObject.TileObject tileObject)
        {
            onRoundExecute.RemoveListener(tileObject.OnRoundExecute);
            onRoundExecuted.RemoveListener(tileObject.OnRoundExecuted);
            onRoundEnd.RemoveListener(tileObject.OnRoundEnd);
        }

        void AddActions()
        {
            foreach (Transform tileTrans in objectTilemap.transform)
            {
                if (tileTrans.TryGetComponent(out GGJ2023.TileObject.TileObject tileObject))
                {
                    AddObjActions(tileObject);
                }
            }
        }

        void RemoveActions()
        {
            foreach (Transform tileTrans in objectTilemap.transform)
            {
                if (tileTrans.TryGetComponent(out GGJ2023.TileObject.TileObject tileObject))
                {
                    RemoveObjActions(tileObject);
                }
            }
        }

        #endregion

        #region Sounds

        public SoundSource fireSoundSource = new("effect", "fire");

        #endregion
    }
}