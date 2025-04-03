#if DEVELOPMENT_BUILD || UNITY_EDITOR
#define NTWEEN_DEBUG
#endif

using System;
using UnityEngine;
using UnityEngine.Pool;

namespace NTween {

    /// <summary>
    /// Tween�̃p�����[�^���i�[����N���X�D
    /// �C���X�^���X��Object-Pool�ŊǗ����邱�Ƃŗ]�v�ȃA���P�[�V�������������j�D
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    internal sealed class TweenBuilderBuffer<TValue> {

        public ushort version;

        public TValue startValue;
        public TValue endValue;

        public float duration;
        public float delay;
        public Ease ease;

        // �o�C���h�p
        public object state0;
        public object state1;
        public object state2;
        public byte stateCount;
        public object updateAction;

        // Callback
        public Action onCompleteAction;

#if NTWEEN_DEBUG
        public string debugName;
#endif


        public void ResetValues() {

            version++;              // �Ȃ��C���N�������g�Ȃ̂�������Ȃ�

            startValue = default;
            endValue = default;

            duration = default;
            delay = default;
            ease = default;

            // 
            state0 = default;
            state1 = default;
            state2 = default;
            stateCount = default;

            // Callback
            updateAction = default;
            onCompleteAction = default;

#if NTWEEN_DEBUG
            debugName = default;
#endif
        }


        /// ----------------------------------------------------------------------------
        #region Static

        private static readonly ObjectPool<TweenBuilderBuffer<TValue>> _pool =
            new(
                createFunc: () => new TweenBuilderBuffer<TValue>(),
                actionOnRelease: buffer => buffer.ResetValues(),
                collectionCheck: false, // �f�t�H���g�ł�true�����Afalse�ɂ��邱�Ƃœ�d����̃`�F�b�N�𖳌����ł���
                defaultCapacity: 10,    // �����e��
                maxSize: 100            // �ő�ێ���
            );

        public static TweenBuilderBuffer<TValue> Rent() {
            return _pool.Get();
        }

        public static void Return(TweenBuilderBuffer<TValue> buffer) {
            _pool.Release(buffer);
        }
        #endregion
    }
}
