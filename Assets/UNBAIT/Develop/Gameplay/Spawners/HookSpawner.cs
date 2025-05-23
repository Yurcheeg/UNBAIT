﻿using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Spawners.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Spawners
{
    public class HookSpawner : Spawner<Hook>
    {
        [SerializeField] private Hook _hookPrefab;

        [SerializeField] private float _spawnDelay;

        public override Hook Spawn(Hook prefab)
        {
            if (prefab == null)
                throw new NullReferenceException("Prefab is not assigned");

            Hook hook = base.Spawn(prefab);
            return hook;
        }

        public Hook ThrowHook() => Spawn(_hookPrefab);
    }
}
