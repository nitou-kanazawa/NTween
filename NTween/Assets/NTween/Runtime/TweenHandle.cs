using System;

namespace NTween {
    
    public struct TweenHandle : IEquatable<TweenHandle>{

        public int StorageId { get; }

        public int Index { get; }

        public readonly bool Equals(TweenHandle other) {
            return StorageId == other.StorageId
                && Index == other.Index; 
        }

        public override readonly bool Equals(object obj) {
            return (obj is TweenHandle handle) && this.Equals(handle);
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(Index, StorageId);
        }

        public static bool operator ==(TweenHandle a, TweenHandle b) {
            return a.Equals(b);
        }

        public static bool operator !=(TweenHandle a, TweenHandle b) {
            return !(a == b);
        }
    }
}
