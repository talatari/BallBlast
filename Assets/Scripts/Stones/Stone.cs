using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BallBlastClone
{
    [System.Serializable]
    public class StonePallete
    {
        public Color StoneColor;
    }

    public enum SizeStone
    {
        Small,
        Normal,
        Big,
        Large,
        Huge
    }

    [RequireComponent(typeof(StoneMovement))]
    public class Stone : Destuctible
    {
        [Header("View")]
        [SerializeField] private SizeStone _sizeStone;
        [SerializeField] private StonePallete[] _stonePallete;

        [Header("Physics")]
        [SerializeField] private float _spawnUpForce;

        private StoneMovement _stoneMovement;
        private SpriteRenderer _stoneViewSpriteRenderer;
        private Color _color;
        private Color _alphaMax = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        private LevelProgressBar _levelProgressBar;
        public UnityEvent<int> OnStoneEvent;

        private void Awake()
        {
            _stoneMovement = GetComponent<StoneMovement>();
            Die.AddListener(OnStoneDestroyed);

            SetSizeStone(_sizeStone);
            _stoneViewSpriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

            _levelProgressBar = GameObject.FindObjectOfType<LevelProgressBar>();
            OnStoneEvent.AddListener(_levelProgressBar.FillAmountProgressBar);
        }

        private void OnDestroy()
        {
            Die.RemoveListener(OnStoneDestroyed);
            OnStoneEvent.RemoveListener(_levelProgressBar.FillAmountProgressBar);
        }

        private void OnStoneDestroyed()
        {
            if (_sizeStone != SizeStone.Small)
            {
                SpawnStones();
            }

            OnStoneEvent.Invoke(1);

            Destroy(gameObject);
        }

        private void SpawnStones()
        {
            for (int i = 0; i < 2; i++)
            {
                Stone stone = Instantiate(this, transform.position, Quaternion.identity);

                stone.SetSizeStone(_sizeStone - 1);
                stone.SetColorStone();

                stone.MaxHitPoints = (int)_sizeStone;

                stone._stoneMovement.AddVerticalVelocity(_spawnUpForce);
                stone._stoneMovement.SetHorizontalDirection((i % 2 * 2) - 1);
            }
        }

        public void SetSizeStone(SizeStone sizeStone)
        {
            if (sizeStone < 0)
            {
                return;
            }

            transform.localScale = GetVectorFromSizeStone(sizeStone);
            this._sizeStone = sizeStone;
        }

        private Vector3 GetVectorFromSizeStone(SizeStone sizeStone)
        {
            switch (sizeStone)
            {
                case SizeStone.Small:
                    return new Vector3(0.40f, 0.40f, 0.40f);

                case SizeStone.Normal:
                    return new Vector3(0.55f, 0.55f, 0.55f);

                case SizeStone.Big:
                    return new Vector3(0.70f, 0.70f, 0.70f);

                case SizeStone.Large:
                    return new Vector3(0.85f, 0.85f, 0.85f);

                case SizeStone.Huge:
                    return new Vector3(1.00f, 1.00f, 1.00f);

                default:
                    return new Vector3(1.00f, 1.00f, 1.00f);
            }
        }

        public void SetColorStone()
        {
            int index = Random.Range(0, _stonePallete.Length);
            _color = _stonePallete[index].StoneColor;
            _stoneViewSpriteRenderer.color = _color + _alphaMax;
        }


    }
}