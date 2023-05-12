using System;
using UnityEngine;
using UnityEngine.UI;

namespace BallBlastClone
{
    public class UITurretAbilities : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private Cart _cart;
        [SerializeField] private LevelState _levelState;
        [SerializeField] private UICoinScore _uICoinScore;
        [SerializeField] private Text _textUICoinScore;

        [Header("Value Text's")]
        [SerializeField] private Text _upSpeedFireValueText;
        [SerializeField] private Text _upDamageValueText;
        [SerializeField] private Text _upShellsValueText;

        [Header("Price Text's")]
        [SerializeField] private Text _upSpeedFirePriceText;
        [SerializeField] private Text _upDamagePriceText;
        [SerializeField] private Text _upShellsPriceText;

        private float _increaseSpeedFire = 0.1f;
        private float _deincreaseSpeedFire = 0.05f;
        private int _levelSpeedFire = 1;
        private int _increaseDamage = 1;
        private int _increaseShells = 1;
        private int _priceSpeedFire;
        private int _priceDamage;
        private int _priceShells;
        private string _maxText = "MAX";
        private int _defaultPriceSpeedFire = 5;
        private int _defaultPriceDamage = 5;
        private int _defaultPriceShells = 5;
        private int _defaultLevelSpeedFire = 1;

        private Wallet _wallet;

        private void Awake()
        {
            _turret = GetComponent<Turret>();
            _cart = GetComponent<Cart>();

            _wallet = _uICoinScore.Wallet;

            LoadPriceTurretAbilities();

            RefreshAbilitiesInfo();
        }

        public void UILoadTurretAbilitiesValues()
        {
            _upSpeedFireValueText.text = _levelSpeedFire.ToString();
            _upDamageValueText.text = _turret.Damage.ToString();
            _upShellsValueText.text = _turret.ProjectileAmount.ToString();
        }

        public void UILoadTurretAbilitiesPrices()
        {
            if (Math.Round(_turret.FireSpeed, 2) <= _deincreaseSpeedFire)
            {
                _upSpeedFirePriceText.text = _maxText;
            }
            else
            {
                _upSpeedFirePriceText.text = _priceSpeedFire.ToString();
            }

            _upDamagePriceText.text = _priceDamage.ToString();
            _upShellsPriceText.text = _priceShells.ToString();
        }

        public void UpSpeedFire()
        {
            if (_wallet.IsEnoughCoins(_priceSpeedFire))
            {
                if ((_turret.FireSpeed - _deincreaseSpeedFire) >= _increaseSpeedFire / 10)
                {
                    _turret.FireSpeed -= _deincreaseSpeedFire;
                    _wallet.SpendCoins(this, _priceSpeedFire);

                    _levelSpeedFire++;
                    _priceSpeedFire += _levelSpeedFire;

                    SavePumping();
                }
                else
                {
                    _upSpeedFirePriceText.text = _maxText;
                }
            }
        }

        public void UpDamage()
        {
            if (_wallet.IsEnoughCoins(_priceDamage))
            {
                _wallet.SpendCoins(this, _priceDamage);

                _turret.Damage += _increaseDamage;
                _priceDamage += _turret.Damage;

                SavePumping();
            }
        }

        public void UpShells()
        {
            if (_wallet.IsEnoughCoins(_priceShells))
            {
                _wallet.SpendCoins(this, _priceShells);

                _turret.ProjectileAmount += _increaseShells;
                _priceShells += _turret.ProjectileAmount;

                SavePumping();
            }
        }

        private void SavePumping()
        {
            _uICoinScore.UpdateScoreText();

            _turret.SaveTurretAbilities();
            SavePriceTurretAbilities();

            RefreshAbilitiesInfo();
        }

        public void RefreshAbilitiesInfo()
        {
            UILoadTurretAbilitiesValues();
            UILoadTurretAbilitiesPrices();
        }

        private void LoadPriceTurretAbilities()
        {
            _priceSpeedFire = PlayerPrefs.GetInt("TurretAbilitiesFireSpeedPrice:_priceSpeedFire", _defaultPriceSpeedFire);
            _priceDamage = PlayerPrefs.GetInt("TurretAbilitiesDamagePrice:_priceDamage", _defaultPriceDamage);
            _priceShells = PlayerPrefs.GetInt("TurretAbilitiesProjectileAmountPrice:_priceShells", _defaultPriceShells);

            _levelSpeedFire = PlayerPrefs.GetInt("TurretAbilities_levelSpeedFireValue:_levelSpeedFire", _defaultLevelSpeedFire);
        }

        public void SavePriceTurretAbilities()
        {
            PlayerPrefs.SetInt("TurretAbilitiesFireSpeedPrice:_priceSpeedFire", _priceSpeedFire);
            PlayerPrefs.SetInt("TurretAbilitiesDamagePrice:_priceDamage", _priceDamage);
            PlayerPrefs.SetInt("TurretAbilitiesProjectileAmountPrice:_priceShells", _priceShells);

            PlayerPrefs.SetInt("TurretAbilities_levelSpeedFireValue:_levelSpeedFire", _levelSpeedFire);
        }

        public void ResetPriceTurretAbilities()
        {
            PlayerPrefs.DeleteKey("TurretAbilitiesFireSpeedPrice:_priceSpeedFire");
            PlayerPrefs.DeleteKey("TurretAbilitiesDamagePrice:_priceDamage");
            PlayerPrefs.DeleteKey("TurretAbilitiesProjectileAmountPrice:_priceShells");

            PlayerPrefs.DeleteKey("TurretAbilities_levelSpeedFireValue:_levelSpeedFire");
        }


    }
}