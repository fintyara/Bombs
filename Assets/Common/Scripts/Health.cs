using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class Health : MonoBehaviour
    {
        public UnityEvent onEndHp;
        public UnityEvent onDamage;
        [SerializeField] private List<DamageType> _vulnerabilities;
        [Space(10)] 
        [SerializeField] private bool immortal;
        [SerializeField] private int _startHp = 100;
        [SerializeField] private int _maxHp = 150;
        [ShowOnly][SerializeField] private int _curHp;
        private bool _empty;
        
        
        private void OnEnable()
        {
            _curHp = _startHp;
            _empty = false;
        }
        
        
        public void Damage(DamageType damageType, int count)
        {
            if (_empty || !_vulnerabilities.Contains(damageType))
                return;
            
            onDamage?.Invoke();

            if (immortal)
                return;
            
            _curHp -= count;
            if (_curHp < 0)
            {
                _curHp = 0;
                _empty = true;
                onEndHp?.Invoke();
            }
        }
        public void Heal( int count)
        {
            if (_empty)
                return;
            
            _curHp += count;
            if (_curHp > _maxHp)
                _curHp = _maxHp;
        }
    }
}
