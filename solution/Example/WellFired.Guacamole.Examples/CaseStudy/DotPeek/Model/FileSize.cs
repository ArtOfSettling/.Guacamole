using System;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public struct FileSize
    {
        private const float EquivalenceTolerance = 512f; 
        
        public bool Equals(FileSize other)
        {
            return _sizeInKb.Equals(other._sizeInKb) && _sizeInMb.Equals(other._sizeInMb);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FileSize && Equals((FileSize) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_sizeInKb.GetHashCode() * 397) ^ _sizeInMb.GetHashCode();
            }
        }

        public static bool operator ==(FileSize a, FileSize b)
        {
            return Math.Abs(a._sizeInKb - b._sizeInKb) < EquivalenceTolerance;
        }

        public static bool operator !=(FileSize a, FileSize b)
        {
            return !(a == b);
        }

        private readonly float _sizeInKb;
        private readonly float _sizeInMb;

        public FileSize(float sizeInKb)
        {
            _sizeInKb = sizeInKb;
            _sizeInMb = sizeInKb / 1024;
        }

        public override string ToString()
        {
            return $"{_sizeInKb} kb";
        }

        public static FileSize ConvertToFileSize(string size, string unit)
        {
            const string b = "b";
            const string kb = "kb";
            const string mb = "mb";
            const string gb = "gb";

            var sizeFloat = float.Parse(size);
            float sizeInKb = -1;
            switch (unit)
            {
                case b:
                    sizeInKb = sizeFloat / 1024f;
                    break;

                case kb:
                    sizeInKb = sizeFloat;
                    break;

                case mb:
                    sizeInKb = sizeFloat * 1024f;
                    break;

                case gb:
                    sizeInKb = sizeFloat * 1024f * 1024f;
                    break;
            }

            return new FileSize(sizeInKb);
        }
    }
}