using System;
using System.Collections.Generic;
using System.Reflection;

namespace CommodityService
{
    public class CommodityService
    {
        private readonly List<Commodity> _Commodities;

        public CommodityService()
        {
            _Commodities = new List<Commodity>();
        }

        public CommodityService(List<Commodity> commodities)
        {
            _Commodities = commodities;
        }

        public IEnumerable<int> GroupValueSums(int amountPerGroup, string dataName)
        {
            var fieldInfo = typeof(Commodity).GetField(dataName);
            ThrowIfArgumentInvaild(amountPerGroup, fieldInfo);

            return GroupValueSums(amountPerGroup, fieldInfo);
        }

        private IEnumerable<int> GroupValueSums(int amountPerGroup, FieldInfo fieldInfo)
        {
            if (amountPerGroup == 0)
                return new[] {0};

            var result = new List<int>();
            for (var startIndex = 0; startIndex < _Commodities.Count; startIndex += amountPerGroup)
                result.Add(GetSum(amountPerGroup, fieldInfo, startIndex));
            return result;
        }

        private int GetSum(int amountPerGroup, FieldInfo fieldInfo, int startIndex)
        {
            var sum = 0;
            var sumAmount = Math.Min(amountPerGroup, _Commodities.Count - startIndex);
            for (var i = 0; i < sumAmount; i++)
                sum += (int) fieldInfo.GetValue(_Commodities[startIndex + i]);
            return sum;
        }

        private static void ThrowIfArgumentInvaild(int amountPerGroup, FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentException();
            if (amountPerGroup < 0)
                throw new ArgumentException();
        }
    }
}
